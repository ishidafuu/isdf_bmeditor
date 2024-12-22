using System;
using ReactiveUI;
using System.Reactive;
using Avalonia.Platform.Storage;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.IO;
using System.Text.Json;

namespace isdf_bmeditor.ViewModels;

public class WindowSettings
{
    public double X { get; set; }
    public double Y { get; set; }
}

/// <summary>
/// メインウィンドウのViewModel
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase? _currentViewModel;
    private IStorageProvider? _storageProvider;
    private readonly Services.ImageService _imageService;
    private Point _windowPosition;
    private const string SettingsFileName = "windowposition.json";
    private readonly string _settingsFilePath;

    public MainWindowViewModel()
    {
        _imageService = new Services.ImageService();
        
        // 設定ファイルのパスを設定
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "isdf_bmeditor"
        );
        Directory.CreateDirectory(appDataPath); // ディレクトリが存在しない場合は作成
        _settingsFilePath = Path.Combine(appDataPath, SettingsFileName);

        LoadWindowPosition();

        ShowCharaCellEditorCommand = ReactiveCommand.Create(() =>
        {
            InitializeStorageProviderIfNeeded();
            var viewModel = new CharaCellEditorViewModel(_imageService, _storageProvider!);
            CurrentViewModel = viewModel;
        });

        ShowBaseMotionEditorCommand = ReactiveCommand.Create(() =>
        {
            var viewModel = new BaseMotionEditorViewModel();
            CurrentViewModel = viewModel;
        });

        ShowBattleMotionEditorCommand = ReactiveCommand.Create(() =>
        {
            var viewModel = new BattleMotionEditorViewModel();
            CurrentViewModel = viewModel;
        });
    }

    public void Initialize()
    {
        InitializeStorageProviderIfNeeded();
        CurrentViewModel = new CharaCellEditorViewModel(_imageService, _storageProvider!);
    }

    private void InitializeStorageProviderIfNeeded()
    {
        if (_storageProvider != null) return;
        
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);
            if (topLevel == null)
            {
                throw new InvalidOperationException("TopLevelが見つかりません");
            }
            _storageProvider = topLevel.StorageProvider;
        }
        else
        {
            throw new InvalidOperationException("デスクトップアプリケーションライフタイムが見つかりません");
        }
    }

    public Point WindowPosition
    {
        get => _windowPosition;
        set => this.RaiseAndSetIfChanged(ref _windowPosition, value);
    }

    private void LoadWindowPosition()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<WindowSettings>(json);
                if (settings != null)
                {
                    WindowPosition = new Point(settings.X, settings.Y);
                    Console.WriteLine($"ウィンドウ位置を読み込みました: X={settings.X}, Y={settings.Y}");
                }
            }
            else
            {
                WindowPosition = new Point(0, 0);
                Console.WriteLine("設定ファイルが存在しないため、デフォルト位置を使用します");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ウィンドウ位置の読み込みに失敗しました: {ex.Message}");
            WindowPosition = new Point(0, 0);
        }
    }

    public void SaveWindowPosition(Point position)
    {
        try
        {
            var settings = new WindowSettings
            {
                X = position.X,
                Y = position.Y
            };
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(_settingsFilePath, json);
            Console.WriteLine($"ウィンドウ位置を保存しました: X={position.X}, Y={position.Y}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ウィンドウ位置の保存に失敗しました: {ex.Message}");
        }
    }

    /// <summary>
    /// 現在表示中のViewModel
    /// </summary>
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public ReactiveCommand<Unit, Unit> ShowCharaCellEditorCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowBaseMotionEditorCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowBattleMotionEditorCommand { get; }
} 