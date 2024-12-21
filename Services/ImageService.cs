using Avalonia.Media.Imaging;
using isdf_bmeditor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace isdf_bmeditor.Services;

/// <summary>
/// 画像管理サービス
/// </summary>
public class ImageService
{
    private readonly Dictionary<string, ImageAsset> _imageCache = new();

    /// <summary>
    /// 画像を読み込む
    /// </summary>
    /// <param name="path">画像ファイルのパス</param>
    /// <returns>読み込んだ画像アセット</returns>
    public ImageAsset LoadImage(string path)
    {
        if (_imageCache.TryGetValue(path, out var cachedImage))
        {
            return cachedImage;
        }

        var asset = new ImageAsset();
        try
        {
            var fullPath = Path.GetFullPath(path);
            Console.WriteLine($"Loading image from: {fullPath}");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Error: File not found at {fullPath}");
                throw new FileNotFoundException($"Image file not found: {fullPath}");
            }

            // ImageSharpを使用して画像を読み込む
            using var image = Image.Load<Rgba32>(fullPath);
            Console.WriteLine($"Image loaded successfully. Size: {image.Width}x{image.Height}");
            
            // マゼンタ色を透過に変換
            image.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < accessor.Height; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    for (int x = 0; x < row.Length; x++)
                    {
                        ref var pixel = ref row[x];
                        if (pixel.R == 255 && pixel.G == 0 && pixel.B == 255)
                        {
                            pixel = new Rgba32(0, 0, 0, 0); // 完全な透過（アルファ値0）
                        }
                    }
                }
            });

            // メモリストリームに保存（PNG形式で保存して透過を維持）
            using var memoryStream = new MemoryStream();
            var encoder = new SixLabors.ImageSharp.Formats.Png.PngEncoder()
            {
                ColorType = SixLabors.ImageSharp.Formats.Png.PngColorType.RgbWithAlpha,
                BitDepth = SixLabors.ImageSharp.Formats.Png.PngBitDepth.Bit8,
            };
            image.Save(memoryStream, encoder);
            memoryStream.Position = 0;

            // AvaloniaのBitmapとして読み込む
            asset.Bitmap = new Bitmap(memoryStream);
            _imageCache[path] = asset;
            Console.WriteLine($"Image processed and cached successfully");
            return asset;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading image: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            asset.Dispose();
            throw;
        }
    }

    /// <summary>
    /// キャッシュをクリアする
    /// </summary>
    public void ClearCache()
    {
        foreach (var asset in _imageCache.Values)
        {
            asset.Dispose();
        }
        _imageCache.Clear();
    }
} 