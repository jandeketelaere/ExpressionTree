using Domain.Visitors;

namespace Domain.Expressions
{
    public record ParameterExpression(string Name) : Expression
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}