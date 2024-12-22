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

/// <summary>
/// メインウィンドウのViewModel
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase? _currentViewModel;
    private IStorageProvider? _storageProvider;
    private readonly Services.ImageService _imageService;
    private PixelPoint _windowPosition;
    private const string SettingsFileName = "windowposition.json";

    public MainWindowViewModel()
    {
        _imageService = new Services.ImageService();
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

    public PixelPoint WindowPosition
    {
        get => _windowPosition;
        set => this.RaiseAndSetIfChanged(ref _windowPosition, value);
    }

    private void LoadWindowPosition()
    {
        try
        {
            var settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
            if (File.Exists(settingsPath))
            {
                var json = File.ReadAllText(settingsPath);
                var position = JsonSerializer.Deserialize<PixelPoint>(json);
                WindowPosition = position;
            }
            else
            {
                WindowPosition = new PixelPoint(0, 0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ウィンドウ位置の読み込みに失敗しました: {ex.Message}");
            WindowPosition = new PixelPoint(0, 0);
        }
    }

    public void SaveWindowPosition(PixelPoint position)
    {
        try
        {
            var settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
            var json = JsonSerializer.Serialize(position);
            File.WriteAllText(settingsPath, json);
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