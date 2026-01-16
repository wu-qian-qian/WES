using Common.AspNetCore;
using Common.Presentation;
using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.AddAspNetCore();
builder.Services.AddInfranstructureConfiguration(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();
app.Run();