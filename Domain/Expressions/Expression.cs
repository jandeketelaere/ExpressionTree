using Domain.Visitors;

namespace Domain.Expressions
{
    public abstract record Expression
    {
        public static Expression Constant(object value)
            => new ConstantExpression(value);

        public static Expression Parameter(string name)
            => new ParameterExpression(name);

        public static Expression Equal(Expression left, Expression right)
            => new BinaryExpression(BinaryExpressionType.Equal, left, right);

        public static Expression Not(Expression expression)
            => new UnaryExpression(UnaryExpressionType.Not, expression);

        public static Expression Return(Expression expression)
            => new ReturnExpression(expression);

        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}