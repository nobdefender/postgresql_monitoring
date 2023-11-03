using Monitoring.Postgresql.Registrars;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Logic.Registars;

var builder = WebApplication.CreateBuilder(args);
var conf = builder.Configuration;
builder.Services.RegisterDbServices();

builder.Services.AddSwaggerGen();

builder.Services.RegisterOptions(conf);
builder.Services.RegisterProvider();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTransient<IUserProvider, UserProvider>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<MonitoringServiceDbContext>();
    context?.Database.Migrate();
}

app.Run();