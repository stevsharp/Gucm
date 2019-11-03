using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Data.EntityFramework.BaseDbContext
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options) { }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder, string context, string assemblyname)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = GetConfiguations(assemblyname);

            foreach (var type in typesToRegister)
            {
                DBDataContextAttribute attribute = (DBDataContextAttribute)type.GetCustomAttribute(typeof(DBDataContextAttribute));
                if (attribute != null)
                {
                    var db = attribute.Description;
                    if (db.Equals(context))
                    {
                        dynamic configurationInstance = Activator.CreateInstance(type);
                        modelBuilder.ApplyConfiguration(configurationInstance);
                    }
                }
            }
        }

    
        protected void Validate()
        {
            var changedEntities = this.ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var errors = new List<ValidationResult>();

            foreach (var e in changedEntities)
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(e.Entity, vc, errors, validateAllProperties: true);
            }
        }

        protected List<Type> GetConfiguations(string assemblyname)
        {
            var assembly = GetAssemblyByName(assemblyname);

            if (assembly != null)
            {
                return assembly.GetTypes()
                      .Where(t => t.GetInterfaces().Any(type =>
                      type.IsGenericType
                      && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();
            }

            return new List<Type>();
        }

        protected Assembly GetAssemblyByName(string assemblyname)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == assemblyname);
        }


        protected List<Type> GetLocalAssemblyConfiguations()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                  .Where(t => t.GetInterfaces().Any(type =>
                  type.IsGenericType
                  && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();
        }

        public List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, int commandTimeout = 30)
        {

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = commandTimeout;
                command.CommandType = CommandType.Text;

                if (this.Database.GetDbConnection().State != ConnectionState.Open)
                    this.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));

                    return entities;
                }
            }
        }

        public List<T> RawSqlQueryWithParameters<T>(string query, SqlParameter[] parameters, Func<DbDataReader, T> map, int commandTimeout = 30)
        {

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = commandTimeout;
                command.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (this.Database.GetDbConnection().State != ConnectionState.Open)
                    this.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));

                    return entities;
                }
            }
        }

        public async Task<List<T>> RawSqlQueryWithParametersAsync<T>(string query, SqlParameter[] parameters, Func<DbDataReader, T> map, int commandTimeout = 30)
        {

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = commandTimeout;
                command.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (this.Database.GetDbConnection().State != ConnectionState.Open)
                    this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));

                    return entities;
                }
            }
        }

        public async Task<object> RawSqlQueryWithParametersAsync(string query, SqlParameter[] parameters, int commandTimeout = 30)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = commandTimeout;
                command.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (this.Database.GetDbConnection().State != ConnectionState.Open)
                    this.Database.OpenConnection();

                return await command.ExecuteScalarAsync();

            }
        }

        public async Task<int> ExecuteSqlCommandAsync(string query, SqlParameter[] parameters, int commandTimeout = 30)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = commandTimeout;
                command.CommandType = CommandType.Text;

                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                if (this.Database.GetDbConnection().State != ConnectionState.Open)
                    this.Database.OpenConnection();

                return await command.ExecuteNonQueryAsync();

            }
        }

        public override int SaveChanges()
        {
            try
            {
                this.Validate();
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. " +
                    "Please refresh and save again.", ex);
            }
            catch (ValidationException ex)
            {
                throw new DatabaseUpdateException("Entry Validation error", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseUpdateException("Database Update error", ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                this.Validate();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. " +
                    "Please refresh and save again.", ex);

            }
            catch (ValidationException ex)
            {
                throw new DatabaseUpdateException("Entry Validation error", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseUpdateException("Database Update error", ex);
            }
        }
    }
}
