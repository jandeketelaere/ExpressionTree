namespace Domain.Expressions
{
    //public enum UnaryExpressionType
    //{
    //    Not
    //}

    public abstract record UnaryExpression(Expression Expression) : Expression
    {
        public T Match<T>(Func<NotUnaryExpression, T> not)
        {
            return this switch
            {
                NotUnaryExpression x => not(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public record NotUnaryExpression(Expression Expression) : UnaryExpression(Expression);
}