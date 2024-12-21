using System.Collections.Generic;
using ReactiveUI;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// 戦闘モーションエディタのViewModel
/// </summary>
public class BattleMotionEditorViewModel : ViewModelBase
{
    private List<Motion> _motions = new();
    private int _activeMotionIndex;
    private int _activeKomaIndex;

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
    public Motion? ActiveMotion => _motions.Count > _activeMotionIndex ? _motions[_activeMotionIndex] : null;

    /// <summary>
    /// 現在選択中のコマ情報
    /// </summary>
    public Koma? ActiveKoma => ActiveMotion?.Komas.Count > _activeKomaIndex ? ActiveMotion.Komas[_activeKomaIndex] : null;
} 