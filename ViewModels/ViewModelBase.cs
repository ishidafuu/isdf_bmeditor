using ReactiveUI;
using System.Collections.Generic;

namespace isdf_bmeditor.ViewModels;

public class ViewModelBase : ReactiveObject
{
    private Stack<object> _undoStack = new();
    private Stack<object> _redoStack = new();

    protected void PushToUndoStack(object state)
    {
        _undoStack.Push(state);
        _redoStack.Clear();
    }

    protected bool CanUndo => _undoStack.Count > 0;
    protected bool CanRedo => _redoStack.Count > 0;

    protected virtual void Undo()
    {
        if (!CanUndo) return;
        var state = _undoStack.Pop();
        _redoStack.Push(state);
        RestoreState(state);
    }

    protected virtual void Redo()
    {
        if (!CanRedo) return;
        var state = _redoStack.Pop();
        _undoStack.Push(state);
        RestoreState(state);
    }

    protected virtual void RestoreState(object state)
    {
        // 継承先で実装
    }
}
