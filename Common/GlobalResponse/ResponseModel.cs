namespace Common.GlobalResponse;

public class ResponseModel
{
    public bool isSuccess { get; set; }
    public List<string>? Errors { get; set; }

    public ResponseModel(List<string> messages)
    {
        Errors = messages;
        isSuccess = false;
    }

    public ResponseModel()
    {
        isSuccess = true;
        Errors = null;
    }
}
