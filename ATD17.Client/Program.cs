using ATD17.Client;
using ATD17.net7.Web;

if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
}

var channel = GrpcChannel.ForAddress("http://localhost:5179");
var client = new Country.CountryClient(channel);

var countriesResponse = await client.GetCountriesAsync(new Empty());
Console.WriteLine(JsonSerializer.Serialize(countriesResponse));
Console.WriteLine("*******************************************");

var request = new CountryRequest {Id = 1};
var countryResponse = await client.GetCountryAsync(request);
Console.WriteLine(JsonSerializer.Serialize(countryResponse));
Console.WriteLine("*******************************************");

using var streamCountriesCall = client.GetStreamCountries(new Empty());
await foreach (var response in streamCountriesCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(JsonSerializer.Serialize(response));
}
Console.WriteLine("*******************************************");




















//.NET 7
//
// var channel = GrpcChannel.ForAddress("http://localhost:5283");
// var client = new Greeter.GreeterClient(channel);
//
// var greetResponse = await client.SayHelloAsync(new HelloRequest { Name = "ATD" });
// Console.WriteLine(JsonSerializer.Serialize(greetResponse));























//Client stream

// using var clientStreamingCall = client.GetClientStreamCountry();
// for (int i = 1; i <= 5; i++)
// {
//     await clientStreamingCall.RequestStream.WriteAsync(new CountryRequest
//     {
//         Id = i,
//     });
// }
//
// await clientStreamingCall.RequestStream.CompleteAsync();
// var clientStreamingResponse = await clientStreamingCall;
// Console.WriteLine(JsonSerializer.Serialize(clientStreamingResponse));
Console.ReadLine();
