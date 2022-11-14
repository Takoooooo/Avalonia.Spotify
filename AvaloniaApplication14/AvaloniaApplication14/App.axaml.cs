using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication14.Navigation;
using AvaloniaApplication14.ViewModels;
using AvaloniaApplication14.Views;
using Egor92.MvvmNavigation;

namespace AvaloniaApplication14
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;
                var mainView = mainWindow.FindControl<MainView>("mainView");

                var navigationManager = new NavigationManager(mainView!.FrameContent);
                navigationManager.Register<MainPage>(NavigationKeys.MainPage, () => new MainViewModel() { NavigationManager = navigationManager });
                navigationManager.Register<EmptyPage>(NavigationKeys.EmptyPage, new object());
                navigationManager.Navigate(NavigationKeys.MainPage, null);
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                var mainView = new MainView();
                singleViewPlatform.MainView = mainView;
                var navigationManager = new NavigationManager(mainView!.FrameContent);
                navigationManager.Register<MainPage>(NavigationKeys.MainPage, () => new MainViewModel() { NavigationManager = navigationManager });
                navigationManager.Register<EmptyPage>(NavigationKeys.EmptyPage, new object());
                navigationManager.Navigate(NavigationKeys.MainPage, null);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}