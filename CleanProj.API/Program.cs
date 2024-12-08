using Azure.Identity;
using CleanProj.API;
using CleanProj.API.Configuration;
using CleanProj.API.Filters;
using CleanProj.Domain.Identity;
using CleanProj.Persistence.EntityFramework.Contexts;
using CleanProj.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDatabaseConfiguration(builder.Configuration);
    builder.Host.UseSerilog();
    

  builder.Services.AddWebApi(builder.Configuration);
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    });
    builder.Services.AddFluentValidation();
    builder.Services.AddOpenApi();
    builder.Services.AddIdentityConfiguration();
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        //app.MapScalarApiReference();
        app.UseDeveloperExceptionPage();
        app.UseSwaggerWithVersion();
    }else
    {
        app.UseHsts();

    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
  //  app.UseSwaggerWithVersion();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}