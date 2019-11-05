using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Domain.Specification
{

    public abstract class ExpressionSpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
        public ExpressionSpecification<T> And(ExpressionSpecification<T> specification) => new AndExpressionSpecification<T>(this, specification);
        public ExpressionSpecification<T> Or(ExpressionSpecification<T> specification) => new OrExpressionSpecification<T>(this, specification);
        public ExpressionSpecification<T> Not(ExpressionSpecification<T> specification) => new NotExpressionSpecification<T>(this);

        public bool IsSatisfiedBy(T candidate)
        {
            Func<T, bool> predicate = ToExpression().Compile();

            return predicate(candidate);
        }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }

    public class AndExpressionSpecification<T> : ExpressionSpecification<T>
    {
        private readonly ExpressionSpecification<T> _left;
        private readonly ExpressionSpecification<T> _right;

        public AndExpressionSpecification(ExpressionSpecification<T> left, ExpressionSpecification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }

    public class NotExpressionSpecification<T> : ExpressionSpecification<T>
    {
        private readonly ExpressionSpecification<T> _left;

        public NotExpressionSpecification(ExpressionSpecification<T> left)
        {
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();

            var notExpression = Expression.Not(leftExpression.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(notExpression, leftExpression.Parameters.Single());

            return lambda;
        }
    }

    public class OrExpressionSpecification<T> : ExpressionSpecification<T>
    {
        private readonly ExpressionSpecification<T> _left;
        private readonly ExpressionSpecification<T> _right;

        public OrExpressionSpecification(ExpressionSpecification<T> left, ExpressionSpecification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }


    public sealed class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(parameter);

        internal ParameterReplacer(ParameterExpression parameter)
        {
            this.parameter = parameter;
        }
    }
}
