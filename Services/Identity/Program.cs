using Common.AspNetCore;
using Identity.Infrastructure;

var builder = WebApplication.CreateSlimBuilder(args);
builder.AddAspNetCore();
builder.Services.AddInfranstructureConfiguration();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}
app.Run();
