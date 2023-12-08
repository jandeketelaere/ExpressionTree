﻿using Domain.Expressions;

namespace Domain.Visitors
{
    public interface IExpressionVisitor<T>
    {
        T Visit(Expression expression);
        T Visit(ConstantExpression expression);
        T Visit(ParameterExpression expression);
        T Visit(BinaryExpression expression);
        T Visit(UnaryExpression expression);
        T Visit(ReturnExpression expression);
    }
}