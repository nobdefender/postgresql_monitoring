using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Logic.Registars;
using Monitoring.Postgresql.Providers.Implementations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterDbServices();
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