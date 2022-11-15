using Avalonia.Controls;
using AvaloniaApplication14.ViewModels;

namespace AvaloniaApplication14.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            //this.FindControl<Button>("s2").Command = (this.DataContext as MainViewModel).NavigateEmptyPageCommand;
        }
    }
}