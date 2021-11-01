using FluentValidation.Results;
using System;
using System.Linq.Expressions;

namespace NetDevPack.SpecificationResult
{
    internal sealed class XorSpecificationValidator<T> : SpecificationValidator<T>
    where T : class
    {
        private readonly SpecificationValidator<T> _left;
        private readonly SpecificationValidator<T> _right;

        public XorSpecificationValidator(SpecificationValidator<T> left, SpecificationValidator<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, ValidationResult, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, ValidationResult, bool>>)Expression.Lambda(Expression.ExclusiveOr(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }
}
