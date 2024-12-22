using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using isdf_bmeditor.Models;

namespace isdf_bmeditor.Controls;

/// <summary>
/// キャラクター表示用のカスタムコントロール
/// </summary>
public class CharacterView : Control
{
    static CharacterView()
    {
        AffectsRender<CharacterView>(
            ImageProperty,
            XProperty,
            YProperty,
            AngleProperty,
            CellIndexProperty,
            CellSizeProperty,
            ScaleProperty
        );
    }

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

    public static readonly StyledProperty<int> CellIndexProperty =
        AvaloniaProperty.Register<CharacterView, int>(nameof(CellIndex));

    public static readonly StyledProperty<Size> CellSizeProperty =
        AvaloniaProperty.Register<CharacterView, Size>(nameof(CellSize), new Size(32, 32));

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

    public int CellIndex
    {
        get => GetValue(CellIndexProperty);
        set => SetValue(CellIndexProperty, value);
    }

    public Size CellSize
    {
        get => GetValue(CellSizeProperty);
        set => SetValue(CellSizeProperty, value);
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

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        
        if (change.Property == XProperty ||
            change.Property == YProperty ||
            change.Property == AngleProperty ||
            change.Property == CellIndexProperty ||
            change.Property == CellSizeProperty ||
            change.Property == ScaleProperty ||
            change.Property == ImageProperty)
        {
            InvalidateVisual();
        }
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    public override void Render(DrawingContext context)
    {
        if (Image?.Bitmap == null) return;

        // セルのサイズと位置を計算
        var cellsPerRow = Image.Width / (int)CellSize.Width;
        var cellsPerColumn = Image.Height / (int)CellSize.Height;

        // セル数が0以下の場合は描画しない
        if (cellsPerRow <= 0 || cellsPerColumn <= 0) return;

        var row = CellIndex / cellsPerRow;
        var col = CellIndex % cellsPerRow;

        // 画像の範囲をチェック
        if (col >= cellsPerRow || row >= cellsPerColumn)
        {
            return;
        }

        var sourceRect = new Rect(
            col * CellSize.Width,
            row * CellSize.Height,
            CellSize.Width,
            CellSize.Height);

        // 拡大後のサイズを計算
        var scaledWidth = CellSize.Width * Scale;
        var scaledHeight = CellSize.Height * Scale;

        // 画像タイプに応じた変換行列を設定
        // ボディ（40x40）、フェイス（16x16）、アイテム（32x32）で判定
        Matrix transform;
        if (CellSize.Width == 40 && CellSize.Height == 40)  // ボディ
        {
            transform = Matrix.CreateTranslation(-CellSize.Width / 2, 0) *  // 左右中心、上下は下端
                       Matrix.CreateScale(Scale, Scale) *
                       Matrix.CreateRotation(Angle * Math.PI / 180) *
                       Matrix.CreateTranslation(X, Y);
        }
        else if (CellSize.Width == 16 && CellSize.Height == 16)  // フェイス
        {
            transform = Matrix.CreateTranslation(-CellSize.Width / 2, 0) *  // 左右中心、上下は下端
                       Matrix.CreateScale(Scale, Scale) *
                       Matrix.CreateRotation(Angle * Math.PI / 180) *
                       Matrix.CreateTranslation(X, Y);
        }
        else  // アイテム（32x32）
        {
            transform = Matrix.CreateTranslation(-CellSize.Width / 2, 0) *  // 左右中心、上下は下端
                       Matrix.CreateScale(Scale, Scale) *
                       Matrix.CreateRotation(Angle * Math.PI / 180) *
                       Matrix.CreateTranslation(X, Y);
        }

        using var _ = context.PushTransform(transform);

        // アンチエイリアスを無効にするレンダリングオプションを設定
        var renderOptions = new RenderOptions
        {
            BitmapInterpolationMode = BitmapInterpolationMode.None
        };

        using (context.PushRenderOptions(renderOptions))
        {
            context.DrawImage(
                Image.Bitmap,
                sourceRect,
                new Rect(0, 0, CellSize.Width, CellSize.Height));
        }
    }
} 