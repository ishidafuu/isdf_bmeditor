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
            // ImageSharpを使用して画像を読み込む
            using var image = Image.Load<Rgba32>(path);
            
            // マゼンタ色を透過に変換
            for (int y = 0; y < image.Height; y++)
            {
                var row = image.GetPixelRowSpan(y);
                for (int x = 0; x < row.Length; x++)
                {
                    ref var pixel = ref row[x];
                    if (pixel.R == 255 && pixel.G == 0 && pixel.B == 255)
                    {
                        pixel = new Rgba32(0, 0, 0, 0);
                    }
                }
            }

            // メモリストリームに保存
            using var memoryStream = new MemoryStream();
            image.SaveAsPng(memoryStream);
            memoryStream.Position = 0;

            // AvaloniaのBitmapとして読み込む
            asset.Bitmap = new Bitmap(memoryStream);
            _imageCache[path] = asset;
            return asset;
        }
        catch (Exception)
        {
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