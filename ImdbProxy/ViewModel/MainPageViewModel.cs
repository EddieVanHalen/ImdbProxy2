using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ImdbProxy.Messages;
using ImdbProxy.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ImdbProxy.ViewModel;

[INotifyPropertyChanged]
public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty] private IList<Movie> _moviesList;

    [ObservableProperty] private int? _selectedIndex;
    
    [ObservableProperty] private string? _movieName;

    public MainPageViewModel()
    {
        MoviesList = new ObservableCollection<Movie>()!;
    }

    [RelayCommand]
    private async void FindMovieAsync()
    {
        var finder = new Proxy.Classes.Proxy();
        
        if(string.IsNullOrWhiteSpace(MovieName)) return;
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        MoviesList = await finder.RequestMoviesAsync(MovieName!);
    }

    [RelayCommand]
    private async void GetConcreteMovieAsync()
    {
        var finder = new Proxy.Classes.Proxy();

        if (SelectedIndex is null)
        {
            return;
        }
        
        var movie = await finder.RequestMovieAsync(MoviesList[SelectedIndex.Value].Id!);

        var model = App.Provider.GetService<MovieFullViewModel>();
        model!.Movie = movie;
        var message = new ChangeViewModelMessage(model!);
        WeakReferenceMessenger.Default.Send(message);
    }
}