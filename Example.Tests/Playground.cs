using Domain;

namespace Example.Tests
{
    public class Playground
    {
        [Fact]
        public void Test()
        {
            var parameter = Expression.StringParameter("FirstName");
            var constant = Expression.StringConstant("Jan");
            var equal = Expression.Equal(parameter, constant);
            var expression = Expression.IfThenElse(equal, Expression.StringConstant("Ja"), Expression.StringConstant("Nee"));

            var interpreter = new ExpressionInterpreter(new ParameterValueProvider());

            var result = interpreter.Interpret(expression);
        }
    }

    public class ParameterValueProvider : IParameterValueProvider
    {
        public object GetParameterValue(string parameterName)
        {
            if (parameterName == "FirstName")
                return "Jan";

            return null;
        }
    }
}