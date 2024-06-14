using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ImdbProxy.Messages;
using ImdbProxy.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ImdbProxy.ViewModel;

[INotifyPropertyChanged]
public partial class MovieFullViewModel : BaseViewModel
{
    [ObservableProperty] public Movie? _movie;
    
    [RelayCommand]
    private void Back()
    {
        var model = App.Provider.GetService<MainPageViewModel>();
        var message = new ChangeViewModelMessage(model!);
        WeakReferenceMessenger.Default.Send(message);
    }
}