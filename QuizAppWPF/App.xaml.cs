using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizAppWPF.Services.Api;
using QuizAppWPF.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var maimWindow = ServiceProvider.GetRequiredService<LoginView>();
            maimWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            var ApiBaseUrl = "https://localhost:7024";

            services.AddRefitClient<IUserApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiBaseUrl));

            services.AddSingleton<LoginView>();

        }
    }
}
