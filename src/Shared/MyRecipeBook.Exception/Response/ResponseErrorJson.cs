namespace MyRecipeBook.Exception.Response;

public class ResponseErrorJson
{
    public List<string> Erros { get; private set; }

    public ResponseErrorJson(List<string> errosMessages)
    {
        Erros = errosMessages;
    }

    public ResponseErrorJson(string erroMessage)
    {
        Erros = [erroMessage];
    }
}
