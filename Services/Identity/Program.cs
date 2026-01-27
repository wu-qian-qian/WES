using Common.AspNetCore;
using Common.Presentation;
using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
//NetCore
builder.AddAspNetCore();
//基础设施
builder.Services.AddInfranstructureConfiguration(builder.Configuration);
//表示层
builder.Services.AddEndpoints(typeof(Identity.Presentation.AssemblyReference).Assembly);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();
app.Run();