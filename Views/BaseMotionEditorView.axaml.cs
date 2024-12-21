using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace isdf_bmeditor.Views;

public partial class BaseMotionEditorView : UserControl
{
    public BaseMotionEditorView()
    {
        AvaloniaXamlLoader.Load(this);
    }
} 