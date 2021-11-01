﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetDevPack.Specification
{
    internal sealed class XorSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public XorSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.ExclusiveOr(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }
}
