using Domain.Expressions;

namespace Example.Tests
{
    public class Playground
    {
        [Fact]
        public void Test()
        {
            var parameter = Expression.Parameter("FirstName");
            var constant = Expression.Constant("Jan");
            var expression = Expression.Equal(parameter, constant);
        }
    }
}