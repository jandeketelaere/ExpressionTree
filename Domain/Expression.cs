namespace Domain
{
    public record StringConstantExpression(string Value) : ConstantExpression;
    public record IntegerConstantExpression(int Value) : ConstantExpression;
    public record BooleanConstantExpression(bool Value) : ConstantExpression;
    public record StringListConstantExpression(IList<string> Value) : ConstantExpression;
    public record StringParameterExpression(string Name) : ParameterExpression(Name);
    public record IntegerParameterExpression(string Name) : ParameterExpression(Name);
    public record BooleanParameterExpression(string Name) : ParameterExpression(Name);
    public record StringListParameterExpression(string Name) : ParameterExpression(Name);
    public record EqualExpression(Expression Left, Expression Right) : Expression;
    public record NotEqualExpression(Expression Left, Expression Right) : Expression;
    public record NotExpression(Expression Expression) : Expression;
    public record IfThenElseExpression(Expression Test, Expression IfTrue, Expression IfFalse) : Expression;
    public record AndExpression(Expression Left, Expression Right) : Expression;
    public record OrExpression(Expression Left, Expression Right) : Expression;

    public abstract record Expression
    {
        public static Expression StringConstant(string value)
            => new StringConstantExpression(value);

        public static Expression IntegerConstant(int value)
            => new IntegerConstantExpression(value);

        public static Expression BooleanConstant(bool value)
            => new BooleanConstantExpression(value);

        public static Expression StringListConstant(IList<string> value)
            => new StringListConstantExpression(value);

        public static Expression StringParameter(string name)
            => new StringParameterExpression(name);

        public static Expression IntegerParameter(string name)
            => new IntegerParameterExpression(name);

        public static Expression BooleanParameter(string name)
            => new BooleanParameterExpression(name);

        public static Expression StringListParameter(string name)
            => new StringListParameterExpression(name);

        public static Expression Equal(Expression left, Expression right)
            => new EqualExpression(left, right);

        public static Expression NotEqual(Expression left, Expression right)
            => new NotEqualExpression(left, right);

        public static Expression Not(Expression expression)
            => new NotExpression(expression);

        public static Expression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
            => new IfThenElseExpression(test, ifTrue, ifFalse);

        public static Expression And(Expression left, Expression right)
            => new AndExpression(left, right);

        public static Expression Or(Expression left, Expression right)
            => new OrExpression(left, right);

        public T Match<T>(Func<ConstantExpression, T> constant, Func<ParameterExpression, T> parameter, Func<EqualExpression, T> equal,
            Func<NotEqualExpression, T> notEqual, Func<NotExpression, T> not, Func<IfThenElseExpression, T> ifThenElse, Func<AndExpression, T> and, Func<OrExpression, T> or)
        {
            return this switch
            {
                ConstantExpression x => constant(x),
                ParameterExpression x => parameter(x),
                EqualExpression x => equal(x),
                NotEqualExpression x => notEqual(x),
                NotExpression x => not(x),
                IfThenElseExpression x => ifThenElse(x),
                AndExpression x => and(x),
                OrExpression x => or(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public abstract record ConstantExpression : Expression
    {
        public T Match<T>(Func<StringConstantExpression, T> str,
            Func<IntegerConstantExpression, T> integer,
            Func<BooleanConstantExpression, T> boolean,
            Func<StringListConstantExpression, T> strList)
        {
            return this switch
            {
                StringConstantExpression x => str(x),
                IntegerConstantExpression x => integer(x),
                BooleanConstantExpression x => boolean(x),
                StringListConstantExpression x => strList(x),
                _ => throw new NotImplementedException(),
            };
        }
    }

    public abstract record ParameterExpression(string Name) : Expression
    {
        public T Match<T>(Func<StringParameterExpression, T> str,
            Func<IntegerParameterExpression, T> integer,
            Func<BooleanParameterExpression, T> boolean,
            Func<StringListParameterExpression, T> strList)
        {
            return this switch
            {
                StringParameterExpression x => str(x),
                IntegerParameterExpression x => integer(x),
                BooleanParameterExpression x => boolean(x),
                StringListParameterExpression x => strList(x),
                _ => throw new NotImplementedException(),
            };
        }
    }
}