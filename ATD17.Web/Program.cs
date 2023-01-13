using ATD17.Web.Services;
using ATD17.Web.Data;
using ATD17.Web.Filters;
using ATD17.Web.Interfaces;

var builder = WebApplication.CreateBuilder(args);

if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        // Setup a HTTP/2 endpoint without TLS.
        options.ListenLocalhost(5178, o => o.Protocols = HttpProtocols.Http1); //http
        options.ListenLocalhost(5179, o => o.Protocols = HttpProtocols.Http2); //grpc
    });
}

builder.Services.AddGrpc();
builder.Services.AddControllers(options => options.Filters.Add(new ExceptionsFilter()));
builder.Services.AddSingleton<ICountryService, CountryService>();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("1.0", new OpenApiInfo { Title = "ATD Api", Version = "v1.0" });
});

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GreeterService>();
    endpoints.MapGrpcService<CountryService>();
});
app.UseSwagger();
app.UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint("swagger/1.0/swagger.json", "v1.0");
    setup.RoutePrefix = string.Empty;
});

FakeCountries.Initialize();

app.Run();