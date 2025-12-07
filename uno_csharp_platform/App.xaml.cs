using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uno.Resizetizer;
using uno_csharp_platform.Presentation.ViewModels;
using uno_csharp_platform.Presentation.Views;
using uno_csharp_platform.Data;
using uno_csharp_platform.Services;

namespace uno_csharp_platform;

public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }
    protected IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {

        SQLitePCL.Batteries_V2.Init();

        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);


                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) => {
#if DEBUG
                // DelegatingHandler will be automatically injected
                services.AddTransient<DelegatingHandler, DebugHttpHandler>();
#endif

})
                .ConfigureServices((context, services) =>
                {
                    // TODO: Register your services
                    //services.AddSingleton<IMyService, MyService>();
                    
                    // using var db = new AppDbContext();
                    // db.Database.EnsureCreated();
                    services.AddSingleton<ICartService, CartService>();
                    services.AddSingleton<IProductService, ProductService>();
                    services.AddSingleton<IOrderService, OrderService>();
                })
                .UseNavigation(RegisterRoutes)
            );
        MainWindow = builder.Window;

        #if DEBUG
        MainWindow.UseStudio();
#endif
                MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell>();

        await InitializeDatabaseAsync();
    }


    private static async Task InitializeDatabaseAsync()
    {
        await Task.Run(() =>
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        });
    }


    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(ShellViewModel)),

            // AUTH
            new ViewMap<LoginPage, LoginViewModel>(),
            new ViewMap<RegisterPage, RegisterViewModel>(),

            // MAIN FLOW
            new ViewMap<HomePage, HomeViewModel>(),
            new ViewMap<ProfilePage, ProfileViewModel>(),
            
            // SHOP FLOW
            new ViewMap<ProductDetailPage, ProductDetailViewModel>(),
            new ViewMap<CartPage, CartViewModel>(),
            new ViewMap<CheckoutPage, CheckoutViewModel>(),
            new ViewMap<OrderSuccessPage, OrderSuccessViewModel>()
        );

        routes.Register(
            new RouteMap(
                "", 
                View: views.FindByViewModel<ShellViewModel>(),
                Nested: [
                    new (
                        "Login", 
                        View: views.FindByViewModel<LoginViewModel>(),
                        IsDefault: true
                    ),
                    new ("Register", View: views.FindByViewModel<RegisterViewModel>()),
                    new ("Home", View: views.FindByViewModel<HomeViewModel>()),
                    new ("Profile", View: views.FindByViewModel<ProfileViewModel>()),
                    new ("ProductDetail", View: views.FindByViewModel<ProductDetailViewModel>()),
                    new ("Cart", View: views.FindByViewModel<CartViewModel>()),
                    new ("Checkout", View: views.FindByViewModel<CheckoutViewModel>()),
                    new ("OrderSuccess", View: views.FindByViewModel<OrderSuccessViewModel>())
                ]
            )
        );
    }
}