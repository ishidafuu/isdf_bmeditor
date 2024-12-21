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
} 