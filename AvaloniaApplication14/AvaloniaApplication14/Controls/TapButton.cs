using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;
using System;

namespace AvaloniaApplication14.Controls
{
    public class TapButton : Button, IStyleable
    {
        Type IStyleable.StyleKey => typeof(Button);

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            e.Handled = false;
        }
    }
}
