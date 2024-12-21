using ReactiveUI;

namespace isdf_bmeditor.Models;

public class Cell : ReactiveObject
{
    private int _bodyIndex;
    private int _bodyX;
    private int _bodyY;
    private int _faceIndex;
    private int _faceAngle;
    private int _faceX;
    private int _faceY;
    private int _facePriority;
    private int _itemIndex;
    private int _itemAngle;
    private int _itemX;
    private int _itemY;
    private int _itemPriority;

    public int BodyIndex
    {
        get => _bodyIndex;
        set => this.RaiseAndSetIfChanged(ref _bodyIndex, value);
    }

    public int BodyX
    {
        get => _bodyX;
        set => this.RaiseAndSetIfChanged(ref _bodyX, value);
    }

    public int BodyY
    {
        get => _bodyY;
        set => this.RaiseAndSetIfChanged(ref _bodyY, value);
    }

    public int FaceIndex
    {
        get => _faceIndex;
        set => this.RaiseAndSetIfChanged(ref _faceIndex, value);
    }

    public int FaceAngle
    {
        get => _faceAngle;
        set => this.RaiseAndSetIfChanged(ref _faceAngle, value);
    }

    public int FaceX
    {
        get => _faceX;
        set => this.RaiseAndSetIfChanged(ref _faceX, value);
    }

    public int FaceY
    {
        get => _faceY;
        set => this.RaiseAndSetIfChanged(ref _faceY, value);
    }

    public int FacePriority
    {
        get => _facePriority;
        set => this.RaiseAndSetIfChanged(ref _facePriority, value);
    }

    public int ItemIndex
    {
        get => _itemIndex;
        set => this.RaiseAndSetIfChanged(ref _itemIndex, value);
    }

    public int ItemAngle
    {
        get => _itemAngle;
        set => this.RaiseAndSetIfChanged(ref _itemAngle, value);
    }

    public int ItemX
    {
        get => _itemX;
        set => this.RaiseAndSetIfChanged(ref _itemX, value);
    }

    public int ItemY
    {
        get => _itemY;
        set => this.RaiseAndSetIfChanged(ref _itemY, value);
    }

    public int ItemPriority
    {
        get => _itemPriority;
        set => this.RaiseAndSetIfChanged(ref _itemPriority, value);
    }
} 