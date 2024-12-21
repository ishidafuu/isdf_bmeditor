using System;
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

    public static readonly StyledProperty<double> XProperty =
        AvaloniaProperty.Register<CharacterView, double>(nameof(X));

    public static readonly StyledProperty<double> YProperty =
        AvaloniaProperty.Register<CharacterView, double>(nameof(Y));

    public static readonly StyledProperty<double> AngleProperty =
        AvaloniaProperty.Register<CharacterView, double>(nameof(Angle));

    public double X
    {
        get => GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    public double Y
    {
        get => GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    public double Angle
    {
        get => GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    public static readonly StyledProperty<double> ScaleProperty =
        AvaloniaProperty.Register<CharacterView, double>(nameof(Scale), 1.0);

    /// <summary>
    /// 表示倍率
    /// </summary>
    public double Scale
    {
        get => GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    public override void Render(DrawingContext context)
    {
        if (Image?.Bitmap == null) return;

        using var _ = context.PushTransform(
            Matrix.CreateTranslation(-Image.Width / 2, -Image.Height / 2) *
            Matrix.CreateScale(Scale, Scale) *
            Matrix.CreateRotation(Angle * Math.PI / 180) *
            Matrix.CreateTranslation(X + Image.Width / 2, Y + Image.Height / 2));

        context.DrawImage(Image.Bitmap, new Rect(0, 0, Image.Width, Image.Height));
    }
} 