using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.Controls;

/// <summary>
/// キャラクター表示用のカスタムコントロール
/// </summary>
public class CharacterView : Control
{
    /// <summary>
    /// 表示する画像
    /// </summary>
    public static readonly StyledProperty<ImageAsset?> ImageProperty =
        AvaloniaProperty.Register<CharacterView, ImageAsset?>(nameof(Image));

    /// <summary>
    /// 表示する画像
    /// </summary>
    public ImageAsset? Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    public override void Render(DrawingContext context)
    {
        if (Image?.Bitmap == null) return;

        var bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
        context.DrawImage(Image.Bitmap, bounds);
    }
} 