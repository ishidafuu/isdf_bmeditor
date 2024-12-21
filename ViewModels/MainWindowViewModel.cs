using System;
using ReactiveUI;
using System.Reactive;
using Avalonia.Platform.Storage;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// メインウィンドウのViewModel
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel = null!;
    private IStorageProvider? _storageProvider;
    private readonly Services.ImageService _imageService;

    public MainWindowViewModel()
    {
        _imageService = new Services.ImageService();

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

        // デフォルトでキャラクターセルエディタを表示
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

    /// <summary>
    /// 現在表示中のViewModel
    /// </summary>
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public ReactiveCommand<Unit, Unit> ShowCharaCellEditorCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowBaseMotionEditorCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowBattleMotionEditorCommand { get; }
} 