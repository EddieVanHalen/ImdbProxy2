using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImdbProxy.Model;
using ImdbProxy.Proxy.Classes;

namespace ImdbProxy.ViewModel;

[INotifyPropertyChanged]
public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty] private ICollection<Movie?> _moviesList;

    [ObservableProperty] private int? _selectedIndex;

    public MainPageViewModel()
    {
        MoviesList = new ObservableCollection<Movie>()!;
    }

    [RelayCommand]
    private async void FindMovieAsync()
    {
        var finder = new OmdbApi();

        // MoviesList = await finder.RequestMoviesAsync("barbie");
    }

    [RelayCommand]
    private async void GetConcreteMovieAsync()
    {
        var finder = new TmdbApi();

        MoviesList = await finder.RequestMoviesAsync("Barbie");
    }
}