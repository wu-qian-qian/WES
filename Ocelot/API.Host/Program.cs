using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateSlimBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseOcelot().Wait();

app.Run();


