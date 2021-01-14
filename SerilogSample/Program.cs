using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

namespace SerilogSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(".");
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "logs/_logs.txt");
            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .Enrich.With(new Enricher())
                            .WriteTo.File(new CompactJsonFormatter(), dir)
                            .WriteTo.Seq("http://localhost:5341")
                            .CreateLogger();
            try
            {
                Log.Information($"application starting up - {DateTime.Now}");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception es)
            {
                Log.Fatal($"{es.Message} - {DateTime.Now}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}