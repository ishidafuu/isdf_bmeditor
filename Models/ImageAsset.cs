using Avalonia.Media.Imaging;
using System;

namespace isdf_bmeditor.Models;

/// <summary>
/// 画像アセットを管理するクラス
/// </summary>
public class ImageAsset : IDisposable
{
    private Bitmap? _bitmap;
    private bool _disposed;

    /// <summary>
    /// 画像データ
    /// </summary>
    public Bitmap? Bitmap
    {
        get => _bitmap;
        set
        {
            _bitmap?.Dispose();
            _bitmap = value;
        }
    }

    /// <summary>
    /// 画像の幅
    /// </summary>
    public int Width => _bitmap?.PixelSize.Width ?? 0;

    /// <summary>
    /// 画像の高さ
    /// </summary>
    public int Height => _bitmap?.PixelSize.Height ?? 0;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _bitmap?.Dispose();
            }
            _disposed = true;
        }
    }

    ~ImageAsset()
    {
        Dispose(false);
    }
} 