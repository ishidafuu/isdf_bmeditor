using Avalonia.Controls;
using Avalonia.Interactivity;
using isdf_bmeditor.ViewModels;
using Avalonia;
using Avalonia.Input;

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

        // キー入力イベントを追加
        KeyDown += MainWindow_KeyDown;
    }

    private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.SaveWindowPosition(new Point(Position.X, Position.Y));
        }
    }
} 