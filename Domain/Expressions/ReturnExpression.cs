using Domain.Visitors;

namespace Domain.Expressions
{
    public record ReturnExpression(Expression Expression) : Expression
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}