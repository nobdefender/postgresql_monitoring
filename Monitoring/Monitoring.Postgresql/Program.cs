using Monitoring.Postgresql.Registrars;

var builder = WebApplication.CreateBuilder(args);
var conf = builder.Configuration;

builder.Services.AddSwaggerGen();

builder.Services.RegisterOptions(conf);
builder.Services.RegisterProvider();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
