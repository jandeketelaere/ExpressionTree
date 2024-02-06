using Domain;

namespace Example.Tests
{
    public class Playground
    {
        [Fact]
        public void Test()
        {
            var leftExpression = Expression.Equal(Expression.StringParameter("FirstName"), Expression.StringConstant("Jan"));
            var rightExpression = Expression.Equal(Expression.StringListParameter("Names"), Expression.StringListConstant(new[] { "Deketelaere" }));
            var andExpression = Expression.And(leftExpression, rightExpression);

            var expression = Expression.IfThenElse(andExpression, ifTrue: Expression.StringConstant("Ja"), ifFalse: Expression.StringConstant("Nee"));

            var interpreter = new ExpressionInterpreter(new ParameterValueTestProvider());

            var result = interpreter.Interpret(expression);
        }
    }

    public class ParameterValueTestProvider : IParameterValueProvider
    {
        public object GetParameterValue(string parameterName)
        {
            if (parameterName == "FirstName")
                return "Jan";

            if (parameterName == "LastName")
                return "Deketelaere";

            if (parameterName == "Names")
                return new[] { "Deketelaere" };

            throw new NotImplementedException();
        }
    }
}