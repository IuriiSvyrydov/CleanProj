namespace CleanProj.Domain.ValueObjects;

public sealed record ValidationError
{
    public ValidationError(string propertyName, List<string> errorMessages)
    {
        PropertyName = propertyName;
        ErrorMessages = errorMessages;
    }
    public ValidationError(string propertyName, string errorMessages)
                    : this(propertyName, new List<string>
                    {
                        errorMessages
                    })
    {
    }
    public string PropertyName { get; init; }
    public List<string> ErrorMessages { get; init; }
}