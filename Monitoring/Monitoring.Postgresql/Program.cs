using System.Text;
using Monitoring.Postgresql.Registrars;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Logic.Registars;
using Monitoring.Postgresql.Providers.Implementations;
using Monitoring.Postgresql.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Monitoring.Postgresql.Extensions;

var builder = WebApplication.CreateBuilder(args);
var conf = builder.Configuration;
builder.Services.RegisterDbServices();

builder.Services.AddSwaggerGen();

builder.Services.RegisterOptions(conf);
builder.Services.RegisterProvider();
builder.Services.RegisterMapperServices();

builder.Services.AddSettings(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTransient<IUserProvider, UserProvider>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});


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