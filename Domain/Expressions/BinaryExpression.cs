namespace Domain.Expressions
{
    //public enum BinaryExpressionType
    //{
    //    Equal,
    //    //NotEqual,
    //    //LessThan,
    //    //LessThanOrEqual,
    //    //GreaterThan,
    //    //GreaterThanOrEqual,
    //    //Contains
    //}

    public abstract record BinaryExpression(Expression Left, Expression Right) : Expression
    {
        public T Match<T>(Func<EqualBinaryExpression, T> equal, Func<NotEqualBinaryExpression, T> notEqual)
        {
            return this switch
            {
                EqualBinaryExpression x => equal(x),
                NotEqualBinaryExpression x => notEqual(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public record EqualBinaryExpression(Expression Left, Expression Right) : BinaryExpression(Left, Right);
    public record NotEqualBinaryExpression(Expression Left, Expression Right) : BinaryExpression(Left, Right);
}