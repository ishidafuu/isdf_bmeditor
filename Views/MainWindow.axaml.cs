using Avalonia.Controls;
using Avalonia.Interactivity;
using isdf_bmeditor.ViewModels;
using Avalonia;

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
            viewModel.SaveWindowPosition(new Point(Position.X, Position.Y));
        }
    }
} 