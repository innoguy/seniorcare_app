using FormsControls.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Infrastructure.Core.Interfaces;

namespace XF.Infrastructure.Core
{
    public class NavigationService : INavigationService
    {
        #region Properties
        private readonly PageLocator _pageLocator = new PageLocator();

        private AnimationNavigationPage _navigationPage;
        private INavigation Navigator => _navigationPage.CurrentPage?.Navigation;

        #endregion

        public AnimationNavigationPage SetMainPage(Type targetType, NavigationAnimation animation, Color? color = null, object args = null)
        {
            try
            {
                var page = _pageLocator.ResolvePageAndViewModel(targetType, args);
                if (animation != NavigationAnimation.None)
                {
                    page.PageAnimation = GetPageAnimanition(animation);
                }
                _navigationPage = new AnimationNavigationPage(page);
                SetBarBackgroundColor(_navigationPage, color ?? Color.Default);
                SetStatusBarColor(color ?? Color.Default);

                return _navigationPage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task NavigateToViewModelAsync(Type viewModelType, NavigationAnimation animation, Color? color = null, bool animated = false, bool modal = false, object args = null)
        {
            try
            {
                AnimationPage targetPage = _pageLocator.ResolvePageAndViewModel(viewModelType, args);
                if (animation != NavigationAnimation.None)
                {
                    targetPage.PageAnimation = GetPageAnimanition(animation);
                    animated = true;
                }

                if (targetPage != null)
                {
                    SetBarBackgroundColor(targetPage, color ?? Color.Default);
                    SetStatusBarColor(color ?? Color.Default);
                    if (!modal)
                        await Navigator.PushAsync(targetPage, animated);
                    else
                        await Navigator.PushModalAsync(targetPage, animated);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task NavigateToViewModelAsync(IViewModel viewModel, NavigationAnimation animation, Color? color = null, bool animated = false, bool modal = false)
        {
            try
            {
                AnimationPage targetPage = _pageLocator.ResolvePage(viewModel);
                if (targetPage != null)
                {
                    if (animation != NavigationAnimation.None)
                    {
                        targetPage.PageAnimation = GetPageAnimanition(animation);
                        animated = true;
                    }
                    SetBarBackgroundColor(targetPage, color ?? Color.Default);
                    SetStatusBarColor(color ?? Color.Default);
                    if (!modal)
                        await Navigator.PushAsync(targetPage, animated);
                    else
                        await Navigator.PushModalAsync(targetPage, animated);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task PopAsync(bool animated = false)
        {
            while (Navigator.ModalStack.Count != 0)
            {
                await Navigator.PopModalAsync(false);
            }
            var page = _navigationPage.CurrentPage as AnimationPage;
            if (page != null)
            {
                animated = page.PageAnimation != null;
            }
            await Navigator.PopAsync(animated);
        }

        public async Task PopModalAsync(NavigationAnimation animation, bool animated = false)
        {
            if (Navigator.ModalStack.Count > 0)
            {
                await Navigator.PopModalAsync(animated);
            }
        }

        public async Task PopToRootAsync(bool animated = false)
        {
            while (Navigator.ModalStack.Count != 0)
            {
                await Navigator.PopModalAsync(animated);
            }
            var page = _navigationPage.CurrentPage as AnimationPage;
            if (page != null)
            {
                animated = page.PageAnimation != null;
            }
            await Navigator.PopToRootAsync(animated);
        }

        public void Pop(bool animated = false)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var page = _navigationPage.CurrentPage as AnimationPage;
                if (page != null)
                {
                    animated = page.PageAnimation != null;
                }
                await PopAsync(animated);
            });
        }

        private void SetBarBackgroundColor(Page page, Color color)
        {
            if (page is NavigationPage)
            {
                _navigationPage.SetValue(NavigationPage.BarBackgroundColorProperty, color);
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarBackgroundColor = color;
            }
        }

        public void SetStatusBarColor(Color color)
        {
            DependencyService.Get<IStatusBarProvider>()?.SetStatusBarColor(color);
        }

        private IPageAnimation GetPageAnimanition(NavigationAnimation animation)
        {
            switch (animation)
            {
                case NavigationAnimation.SlideTopToBottom:
                    return new SlidePageAnimation() { Duration = AnimationDuration.Short, Type = AnimationType.Slide, Subtype = AnimationSubtype.FromTop };
                case NavigationAnimation.SlideBottomToTop:
                    return new SlidePageAnimation() { Duration = AnimationDuration.Short, Type = AnimationType.Slide, Subtype = AnimationSubtype.FromBottom };
                case NavigationAnimation.SlideLeftToRight:
                    return new SlidePageAnimation() { Duration = AnimationDuration.Short, Type = AnimationType.Slide, Subtype = AnimationSubtype.FromRight };
                case NavigationAnimation.SlideRightToLeft:
                    return new SlidePageAnimation() { Duration = AnimationDuration.Short, Type = AnimationType.Slide, Subtype = AnimationSubtype.FromLeft };
                default:
                    return null;
            }


        }
    }
}
