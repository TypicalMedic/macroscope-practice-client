using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseContentRoot(Environment.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) => cfg
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)                )
                .ConfigureServices(App.ConfigureServices);
        }
    }
}
