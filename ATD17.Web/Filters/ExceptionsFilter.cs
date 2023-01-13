namespace ATD17.Web.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var contentTypes = new MediaTypeCollection {  "text/plain" };
        if (context.Exception is RpcException e)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (int)MapRpcStatusCodeToHttpStatusCode(e.StatusCode),
                ContentTypes = contentTypes
            };
        }
        else
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (int)MapExceptionToStatusCode(context.Exception),
                ContentTypes = contentTypes
            };
        }
        context.ExceptionHandled = true;
    }

    private static HttpStatusCode MapExceptionToStatusCode(Exception e) => e switch
    {
        _ => HttpStatusCode.InternalServerError
    };

    private static HttpStatusCode MapRpcStatusCodeToHttpStatusCode(StatusCode sc) => sc switch
    {
        StatusCode.InvalidArgument => HttpStatusCode.BadRequest,
        StatusCode.NotFound => HttpStatusCode.NotFound,

        _ => HttpStatusCode.InternalServerError,
    };
}