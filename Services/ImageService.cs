using Avalonia.Media.Imaging;
using isdf_bmeditor.Models;
using System;
using System.Collections.Generic;
using System.IO;

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
            using var stream = File.OpenRead(path);
            asset.Bitmap = new Bitmap(stream);
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