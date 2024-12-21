using System;
using ReactiveUI;
using System.Reactive;
using Avalonia.Platform.Storage;
using Avalonia;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// メインウィンドウのViewModel
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel = null!;
    private readonly IStorageProvider _storageProvider;
    private readonly Services.ImageService _imageService;

    public MainWindowViewModel()
    {
        _imageService = new Services.ImageService();
        _storageProvider = new StandardStorageProvider();

        ShowCharaCellEditorCommand = ReactiveCommand.Create(() =>
        {
            var viewModel = new CharaCellEditorViewModel(_imageService, _storageProvider);
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
        CurrentViewModel = new CharaCellEditorViewModel(_imageService, _storageProvider);
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