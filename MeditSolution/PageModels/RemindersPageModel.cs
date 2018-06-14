using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;
using MeditSolution.Helpers;
using Newtonsoft.Json;
using Plugin.Notifications;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeditSolution.Resources;
using MeditSolution.Service;

namespace MeditSolution.PageModels
{
	public class RemindersPageModel : BasePageModel
    {
		public ObservableCollection<ReminderModel> Reminders { get; set; } = new ObservableCollection<ReminderModel>();
        INotifications Notifications => CrossNotifications.Current;

        public override void Init(object initData)
        {
            base.Init(initData);

            if (string.IsNullOrEmpty(Settings.ReminderStorage))
            {
                Settings.ReminderStorage = JsonConvert.SerializeObject(Reminders.ToList());
            }

            var list = JsonConvert.DeserializeObject<List<ReminderModel>>(Settings.ReminderStorage);

            foreach (var item in list)
            {
                item.Model = this;
            }

            Reminders = new ObservableCollection<ReminderModel>(list);
        }

		public Command AddCommand => new Command(() =>
		{
            Reminders.Add(new ReminderModel(){ Model = this });
            Settings.ReminderStorage = JsonConvert.SerializeObject(Reminders.ToList());
		});

        public Command DeleteCommand => new Command((obj) =>
        {
            Dialog.ShowLoading();

            var model = obj as ReminderModel;

            Reminders.Remove(model);
           
            Settings.ReminderStorage = JsonConvert.SerializeObject(Reminders.ToList());

            Dialog.HideLoading();
        });

        protected async override void ViewIsDisappearing(object sender, EventArgs e)
        {
            await ScheduleReminders();

            Settings.ReminderStorage = JsonConvert.SerializeObject(Reminders.ToList());

            base.ViewIsDisappearing(sender, e);
        }

        public async Task ScheduleReminders()
        {
             
            if(Device.RuntimePlatform== Device.iOS)
            {
                DependencyService.Get<ICancelNotification>()?.CancelAll();
            }
            else
            await Notifications.CancelAll();

            foreach (var reminder in Reminders)
            {
                var time = (reminder.Time);

                if (reminder.IsActive)
                {
                    DateTime startDate = DateTime.Now.Date;
                    DateTime endDate = DateTime.Now.AddMonths(1);

                    startDate = startDate + time;

                    for (int i = 0; i < endDate.Date.Subtract(startDate.Date).Days; i++)
                    {
                        DateTime item = startDate.AddDays(i);

                        bool isMonday = reminder.Monday.IsEnabled && item.DayOfWeek == reminder.Monday.Day;
                        bool isTuesday = reminder.Tuesday.IsEnabled && item.DayOfWeek == reminder.Tuesday.Day;
                        bool isWednesday = reminder.Wednesday.IsEnabled && item.DayOfWeek == reminder.Wednesday.Day;
                        bool isThursday = reminder.Thursday.IsEnabled && item.DayOfWeek == reminder.Thursday.Day;
                        bool isFriday = reminder.Friday.IsEnabled && item.DayOfWeek == reminder.Friday.Day;
                        bool isSaturday = reminder.Saturday.IsEnabled && item.DayOfWeek == reminder.Saturday.Day;
                        bool isSunday = reminder.Sunday.IsEnabled && item.DayOfWeek == reminder.Sunday.Day;

                        if (isMonday || isTuesday || isWednesday || isThursday || isFriday || isSaturday || isSunday)
                        {
                            if (item.CompareTo(DateTime.Now) > 0)
                            {
                                var notification = new Notification() { Id = GetUniqueID(item,Reminders.IndexOf(reminder)), Title = AppResources.NotificationHeader, Message = AppResources.NotificationContent, Date = item, Sound = "notification", Vibrate = false };

                                if (Device.RuntimePlatform == Device.iOS)
                                    notification.Sound = "notification.caf";

                                await Notifications.Send(notification);
                            }
                        }
                    }
                }
            }

        }

        int GetUniqueID(DateTime time, int modelid)
        {
            Int32 unixTimestamp = (Int32)(time.Date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp - modelid;
        }


    }
}
