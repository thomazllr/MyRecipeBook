namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : MyRecipeBookException
{
    private readonly List<string> _errors = errorMessages;

    public List<string> GetErrorMessages() 
    {
        return _errors;
    }
}
