namespace Domain.Expressions
{
    public abstract record ParameterExpression(string Name) : Expression
    {
        public T Match<T>(Func<StringParameterExpression, T> str, Func<IntegerParameterExpression, T> integer,
            Func<BooleanParameterExpression, T> boolean)
        {
            return this switch
            {
                StringParameterExpression x => str(x),
                IntegerParameterExpression x => integer(x),
                BooleanParameterExpression x => boolean(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public record StringParameterExpression(string Name) : ParameterExpression(Name);
    public record IntegerParameterExpression(string Name) : ParameterExpression(Name);
    public record BooleanParameterExpression(string Name) : ParameterExpression(Name);
}