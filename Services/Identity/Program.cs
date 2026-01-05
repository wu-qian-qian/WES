var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}
app.Run();
