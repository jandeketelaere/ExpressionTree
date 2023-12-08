using Domain.Expressions;

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
                Binary,
                Unary,
                Return
            );
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

        private Expression Binary(BinaryExpression expression)
        {
            return expression.Match
            (
                Equal,
                NotEqual
            );
        }

        private Expression Equal(EqualBinaryExpression expression)
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

        private Expression NotEqual(NotEqualBinaryExpression expression)
        {
            return Interpret(Expression.Not(Expression.Equal(expression.Left, expression.Right)));
        }

        private Expression Unary(UnaryExpression expression)
        {
            return expression.Match
            (
                Not
            );
        }
        
        private Expression Not(NotUnaryExpression expression)
        {
            var result = (ConstantExpression)Interpret(expression.Expression);

            return result switch
            {
                BooleanConstantExpression x => Expression.BooleanConstant(!x.Value),
                _ => throw new NotImplementedException()
            };
        }

        private Expression Return(ReturnExpression expression)
        {
            return Interpret(expression.Expression);
        }
    }
}