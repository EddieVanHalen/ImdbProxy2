using System.Windows;
using ImdbProxy.ViewModel;

namespace ImdbProxy.View;

public partial class MainView : Window
{
    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();

        DataContext = mainViewModel;
    }
}