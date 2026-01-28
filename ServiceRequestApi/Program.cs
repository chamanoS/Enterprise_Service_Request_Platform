
using Microsoft.EntityFrameworkCore;
using ServiceRequestApi.Infrastructure.Data;
using ServiceRequestApi.Services.Users;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    ReferenceDataSeeder.Seed(context);
}


app.Run();
