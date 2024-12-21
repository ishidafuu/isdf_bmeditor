using System.Collections.Generic;
using ReactiveUI;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// キャラクターセルエディタのViewModel
/// </summary>
public class CharaCellEditorViewModel : ViewModelBase
{
    private List<Cell> _cells = new();
    private int _activeCellIndex;

    /// <summary>
    /// 現在選択中のセルインデックス
    /// </summary>
    public int ActiveCellIndex
    {
        get => _activeCellIndex;
        set => this.RaiseAndSetIfChanged(ref _activeCellIndex, value);
    }

    /// <summary>
    /// 現在選択中のセル情報
    /// </summary>
    public Cell? ActiveCell => _cells.Count > _activeCellIndex ? _cells[_activeCellIndex] : null;

    /// <summary>
    /// セルインデックスを変更する
    /// </summary>
    /// <param name="amount">変更量（正の値で増加、負の値で減少）</param>
    public void ChangeCellIndex(int amount)
    {
        var newIndex = ActiveCellIndex + amount;
        if (newIndex >= 0)
        {
            ActiveCellIndex = newIndex;
        }
    }
} 