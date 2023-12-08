using Domain.Visitors;

namespace Domain.Expressions
{
    public enum UnaryExpressionType
    {
        Not
    }

    public record UnaryExpression(UnaryExpressionType Type, Expression Expression) : Expression
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}