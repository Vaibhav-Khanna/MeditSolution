using System;
using System.Threading.Tasks;
using MeditSolution.iOS.Services;
using MeditSolution.Service;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(CancelNotifications))]
namespace MeditSolution.iOS.Services
{
    public class CancelNotifications : ICancelNotification
    {

        public void CancelAll()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    UIApplication.SharedApplication.CancelAllLocalNotifications(); 
                }
                catch(Exception ex)
                {
                    
                }

                //UNUserNotificationCenter.Current.RemoveAllPendingNotificationRequests();
            });
        }
    }
}
