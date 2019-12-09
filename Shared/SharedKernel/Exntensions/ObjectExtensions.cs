using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq.Expressions;


namespace SharedKernel.Exntensions
{
    public static class ObjectExtensions
    {
        public static T CloneAsJson<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static T GetPropertyValue<T>(this object objectSource, string propertyName)
        {
            return (T)objectSource.GetType().GetProperty(propertyName).GetValue(objectSource, null);
        }

        public static string GetMethodName<T, TDelegate>(this Expression<Func<T, TDelegate>> handlerName)
            where TDelegate : Delegate
        {
            var unaryExpression = (UnaryExpression)handlerName.Body;
            var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;
            var constantExpression = (ConstantExpression)methodCallExpression.Object;
            var methodInfoExpression = (MethodInfo)constantExpression.Value;
            return methodInfoExpression.Name;
        }

        public static string GetFieldName<T, TResult>(this Expression<Func<T, TResult>> handlerName)
        {
            var unaryExpression = (MemberExpression)handlerName.Body;
            var name = unaryExpression.Member.Name;
            return name;
        }

        public static T Clone<T>(this object item)
        {
            if (item != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();

                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);

                T result = (T)formatter.Deserialize(stream);
                stream.Close();

                return result;
            }
            else
                return default(T);
        }

        public static T ToObject<T>(this ExpandoObject source)
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            var objType = obj.GetType();
            foreach (var item in source)
                objType.GetProperty(item.Key).SetValue(obj, item.Value);
            return obj;
        }

        public static Dictionary<String, object> AsDictionary(this object obj, BindingFlags bindingAttr = BindingFlags.DeclaredOnly
                                                                                | BindingFlags.Public | BindingFlags.Instance)
        {
            return obj.GetType().GetProperties(bindingAttr).ToDictionary(x => x.Name, x => x.GetValue(obj, null));
        }

        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static T1 CopyFrom<T1, T2>(this T1 destinationObject, T2 sourceObject) where T1 : class
            where T2 : class
        {
            PropertyInfo[] srcFields =
                sourceObject.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            PropertyInfo[] destFields =
                destinationObject.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.CanWrite)
                    dest.SetValue(destinationObject, property.GetValue(sourceObject, null), null);
            }

            return destinationObject;
        }

        public static List<TSource> EnumerableToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new Exception("Source is null");

            return new List<TSource>(source);
        }

        public static void WhenSome<T>(this T target, Action<T> action)
        {
            if (target != null)
            {
                action(target);
            }
        }
    }
}
