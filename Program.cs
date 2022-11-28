using BlazorApp1.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
   // https://stackoverflow.com/questions/69390676/how-to-use-appsettings-json-in-asp-net-core-6-program-cs-file
   //IConfiguration configuration = new ConfigurationBuilder()
   //                         .AddJsonFile("appsettings.json")
   //                         .Build();

   var builder = WebApplication.CreateBuilder(args);

   //string sBaseUrl = builder.Configuration.GetSection("AppSettings:BaseUrl").Value;
   string sBaseUrl = builder.Configuration.GetValue<string>("AppSettings:BaseUrl");
   
   builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .ReadFrom.Configuration(ctx.Configuration));

   // Enabling Serilog's internal debug logging
   Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg)); Serilog.Debugging.SelfLog.Enable(Console.Error);

   // https://learn.microsoft.com/en-us/aspnet/core/blazor/blazor-server-ef-core?view=aspnetcore-6.0
   var cs = builder.Configuration.GetConnectionString("APP");
   //builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(cs));
   builder.Services.AddDbContext<DataContext>(o =>
   {
      o.UseSqlServer(cs);
      // https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
      // https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
      o.LogTo(msg => Debug.WriteLine(msg));
   });

   // Add services to the container.
   builder.Services.AddRazorPages();
   builder.Services.AddServerSideBlazor();
   builder.Services.AddSingleton<WeatherForecastService>();

   builder.Services.AddSingleton<ICircuitUserService, CircuitUserService>();
   builder.Services.AddScoped<CircuitHandler>((sp) => 
      new CircuitHandlerService(sp.GetRequiredService<ICircuitUserService>()));


   var app = builder.Build();

   // Configure the HTTP request pipeline.
   if (!app.Environment.IsDevelopment())
   {
      app.UseExceptionHandler("/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
   }

   app.UseHttpsRedirection();

   app.UseSerilogRequestLogging();

   app.UseStaticFiles();

   app.UseRouting();

   app.MapBlazorHub();
   app.MapFallbackToPage("/_Host");

   app.Run();
}
catch (Exception ex)
{
   Log.Fatal(ex, "Unhandled exception");
}
finally
{
   Log.Information("Shut down complete");
   Log.CloseAndFlush();
}