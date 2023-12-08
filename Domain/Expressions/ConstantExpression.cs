using Domain.Visitors;

namespace Domain.Expressions
{
    public record ConstantExpression(object Value) : Expression
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}