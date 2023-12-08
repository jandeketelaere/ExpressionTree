using Domain;
using Domain.Expressions;

namespace Example.Tests
{
    public class Playground
    {
        [Fact]
        public void Test()
        {
            var parameter = Expression.StringParameter("FirstName");
            var constant = Expression.StringConstant("NotJan");
            var expression = Expression.NotEqual(parameter, constant);

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