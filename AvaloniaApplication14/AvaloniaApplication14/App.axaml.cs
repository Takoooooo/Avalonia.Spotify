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
                var mainVm = new MainViewModel();
                var navigationManagerParent = new NavigationManager(mainWindow.MainFrameContent);
                navigationManagerParent.Register<MainView>(NavigationKeysParent.MainPage, mainVm);
                navigationManagerParent.Register<PlayerPage>(NavigationKeysParent.PlayerPage, mainVm);
                navigationManagerParent.Navigate(NavigationKeysParent.MainPage, null);
                var mainView = mainWindow.MainFrameContent.Content as MainView;
                var navigationManager = new NavigationManager(mainView.FrameContent);
                mainVm.NavigationManager = new ViewModels.Navigation()
                {
                    NavigationManagerChild = navigationManager,
                    NavigationManagerParent = navigationManagerParent
                };
                mainWindow.DataContext = mainVm;
                navigationManager.Register<HomePage>(NavigationKeys.HomePage, mainVm);
                navigationManager.Register<EmptyPage>(NavigationKeys.EmptyPage, new object());
                navigationManager.Navigate(NavigationKeys.HomePage, null);
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                var mainPage = new MainPage();
                singleViewPlatform.MainView = mainPage;
                var mainVm = new MainViewModel();
                var navigationManagerParent = new NavigationManager(mainPage.MainFrameContent);
                navigationManagerParent.Navigated += NavigationManagerParent_Navigated;

                void NavigationManagerParent_Navigated(object? sender, Egor92.MvvmNavigation.Abstractions.NavigationEventArgs e)
                {
                    if (e.NavigationKey == NavigationKeysParent.MainPage)
                    {
                        var mainView = mainPage.MainFrameContent.Content as MainView;
                        var navigationManager = new NavigationManager(mainView.FrameContent);
                        mainVm.NavigationManager = new ViewModels.Navigation()
                        {
                            NavigationManagerChild = navigationManager,
                            NavigationManagerParent = navigationManagerParent
                        };
                        mainPage.DataContext = mainVm;
                        navigationManager.Register<HomePage>(NavigationKeys.HomePage, mainVm);
                        navigationManager.Register<EmptyPage>(NavigationKeys.EmptyPage, new object());
                        navigationManager.Navigate(NavigationKeys.HomePage, null);
                    } 
                }
                navigationManagerParent.Register<MainView>(NavigationKeysParent.MainPage, mainVm);
                navigationManagerParent.Register<PlayerPage>(NavigationKeysParent.PlayerPage, mainVm);
                navigationManagerParent.Navigate(NavigationKeysParent.MainPage, null);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}