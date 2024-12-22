using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;
using isdf_bmeditor.Models;
using isdf_bmeditor.Services;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using System.IO;

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
    private double _scale = 4.0;

    public CharaCellEditorViewModel(ImageService imageService, IStorageProvider storageProvider)
    {
        _imageService = imageService;
        _storageProvider = storageProvider;

        // 初期セルの作成
        _cells.Add(new Cell());
        ActiveCellIndex = 0;

        // コマンドの初期化
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

        // 固定画像の読み込み
        LoadFixedImages();
    }

    private void LoadFixedImages()
    {
        try
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var assetsPath = Path.Combine(baseDirectory, "Assets", "Characters");
            Console.WriteLine($"Assets path: {assetsPath}");

            BodyImage = _imageService.LoadImage(Path.Combine(assetsPath, "body.png"));
            FaceImage = _imageService.LoadImage(Path.Combine(assetsPath, "face.png"));
            ItemImage = _imageService.LoadImage(Path.Combine(assetsPath, "item.png"));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading fixed images: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }

    /// <summary>
    /// 現在選択中のセル
    /// </summary>
    public Cell? ActiveCell
    {
        get => _cells.Count > _activeCellIndex ? _cells[_activeCellIndex] : null;
    }

    /// <summary>
    /// 現在選択中のセルのインデックス
    /// </summary>
    public int ActiveCellIndex
    {
        get => _activeCellIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _activeCellIndex, value);
            this.RaisePropertyChanged(nameof(ActiveCell));
        }
    }

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
        private set => this.RaiseAndSetIfChanged(ref _scale, value);
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
        if (ActiveCell == null)
        {
            Console.WriteLine("ActiveCell is null");
            return;
        }

        var parts = parameter.Split('_');
        if (parts.Length != 2)
        {
            Console.WriteLine($"Invalid parameter format: {parameter}");
            return;
        }

        if (!int.TryParse(parts[1], out int amount))
        {
            Console.WriteLine($"Invalid amount format: {parts[1]}");
            return;
        }

        Console.WriteLine($"Updating position: {parameter}");
        Console.WriteLine($"Current values - BodyX:{ActiveCell.BodyX}, BodyY:{ActiveCell.BodyY}, FaceX:{ActiveCell.FaceX}, FaceY:{ActiveCell.FaceY}, ItemX:{ActiveCell.ItemX}, ItemY:{ActiveCell.ItemY}");

        var property = parts[0];
        switch (property)
        {
            case "BodyX":
                ActiveCell.BodyX += amount;
                Console.WriteLine($"Updated BodyX to: {ActiveCell.BodyX}");
                break;
            case "BodyY":
                ActiveCell.BodyY += amount;
                Console.WriteLine($"Updated BodyY to: {ActiveCell.BodyY}");
                break;
            case "FaceX":
                ActiveCell.FaceX += amount;
                Console.WriteLine($"Updated FaceX to: {ActiveCell.FaceX}");
                break;
            case "FaceY":
                ActiveCell.FaceY += amount;
                Console.WriteLine($"Updated FaceY to: {ActiveCell.FaceY}");
                break;
            case "FaceAngle":
                // 90度単位の回転のみを許可
                var newAngle = (ActiveCell.FaceAngle + amount + 360) % 360;
                // 最も近い90度の倍数に丸める
                ActiveCell.FaceAngle = (int)(Math.Round(newAngle / 90.0) * 90) % 360;
                Console.WriteLine($"Updated FaceAngle to: {ActiveCell.FaceAngle}");
                break;
            case "ItemX":
                ActiveCell.ItemX += amount;
                Console.WriteLine($"Updated ItemX to: {ActiveCell.ItemX}");
                break;
            case "ItemY":
                ActiveCell.ItemY += amount;
                Console.WriteLine($"Updated ItemY to: {ActiveCell.ItemY}");
                break;
            case "ItemAngle":
                ActiveCell.ItemAngle = (ActiveCell.ItemAngle + amount + 360) % 360;
                Console.WriteLine($"Updated ItemAngle to: {ActiveCell.ItemAngle}");
                break;
            default:
                Console.WriteLine($"Unknown property: {property}");
                break;
        }

        // 変更を通知
        this.RaisePropertyChanged(nameof(ActiveCell));
        // 個別のプロパティ変更も通知
        this.RaisePropertyChanged($"ActiveCell.{property}");
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
} 