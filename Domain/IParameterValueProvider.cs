namespace Domain
{
    public interface IParameterValueProvider
    {
        object GetParameterValue(string parameterName);
    }
}