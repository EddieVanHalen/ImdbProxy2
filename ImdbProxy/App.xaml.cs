using System.Windows;
using ImdbProxy.Proxy.Classes;
using ImdbProxy.View;
using ImdbProxy.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ImdbProxy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ServiceCollection Collection { get; set; } = null!;
    public static ServiceProvider Provider { get; set; } = null!;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Collection = new ServiceCollection();

        Collection.AddSingleton<MainViewModel>();
        Collection.AddSingleton<MainPageViewModel>();
        Collection.AddSingleton<MovieFullViewModel>();
        Collection.AddSingleton<OmdbApi>();
        Collection.AddSingleton<TmdbApi>();
        Collection.AddSingleton<Proxy.Classes.Proxy>();

        Collection.AddSingleton<MainView>();

        Provider = Collection.BuildServiceProvider();

        var view = Provider.GetService<MainView>();
        
        view!.ShowDialog();
    }
}