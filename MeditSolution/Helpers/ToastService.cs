using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MeditSolution.Helpers
{
    public static class ToastService
    {
       
        public static async Task Show(string text)
        {
            var popupPage = new ToastLayout(text,null);

            if(PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAllAsync();
            }

            await PopupNavigation.PushAsync(popupPage);
        }

        public static async Task Show(string text, Color ToastColor)
        {
            var popupPage = new ToastLayout(text, ToastColor);

            if (PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAllAsync();
            }

            await PopupNavigation.PushAsync(popupPage);
        }

        public static async Task Hide()
        {
            if (PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAllAsync();
            }
        }

    }
}
