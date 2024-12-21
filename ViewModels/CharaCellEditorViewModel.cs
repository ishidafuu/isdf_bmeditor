using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;
using isdf_bmeditor.Models;
using isdf_bmeditor.Services;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace isdf_bmeditor.ViewModels;

/// <summary>
/// キャラクターセルエディタのViewModel
/// </summary>
public class CharaCellEditorViewModel : ViewModelBase
{
    private readonly ImageService _imageService;
    private readonly IStorageProvider _storageProvider;
    private List<Cell> _cells = new();
    private int _activeCellIndex;
    private ImageAsset? _bodyImage;
    private ImageAsset? _faceImage;
    private ImageAsset? _itemImage;
    private double _scale = 1.0;

    public CharaCellEditorViewModel(ImageService imageService, IStorageProvider storageProvider)
    {
        _imageService = imageService;
        _storageProvider = storageProvider;
        ChangeCellIndexCommand = ReactiveCommand.Create<string>(param => 
        {
            if (int.TryParse(param, out int amount))
            {
                ChangeCellIndex(amount);
            }
        });
        UpdateCellPositionCommand = ReactiveCommand.Create<string>(UpdateCellPosition);
        SaveCommand = ReactiveCommand.CreateFromTask<string>(SaveCells);
        LoadCommand = ReactiveCommand.CreateFromTask<string>(LoadCells);
        AddCellCommand = ReactiveCommand.Create(AddCell);
        DeleteCellCommand = ReactiveCommand.Create(DeleteCell);
        UpdateScaleCommand = ReactiveCommand.Create<string>(param =>
        {
            if (double.TryParse(param, out double amount))
            {
                UpdateScale(amount);
            }
        });

        // 固定画像の読み込み
        LoadFixedImages();
    }

    private void LoadFixedImages()
    {
        BodyImage = _imageService.LoadImage("Assets/Characters/body.png");
        FaceImage = _imageService.LoadImage("Assets/Characters/face.png");
        ItemImage = _imageService.LoadImage("Assets/Characters/item.png");
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
    /// 表示倍率
    /// </summary>
    public double Scale
    {
        get => _scale;
        set => this.RaiseAndSetIfChanged(ref _scale, value);
    }

    /// <summary>
    /// セル番号変更コマンド
    /// </summary>
    public ReactiveCommand<string, Unit> ChangeCellIndexCommand { get; }

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

        if (!int.TryParse(parts[1], out int amount)) return;

        var property = parts[0];
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
    private async Task SaveCells(string defaultName)
    {
        var options = new FilePickerSaveOptions
        {
            Title = "セルデータを保存",
            DefaultExtension = ".json",
            ShowOverwritePrompt = true,
            SuggestedFileName = defaultName,
            FileTypeChoices = new[]
            {
                new FilePickerFileType("JSONファイル")
                {
                    Patterns = new[] { "*.json" },
                    MimeTypes = new[] { "application/json" }
                }
            }
        };

        var file = await _storageProvider.SaveFilePickerAsync(options);
        if (file != null)
        {
            var data = new CellData { Cells = _cells };
            data.Save(file.Path.LocalPath);
        }
    }

    /// <summary>
    /// セルデータを読み込む
    /// </summary>
    /// <param name="path">読み込むファイルのパス</param>
    private async Task LoadCells(string _)
    {
        var options = new FilePickerOpenOptions
        {
            Title = "セルデータを読み込み",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("JSONファイル")
                {
                    Patterns = new[] { "*.json" },
                    MimeTypes = new[] { "application/json" }
                }
            }
        };

        var files = await _storageProvider.OpenFilePickerAsync(options);
        if (files.Count > 0)
        {
            var data = CellData.Load(files[0].Path.LocalPath);
            _cells = data.Cells;
            ActiveCellIndex = 0;
            this.RaisePropertyChanged(nameof(ActiveCell));
        }
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

    /// <summary>
    /// 拡大率変更コマンド
    /// </summary>
    public ReactiveCommand<string, Unit> UpdateScaleCommand { get; }

    /// <summary>
    /// 拡大率を変更する
    /// </summary>
    private void UpdateScale(double amount)
    {
        var newScale = Scale + amount;
        if (newScale >= 0.1 && newScale <= 5.0)
        {
            Scale = newScale;
        }
    }
} 