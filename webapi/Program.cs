using Microsoft.EntityFrameworkCore;
using webapi.Authorization;
using webapi.Healpers;
using webapi.Helpers;
using webapi.Interfaces;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;
    // Add services to the container.
    services.AddDbContext<DataContext>();

    services.AddControllers(options => {
        options.SuppressAsyncSuffixInActionNames = false;
    });
    services.AddCors();
    services.AddAutoMapper(typeof(Program));
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.Run();
