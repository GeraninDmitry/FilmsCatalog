using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Helpers;

namespace FilmsCatalog
{
    public class Program
    {

        public static void Main(string[] args)
		{
			var webHost = new WebHostBuilder().UseIIS().UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables();
				}).ConfigureLogging((builderContext, loggingBuilder) =>
				{
					loggingBuilder.AddSeq(builderContext.Configuration.GetSection("Seq"));
					loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
				}).UseStartup<Startup>().Build()
#if DEBUG
				.FillDb()
				.AddDefaultUser();
#else
            ;
#endif

			webHost.Run();
        }

    }
}
