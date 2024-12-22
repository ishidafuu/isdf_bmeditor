using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// 基本モーションエディタのViewModel
/// </summary>
public class BaseMotionEditorViewModel : ViewModelBase
{
    private ObservableCollection<Motion> _motions = new();
    private int _activeMotionIndex;
    private int _activeKomaIndex;

    public BaseMotionEditorViewModel()
    {
        ChangeKomaIndexCommand = ReactiveCommand.Create<string>(param => 
        {
            if (int.TryParse(param, out int amount))
            {
                ChangeKomaIndex(amount);
            }
        });
    }

    /// <summary>
    /// モーションリスト
    /// </summary>
    public ObservableCollection<Motion> Motions
    {
        get => _motions;
        set => this.RaiseAndSetIfChanged(ref _motions, value);
    }

    /// <summary>
    /// 現在選択中のモーションインデックス
    /// </summary>
    public int ActiveMotionIndex
    {
        get => _activeMotionIndex;
        set => this.RaiseAndSetIfChanged(ref _activeMotionIndex, value);
    }

    /// <summary>
    /// 現在選択中のコマインデックス
    /// </summary>
    public int ActiveKomaIndex
    {
        get => _activeKomaIndex;
        set => this.RaiseAndSetIfChanged(ref _activeKomaIndex, value);
    }

    /// <summary>
    /// 現在選択中のモーション情報
    /// </summary>
    public Motion? ActiveMotion => Motions.Count > ActiveMotionIndex ? Motions[ActiveMotionIndex] : null;

    /// <summary>
    /// 現在選択中のコマ情報
    /// </summary>
    public Koma? ActiveKoma => ActiveMotion?.Komas.Count > ActiveKomaIndex ? ActiveMotion.Komas[ActiveKomaIndex] : null;

    /// <summary>
    /// コマインデックス変更コマンド
    /// </summary>
    public ReactiveCommand<string, Unit> ChangeKomaIndexCommand { get; }

    /// <summary>
    /// コマインデックスを変更する
    /// </summary>
    /// <param name="amount">変更量（正の値で増加、負の値で減少）</param>
    private void ChangeKomaIndex(int amount)
    {
        var newIndex = ActiveKomaIndex + amount;
        if (ActiveMotion != null && newIndex >= 0 && newIndex < ActiveMotion.Komas.Count)
        {
            ActiveKomaIndex = newIndex;
        }
    }
} 