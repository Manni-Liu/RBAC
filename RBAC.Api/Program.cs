using Microsoft.EntityFrameworkCore;
using RBAC.Application.Users;
using RBAC.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<RbacDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("Default")
        )
    );
});

builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapControllers();

app.Run();
