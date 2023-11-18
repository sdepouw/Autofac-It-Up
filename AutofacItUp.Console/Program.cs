using AutofacItUp.Console;
using AutofacItUp.SomeLib;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.RegisterDependencies();
builder.Services.AddWindowsService();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
