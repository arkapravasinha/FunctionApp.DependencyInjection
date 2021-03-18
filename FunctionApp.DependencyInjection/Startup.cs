using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

[assembly: FunctionsStartup(typeof(FunctionApp.DependencyInjection.Startup))]
namespace FunctionApp.DependencyInjection
{
    public class Startup : FunctionsStartup
    {

        //override ConfigurationBuilder Settings if you want
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            //Overrideden behavior read from, appsettings.json
            FunctionsHostBuilderContext context = builder.GetContext();
            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: false, reloadOnChange: true)
                //Adding Environmental Files //add Conditions
                //.AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            //Default behavior read from local.settings.json
            //base.ConfigureAppConfiguration(builder);
        }

        //Configure services
        public override void Configure(IFunctionsHostBuilder builder)
        {

            //add dependencies examples i.e. 
            //builder.Services.AddSingleton<IMyService, MyService>();
            //builder.Services.AddTransient<IMyService, MyService>();
            //builder.Services.AddScoped<IMyService, MyService>();

            //Extended Dependencies
            //part of Microsoft.Extensions.Http
            //builder.Services.AddHttpClient();


            //You can access configurations by accessing
            //var configuration = builder.GetContext().Configuration;
            //var connectionStrings = configuration.GetConnectionString("Default");

            //Don't add AddApplicationInsightsTelemetry() to the services collection,
            //which registers services that conflict with services provided by the environment.
            //Don't register your own TelemetryConfiguration or TelemetryClient if you are using
            //the built-in Application Insights functionality. If you need to configure your own TelemetryClient instance,
            //create one via the injected TelemetryConfiguration as shown in Log custom telemetry in C# functions.
            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
