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
        
        // DataContextが設定された後に位置を設定
        DataContextChanged += (sender, e) =>
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                Position = new PixelPoint((int)viewModel.WindowPosition.X, (int)viewModel.WindowPosition.Y);
            }
        };

        // ウィンドウの位置が変更されたときにViewModelを更新
        PositionChanged += (sender, e) =>
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.WindowPosition = new Point(Position.X, Position.Y);
            }
        };
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.SaveWindowPosition(new Point(Position.X, Position.Y));
        }
    }
} 