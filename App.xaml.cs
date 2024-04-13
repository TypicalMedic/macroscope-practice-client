using ClientSide.Data.FileStorage;
using ClientSide.Data.Interfaces;
using ClientSide.PalindromeValidator.FromServer;
using ClientSide.PalindromeValidator.Interfaces;
using ClientSide.Services;
using ClientSide.Services.Interfaces;
using ClientSide.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly.Extensions.Http;
using Polly;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using Polly.Contrib.WaitAndRetry;

namespace ClientSide
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static TimeSpan httpHandlerLifetime = TimeSpan.FromMinutes(10);
        private static TimeSpan requestRetryTimeStart = TimeSpan.FromSeconds(1);
        public static bool IsDesignMode { get; private set; } = true;
        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }

        private static IHost? _Host;
        public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            string? serverUrl = ConfigurationManager.AppSettings.Get("server_url");
            if (!IsDesignMode && serverUrl == null)
            {
                MessageBox.Show("Ошибка запуска приложения: параметр server_url не установлен.", "Ошибка");
                Environment.Exit(1);
            }
            services.AddHttpClient<IPalindromeValidator, PalindromeValidatorFromServer>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(serverUrl))
                .SetHandlerLifetime(httpHandlerLifetime)
                .AddPolicyHandler(GetRetryPolicy());
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IPalindromeService, PalindromeService>();
            services.AddTransient<IData, FileStorage>();
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                .WaitAndRetryForeverAsync(attempt => requestRetryTimeStart+TimeSpan.FromSeconds(attempt));
        }
    }

}
