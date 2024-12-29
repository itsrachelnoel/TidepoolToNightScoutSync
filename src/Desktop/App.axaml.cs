using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Desktop.ViewModels;
using Desktop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
        // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
        DisableAvaloniaDataAnnotationValidation();

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        RegisterViewModels(collection);

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        var services = collection.BuildServiceProvider();
        var vm = services.GetRequiredService<MainWindowViewModel>();
        LoadState(vm);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = vm };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow { DataContext = vm };
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Load saved state.
    /// </summary>
    private void LoadState(MainWindowViewModel vm)
    {
        StateManager.LoadState().Wait();
        vm.TidepoolUsername = StateManager.State.TidepoolUsername;
        vm.TidepoolPassword = StateManager.State.TidepoolPassword;
    }

    private static void RegisterViewModels(ServiceCollection collection)
    {
        collection.AddTransient<MainWindowViewModel>();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}