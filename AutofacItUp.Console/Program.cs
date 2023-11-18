using AutofacItUp.Console;
using AutofacItUp.SomeLib;
using Serilog;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.RegisterDependencies();
builder.Services.AddSerilog(config => config.ReadFrom.Configuration(builder.Configuration));
builder.Services.AddWindowsService();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
