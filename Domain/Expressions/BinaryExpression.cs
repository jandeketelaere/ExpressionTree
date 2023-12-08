using Domain.Visitors;

namespace Domain.Expressions
{
    public enum BinaryExpressionType
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Contains
    }

    public record BinaryExpression(BinaryExpressionType Type, Expression Left, Expression Right) : Expression
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}