namespace Domain.Expressions
{
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
            => new EqualBinaryExpression(left, right);

        public static Expression NotEqual(Expression left, Expression right)
            => new NotEqualBinaryExpression(left, right);

        public static Expression Not(Expression expression)
            => new NotUnaryExpression(expression);

        public static Expression Return(Expression expression)
            => new ReturnExpression(expression);

        public T Match<T>(Func<ConstantExpression, T> constant, Func<ParameterExpression, T> parameter, Func<BinaryExpression, T> binary,
            Func<UnaryExpression, T> unary, Func<ReturnExpression, T> ret)
        {
            return this switch
            {
                ConstantExpression x => constant(x),
                ParameterExpression x => parameter(x),
                BinaryExpression x => binary(x),
                UnaryExpression x => unary(x),
                ReturnExpression x => ret(x),
                _ => throw new NotImplementedException(),
            };
        }
    }
}