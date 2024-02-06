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
                IfThenElse,
                And
            );
        }

        private Expression And(AndExpression expression)
        {
            var left = (BooleanConstantExpression)Interpret(expression.Left);
            var right = (BooleanConstantExpression)Interpret(expression.Right);

            return Expression.BooleanConstant(left.Value && right.Value);
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
                boolean => Expression.BooleanConstant((bool) parameter),
                strList => Expression.StringListConstant((IList<string>) parameter)
            );
        }

        private Expression Equal(EqualExpression expression)
        {
            var left = (ConstantExpression)Interpret(expression.Left);
            var right = (ConstantExpression)Interpret(expression.Right);

            return left.Match
            (
                str =>
                {
                    return right switch
                    {
                        StringConstantExpression rightStr => Expression.BooleanConstant(str.Value == rightStr.Value),
                        StringListConstantExpression rightStrList => EqualStringListWithString(rightStrList.Value, str.Value),
                        _ => throw new NotImplementedException()
                    };
                },
                integer =>
                {
                    return right switch
                    {
                        IntegerConstantExpression rightInteger => Expression.BooleanConstant(integer.Value == rightInteger.Value),
                        _ => throw new NotImplementedException()
                    };
                },
                boolean =>
                {
                    return right switch
                    {
                        BooleanConstantExpression rightBoolean => Expression.BooleanConstant(boolean.Value == rightBoolean.Value),
                        _ => throw new NotImplementedException()
                    };
                },
                strList =>
                {
                    return right switch
                    {
                        StringConstantExpression rightStr => EqualStringListWithString(strList.Value, rightStr.Value),
                        StringListConstantExpression rightStrList => Expression.BooleanConstant(strList.Value.SequenceEqual(rightStrList.Value)),
                        _ => throw new NotImplementedException()
                    };
                }
            );

            static Expression EqualStringListWithString(IList<string> list, string str)
            {
                if (list.Count != 1)
                    return Expression.BooleanConstant(false);

                var firstItem = list.FirstOrDefault();
                if (firstItem == null)
                    return Expression.BooleanConstant(false);

                return Expression.BooleanConstant(firstItem == str);
            }
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