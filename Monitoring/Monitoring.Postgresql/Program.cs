using Monitoring.Postgresql.Registrars;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Logic.Registars;
using Monitoring.Postgresql.Providers.Implementations;

var builder = WebApplication.CreateBuilder(args);
var conf = builder.Configuration;
builder.Services.RegisterDbServices();

builder.Services.AddSwaggerGen();

builder.Services.RegisterOptions(conf);
builder.Services.RegisterProvider(conf);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTransient<IUserProvider, UserProvider>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
}

//using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
//{
//    var context = serviceScope.ServiceProvider.GetService<MonitoringServiceDbContext>();
//    context?.Database.Migrate();
//}

app.Run();