using Domain.Expressions;

namespace Domain.Visitors
{
    public class ParameterValueProviderExpressionVisitor(IParameterValueProvider parameterValueProvider) : ExpressionVisitor<object>
    {
        private readonly IParameterValueProvider _parameterValueProvider = parameterValueProvider;

        public override object Visit(ConstantExpression expression)
        {
            return expression.Value;
        }

        public override object Visit(ParameterExpression expression)
        {
            return _parameterValueProvider.GetParameterValue(expression.Name);
        }

        public override object Visit(BinaryExpression expression)
        {
            var left = Visit(expression.Left);
            var right = Visit(expression.Right);

            return expression.Type switch
            {
                BinaryExpressionType.Equal => Equals(left, right),
                BinaryExpressionType.NotEqual => !Equals(left, right),
                BinaryExpressionType.LessThan => throw new NotImplementedException(),
                BinaryExpressionType.LessThanOrEqual => throw new NotImplementedException(),
                BinaryExpressionType.GreaterThan => throw new NotImplementedException(),
                BinaryExpressionType.GreaterThanOrEqual => throw new NotImplementedException(),
                BinaryExpressionType.Contains => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        public override object Visit(UnaryExpression expression)
        {
            var result = Visit(expression);

            return expression.Type switch
            {
                UnaryExpressionType.Not => !(bool)result,
                _ => throw new NotImplementedException(),
            };
        }

        public override object Visit(ReturnExpression expression)
        {
            return Visit(expression.Expression);
        }
    }
}