﻿using ClientSide.Data.FileStorage;
using ClientSide.Data.Interfaces;
using ClientSide.PalindromeValidator.FromServer;
using ClientSide.PalindromeValidator.Interfaces;
using ClientSide.Services;
using ClientSide.Services.Interfaces;
using ClientSide.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace ClientSide
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
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
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IPalindromeService, PalindromeService>();
            services.AddTransient<IData, FileStorage>();
            services.AddTransient<IPalindromeValidator, PalindromeValidatorFromServer>();
        }
    }

}
