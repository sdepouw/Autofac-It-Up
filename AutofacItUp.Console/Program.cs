using AutofacItUp.Console;
using AutofacItUp.SomeLib;
using Refit;
using Serilog;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.RegisterDependencies();
builder.Services.AddSerilog(config => config.ReadFrom.Configuration(builder.Configuration));
builder.Services.AddWindowsService();
builder.Services.AddHostedService<Worker>();
builder.Services.AddRefitClient<ICatFactsClient>().ConfigureHttpClient(CatFactsConfig);

IHost host = builder.Build();
host.ValidateSerilog();
host.Run();

// .NET 7 Example
// IHostBuilder builder = Host.CreateDefaultBuilder();
// builder.RegisterDependencies();
// builder.ConfigureServices((hostBuilderContext, services) =>
// {
//   services.AddSerilog(config => config.ReadFrom.Configuration(hostBuilderContext.Configuration));
//   services.AddWindowsService();
//   services.AddHostedService<Worker>();
//   services.AddRefitClient<ICatFactsClient>().ConfigureHttpClient(CatFactsConfig);
// });
// var otherHost = builder.Build();
// otherHost.Run();

void CatFactsConfig(HttpClient c)
{
    c.BaseAddress = new Uri("https://cat-fact.herokuapp.com");
    c.Timeout = TimeSpan.FromSeconds(3);
}
