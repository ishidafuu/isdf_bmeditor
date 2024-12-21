using ReactiveUI;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.ViewModels;

public class BaseMotionEditorViewModel : ViewModelBase
{
    private List<Motion> _motions = new();
    private int _activeMotionIndex;
    private int _activeKomaIndex;

    public int ActiveMotionIndex
    {
        get => _activeMotionIndex;
        set => this.RaiseAndSetIfChanged(ref _activeMotionIndex, value);
    }

    public int ActiveKomaIndex
    {
        get => _activeKomaIndex;
        set => this.RaiseAndSetIfChanged(ref _activeKomaIndex, value);
    }

    public Motion? ActiveMotion => _motions.Count > _activeMotionIndex ? _motions[_activeMotionIndex] : null;
    public Koma? ActiveKoma => ActiveMotion?.Komas.Count > _activeKomaIndex ? ActiveMotion.Komas[_activeKomaIndex] : null;
} 