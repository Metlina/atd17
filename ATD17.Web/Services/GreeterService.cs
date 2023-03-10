namespace ATD17.Web.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Name is missing"));
        }
        
        var httpContext = context.GetHttpContext();
        var headers = context.RequestHeaders;
        
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}