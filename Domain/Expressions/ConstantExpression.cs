namespace Domain.Expressions
{
    public abstract record ConstantExpression : Expression
    {
        public T Match<T>(Func<StringConstantExpression, T> str, Func<IntegerConstantExpression, T> integer, Func<BooleanConstantExpression, T> boolean)
        {
            return this switch
            {
                StringConstantExpression x => str(x),
                IntegerConstantExpression x => integer(x),
                BooleanConstantExpression x => boolean(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public record StringConstantExpression(string Value) : ConstantExpression;
    public record IntegerConstantExpression(int Value) : ConstantExpression;
    public record BooleanConstantExpression(bool Value) : ConstantExpression;
}