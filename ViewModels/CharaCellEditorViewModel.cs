using ReactiveUI;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.ViewModels;

public class CharaCellEditorViewModel : ViewModelBase
{
    private List<Cell> _cells = new();
    private int _activeCellIndex;

    public int ActiveCellIndex
    {
        get => _activeCellIndex;
        set => this.RaiseAndSetIfChanged(ref _activeCellIndex, value);
    }

    public Cell? ActiveCell => _cells.Count > _activeCellIndex ? _cells[_activeCellIndex] : null;

    public void ChangeCellIndex(int amount)
    {
        var newIndex = ActiveCellIndex + amount;
        if (newIndex >= 0)
        {
            ActiveCellIndex = newIndex;
        }
    }
} 