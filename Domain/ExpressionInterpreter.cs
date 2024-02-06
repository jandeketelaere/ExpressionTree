namespace Domain
{
    public interface IExpressionInterpreter
    {
        Expression Interpret(Expression expression);
    }

    public class ExpressionInterpreter(IParameterValueProvider parameterValueProvider) : IExpressionInterpreter
    {
        private readonly IParameterValueProvider _parameterValueProvider = parameterValueProvider;

        public Expression Interpret(Expression expression)
        {
            return expression.Match
            (
                Constant,
                Parameter,
                Equal,
                NotEqual,
                Not,
                IfThenElse
            );
        }

        private Expression IfThenElse(IfThenElseExpression expression)
        {
            var test = (BooleanConstantExpression)Interpret(expression.Test);

            if (test.Value)
                return Interpret(expression.IfTrue);

            return Interpret(expression.IfFalse);
        }

        private Expression Constant(ConstantExpression expression)
        {
            return expression;
        }

        private Expression Parameter(ParameterExpression expression)
        {
            var parameter = _parameterValueProvider.GetParameterValue(expression.Name);

            return expression.Match
            (
                str => Expression.StringConstant((string) parameter),
                integer => Expression.IntegerConstant((int) parameter),
                boolean => Expression.BooleanConstant((bool) parameter)
            );
        }

        private Expression Equal(EqualExpression expression)
        {
            var left = (ConstantExpression)Interpret(expression.Left);
            var right = (ConstantExpression)Interpret(expression.Right);

            return (left, right) switch
            {
                (StringConstantExpression l, StringConstantExpression r) => Expression.BooleanConstant(l.Value == r.Value),
                (IntegerConstantExpression l, IntegerConstantExpression r) => Expression.BooleanConstant(l.Value == r.Value),
                (BooleanConstantExpression l, BooleanConstantExpression r) => Expression.BooleanConstant(l.Value == r.Value),
                _ => throw new NotImplementedException(),
            };
        }

        private Expression NotEqual(NotEqualExpression expression)
        {
            return Interpret(Expression.Not(Expression.Equal(expression.Left, expression.Right)));
        }

        private Expression Not(NotExpression expression)
        {
            var result = (BooleanConstantExpression)Interpret(expression.Expression);

            return Expression.BooleanConstant(!result.Value);
        }
    }
}