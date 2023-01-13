using Avalonia.Web.Blazor;

namespace AvaloniaApplication14.Web
{
    public partial class App
    {
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            WebAppBuilder.Configure<AvaloniaApplication14.App>()
                .SetupWithSingleViewLifetime();
        }
    }
}