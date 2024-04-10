namespace Core.Helpers;

public class Response
{
    public bool IsOk { get; }
    public bool IsNotFound { get; }
    public bool IsBadRequest { get; }
    public object? Content { get; set; }

    public Response(object? content, bool isOk, bool isNotFound, bool badRequest)
    {
        Content = content;
        IsOk = isOk;
        IsNotFound = isNotFound;
        IsBadRequest = badRequest;
    }

    //public static Response Ok() => new (null, true, false, false);
    public static Response Ok(object? content = null) => new (content, true, false, false);
    public static Response NotFound(object? content) => new (content, false, true, false);
    public static Response BadRequest() => new (null, false, false, true);
}
