using Avalonia.Controls;
using Avalonia.Interactivity;
using isdf_bmeditor.ViewModels;

namespace isdf_bmeditor.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.SaveWindowPosition(Position);
        }
    }
} 