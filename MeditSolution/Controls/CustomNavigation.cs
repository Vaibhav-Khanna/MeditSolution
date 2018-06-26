using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BottomBar.XamarinForms;
using FreshMvvm;
using Xamarin.Forms;
using MeditSolution.Helpers;
using MeditSolution.Resources;
using MeditSolution.Service;

namespace MeditSolution.Controls
{
 

    public class CustomNavigation : BottomBarPage, IFreshNavigationService
    {
      
		List<Page> _tabs = new List<Page>();
        public IEnumerable<Page> TabbedPages { get { return _tabs; } }

		public CustomNavigation() : this(FreshMvvm.Constants.DefaultNavigationServiceName)
        {
			NavigationPage.SetHasNavigationBar(this, false);
        }

        public CustomNavigation(string navigationServiceName)
        {
			NavigationPage.SetHasNavigationBar(this, false);
            NavigationServiceName = navigationServiceName;
            RegisterNavigation();
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public virtual Page AddTab<T>(string title, string icon, object data = null) where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            _tabs.Add(page);
          
			var navigationContainer = CreateContainerPageSafe(page);
            navigationContainer.Title = title;
            
			if (!string.IsNullOrWhiteSpace(icon))
                navigationContainer.Icon = icon;
           
			Children.Add(navigationContainer);
            return navigationContainer;
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
			return new DynamicNavigationPage(page);
        }

        public System.Threading.Tasks.Task PushPage(Xamarin.Forms.Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
				return this.CurrentPage.Navigation.PushModalAsync(CreateContainerPageSafe(page),Device.RuntimePlatform == Device.iOS);
           
			return animate ? this.CurrentPage.Navigation.PushAsync(page) : this.Navigation.PushAsync(page);
        }

        public System.Threading.Tasks.Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
				return this.CurrentPage.Navigation.PopModalAsync(Device.RuntimePlatform == Device.iOS);
			
			return animate ? this.CurrentPage.Navigation.PopAsync(true) : this.Navigation.PopAsync(true);
        }

        public Task PopToRoot(bool animate = true)
        {
			return this.CurrentPage.Navigation.PopToRootAsync(animate);
        }

        public string NavigationServiceName { get; private set; }

        public void NotifyChildrenPageWasPopped()
        {
            foreach (var page in this.Children)
            {
                if (page is NavigationPage)
                    ((NavigationPage)page).NotifyAllChildrenPopped();
            }
        }

        bool isWaitingForBack { get; set; }

        protected override bool OnBackButtonPressed()
        {
            if(this.CurrentPage.Navigation.NavigationStack.Count == 1 && this.CurrentPage.Navigation?.ModalStack?.Count == 0)
            {
                if(isWaitingForBack)
                {
                    DependencyService.Get<ICloseApp>().CloseApp(false);
                }

                isWaitingForBack = true;

                Device.StartTimer(new TimeSpan(0, 0, 4), () =>
                   {                 
                       isWaitingForBack = false;
                       return false;
                   });

                ToastService.Show(AppResources.backbutton);
                return true; 
            }
            else
            {
                return base.OnBackButtonPressed();   
            }
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            var page = _tabs.FindIndex(o => o.GetModel().GetType().FullName == typeof(T).FullName);

            if (page > -1)
            {
                CurrentPage = this.Children[page];
                var topOfStack = CurrentPage.Navigation.NavigationStack.LastOrDefault();
                if (topOfStack != null)
                    return Task.FromResult(topOfStack.GetModel());

            }
            return null;
        }
    }
}