using System;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;

namespace isdf_bmeditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        // 初期表示のViewModelを設定
        CurrentViewModel = new CharaCellEditorViewModel();
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public void SwitchToCharaCellEditor() => CurrentViewModel = new CharaCellEditorViewModel();
    public void SwitchToBaseMotionEditor() => CurrentViewModel = new BaseMotionEditorViewModel();
    public void SwitchToBattleMotionEditor() => CurrentViewModel = new BattleMotionEditorViewModel();
} 