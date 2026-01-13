using Common.AspNetCore;
using Identity.Infrastructure;

var builder = WebApplication.CreateSlimBuilder(args);
builder.AddAspNetCore();
builder.Services.AddInfranstructureConfiguration(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
    var sc=app.Services.CreateScope();
    InfranstructureConfiguration.Initialize(sc);
}
app.Run();
