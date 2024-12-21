using System;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;
using isdf_bmeditor.Services;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// メインウィンドウのViewModel
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private readonly ImageService _imageService;
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        _imageService = new ImageService();
        // 初期表示のViewModelを設定
        CurrentViewModel = new CharaCellEditorViewModel(_imageService);
    }

    /// <summary>
    /// 現在表示中のViewModel
    /// </summary>
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    /// <summary>
    /// キャラクターセルエディタに切り替え
    /// </summary>
    public void SwitchToCharaCellEditor() => CurrentViewModel = new CharaCellEditorViewModel(_imageService);

    /// <summary>
    /// 基本モーションエディタに切り替え
    /// </summary>
    public void SwitchToBaseMotionEditor() => CurrentViewModel = new BaseMotionEditorViewModel();

    /// <summary>
    /// 戦闘モーションエディタに切り替え
    /// </summary>
    public void SwitchToBattleMotionEditor() => CurrentViewModel = new BattleMotionEditorViewModel();
} 