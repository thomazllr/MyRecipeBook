namespace MyRecipeBook.Exception.Response;

public class ResponseErrorJson
{
    public List<string> Errors { get; private set; }

    public ResponseErrorJson(List<string> errosMessages)
    {
        Errors = errosMessages;
    }

    public ResponseErrorJson(string erroMessage)
    {
        Errors = [erroMessage];
    }
}
