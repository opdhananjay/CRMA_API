using APPC.Middleware;
using APPC.Repositories;
using APPC.Repositories.Interfaces;
using APPC.Services;
using Serilog;

Log.Logger = new LoggerConfiguration().
    WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

try
{

    Log.Information("Application is starting");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    
    builder.Services.AddTransient<IPasswordHelperService, PasswordHelperService>();
    
    builder.Services.AddScoped<IAuthRepository, AuthRepository>();



    // End services



    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    // CORS : 
    builder.Services.AddCors(option =>
    {
        option.AddPolicy("AllowAll", policy =>
        {
            policy.WithOrigins("*")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors("AllowAll");

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}

