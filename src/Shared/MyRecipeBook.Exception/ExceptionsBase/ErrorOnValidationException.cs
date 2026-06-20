using System.Net;

namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : MyRecipeBookException
{
    private readonly List<string> _errors = errorMessages;

    public override List<string> GetErrorMessages() => _errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
