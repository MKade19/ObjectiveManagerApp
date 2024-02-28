using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ObjectiveManagerApp.UI.Data;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Security;
using ObjectiveManagerApp.UI.Services;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Configuration;
using System.Globalization;
using System.Windows;

namespace ObjectiveManagerApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private static List<CultureInfo> _languages = new List<CultureInfo>();
        private const string DatabaseConnectionName = "MsSqlDbConnection";
        private const string LogFileName = "log.txt";

        public App()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration
                    .WriteTo.File(LogFileName)
                    .MinimumLevel.Debug();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddDbContextPool<ApplicationContext>(options =>
                    {
                        options.UseSqlServer(ConfigurationManager.ConnectionStrings[DatabaseConnectionName].ConnectionString);
                    });
                    services.AddTransient<IUserRepository, UserRepository>();
                    services.AddTransient<IProjectRepository, ProjectRepository>();
                    services.AddTransient<IObjectiveRepository, ObjectiveRepository>();
                    services.AddTransient<IEmployeeService, EmployeeService>();
                    services.AddTransient<IProjectService, ProjectService>();
                    services.AddTransient<IObjectiveService, ObjectiveService>();
                    services.AddTransient<ICategoryService, CategoryService>();
                    services.AddTransient<IAuthenticationService, AuthenticationService>();
                    services.AddTransient<IHashService, HashService>();
                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IRoleRepository, RoleRepository>();
                    services.AddTransient<ICategoryRepository, CategoryRepository>();
                })
                .Build();
        }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBoxStore.Error(ex.Message);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxStore.Error(e.Exception.Message);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }

}
