namespace Domain
{
    public record StringConstantExpression(string Value) : ConstantExpression;
    public record IntegerConstantExpression(int Value) : ConstantExpression;
    public record BooleanConstantExpression(bool Value) : ConstantExpression;
    public record StringParameterExpression(string Name) : ParameterExpression(Name);
    public record IntegerParameterExpression(string Name) : ParameterExpression(Name);
    public record BooleanParameterExpression(string Name) : ParameterExpression(Name);
    public record EqualExpression(Expression Left, Expression Right) : Expression;
    public record NotEqualExpression(Expression Left, Expression Right) : Expression;
    public record NotExpression(Expression Expression) : Expression;
    public record IfThenElseExpression(Expression Test, Expression IfTrue, Expression IfFalse) : Expression;

    public abstract record Expression
    {
        public static Expression StringConstant(string value)
            => new StringConstantExpression(value);

        public static Expression IntegerConstant(int value)
            => new IntegerConstantExpression(value);

        public static Expression BooleanConstant(bool value)
            => new BooleanConstantExpression(value);

        public static Expression StringParameter(string name)
            => new StringParameterExpression(name);

        public static Expression IntegerParameter(string name)
            => new IntegerParameterExpression(name);

        public static Expression BooleanParameter(string name)
            => new BooleanParameterExpression(name);

        public static Expression Equal(Expression left, Expression right)
            => new EqualExpression(left, right);

        public static Expression NotEqual(Expression left, Expression right)
            => new NotEqualExpression(left, right);

        public static Expression Not(Expression expression)
            => new NotExpression(expression);

        public static Expression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
            => new IfThenElseExpression(test, ifTrue, ifFalse);

        public T Match<T>(Func<ConstantExpression, T> constant, Func<ParameterExpression, T> parameter, Func<EqualExpression, T> equal,
            Func<NotEqualExpression, T> notEqual, Func<NotExpression, T> not, Func<IfThenElseExpression, T> ifThenElse)
        {
            return this switch
            {
                ConstantExpression x => constant(x),
                ParameterExpression x => parameter(x),
                EqualExpression x => equal(x),
                NotEqualExpression x => notEqual(x),
                NotExpression x => not(x),
                IfThenElseExpression x => ifThenElse(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

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
}