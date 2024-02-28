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
//using Microsoft.Extensions.Configuration;

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

        public App()
        {
            _languages.Clear();
            _languages.Add(new CultureInfo("en-US"));
            _languages.Add(new CultureInfo("ru-RU"));

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(host.Configuration)
                        .WriteTo.File(ConfigurationManager.AppSettings["serilog:write-to:File.path"]);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddDbContextPool<ApplicationContext>(options =>
                    {
                        options.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings[DatabaseConnectionName].ConnectionString);
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

        public static List<CultureInfo> Languages
        {
            get
            {
                return _languages;
            }
        }

        public static event EventHandler? LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value==null) throw new ArgumentNullException("value");
                if (value==System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
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
