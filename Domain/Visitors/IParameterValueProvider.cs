namespace Domain.Visitors
{
    public interface IParameterValueProvider
    {
        object GetParameterValue(string parameterName);
    }
}