using System.Collections.Generic;
using ReactiveUI;
using isdf_bmeditor.Models;
using isdf_bmeditor.Services;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// キャラクターセルエディタのViewModel
/// </summary>
public class CharaCellEditorViewModel : ViewModelBase
{
    private readonly ImageService _imageService;
    private List<Cell> _cells = new();
    private int _activeCellIndex;
    private ImageAsset? _bodyImage;
    private ImageAsset? _faceImage;
    private ImageAsset? _itemImage;

    public CharaCellEditorViewModel(ImageService imageService)
    {
        _imageService = imageService;
        ChangeCellIndexCommand = ReactiveCommand.Create<int>(ChangeCellIndex);
        UpdateCellPositionCommand = ReactiveCommand.Create<string>(UpdateCellPosition);
        SaveCommand = ReactiveCommand.Create<string>(SaveCells);
        LoadCommand = ReactiveCommand.Create<string>(LoadCells);
        AddCellCommand = ReactiveCommand.Create(AddCell);
        DeleteCellCommand = ReactiveCommand.Create(DeleteCell);
    }

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
    /// 体の画像
    /// </summary>
    public ImageAsset? BodyImage
    {
        get => _bodyImage;
        private set => this.RaiseAndSetIfChanged(ref _bodyImage, value);
    }

    /// <summary>
    /// 顔の画像
    /// </summary>
    public ImageAsset? FaceImage
    {
        get => _faceImage;
        private set => this.RaiseAndSetIfChanged(ref _faceImage, value);
    }

    /// <summary>
    /// アイテムの画像
    /// </summary>
    public ImageAsset? ItemImage
    {
        get => _itemImage;
        private set => this.RaiseAndSetIfChanged(ref _itemImage, value);
    }

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

    /// <summary>
    /// 画像を読み込む
    /// </summary>
    /// <param name="bodyPath">体の画像パス</param>
    /// <param name="facePath">顔の画像パス</param>
    /// <param name="itemPath">アイテムの画像パス</param>
    public void LoadImages(string bodyPath, string facePath, string itemPath)
    {
        BodyImage = _imageService.LoadImage(bodyPath);
        FaceImage = _imageService.LoadImage(facePath);
        ItemImage = _imageService.LoadImage(itemPath);
    }

    /// <summary>
    /// セル番号変更コマンド
    /// </summary>
    public ReactiveCommand<int, Unit> ChangeCellIndexCommand { get; }

    /// <summary>
    /// セル位置更新コマンド
    /// </summary>
    public ReactiveCommand<string, Unit> UpdateCellPositionCommand { get; }

    /// <summary>
    /// セルの位置を更新する
    /// </summary>
    private void UpdateCellPosition(string parameter)
    {
        if (ActiveCell == null) return;

        var parts = parameter.Split('_');
        if (parts.Length != 2) return;

        var (property, amount) = (parts[0], int.Parse(parts[1]));

        switch (property)
        {
            case "BodyX":
                ActiveCell.BodyX += amount;
                break;
            case "BodyY":
                ActiveCell.BodyY += amount;
                break;
            case "FaceX":
                ActiveCell.FaceX += amount;
                break;
            case "FaceY":
                ActiveCell.FaceY += amount;
                break;
            case "FaceAngle":
                ActiveCell.FaceAngle = (ActiveCell.FaceAngle + amount + 360) % 360;
                break;
            case "ItemX":
                ActiveCell.ItemX += amount;
                break;
            case "ItemY":
                ActiveCell.ItemY += amount;
                break;
            case "ItemAngle":
                ActiveCell.ItemAngle = (ActiveCell.ItemAngle + amount + 360) % 360;
                break;
        }

        this.RaisePropertyChanged(nameof(ActiveCell));
    }

    /// <summary>
    /// 保存コマンド
    /// </summary>
    public ReactiveCommand<string, Unit> SaveCommand { get; }

    /// <summary>
    /// 読み込みコマンド
    /// </summary>
    public ReactiveCommand<string, Unit> LoadCommand { get; }

    /// <summary>
    /// セルデータを保存する
    /// </summary>
    /// <param name="path">保存先のパス</param>
    private void SaveCells(string path)
    {
        var data = new CellData { Cells = _cells };
        data.Save(path);
    }

    /// <summary>
    /// セルデータを読み込む
    /// </summary>
    /// <param name="path">読み込むファイルのパス</param>
    private void LoadCells(string path)
    {
        var data = CellData.Load(path);
        _cells = data.Cells;
        ActiveCellIndex = 0;
        this.RaisePropertyChanged(nameof(ActiveCell));
    }

    /// <summary>
    /// セル追加コマンド
    /// </summary>
    public ReactiveCommand<Unit, Unit> AddCellCommand { get; }

    /// <summary>
    /// セル削除コマンド
    /// </summary>
    public ReactiveCommand<Unit, Unit> DeleteCellCommand { get; }

    /// <summary>
    /// 新規セルを追加する
    /// </summary>
    private void AddCell()
    {
        _cells.Add(new Cell());
        ActiveCellIndex = _cells.Count - 1;
        this.RaisePropertyChanged(nameof(ActiveCell));
    }

    /// <summary>
    /// 現在選択中のセルを削除する
    /// </summary>
    private void DeleteCell()
    {
        if (_cells.Count == 0 || ActiveCellIndex < 0 || ActiveCellIndex >= _cells.Count)
            return;

        _cells.RemoveAt(ActiveCellIndex);
        if (ActiveCellIndex >= _cells.Count)
        {
            ActiveCellIndex = Math.Max(0, _cells.Count - 1);
        }
        this.RaisePropertyChanged(nameof(ActiveCell));
    }
} 