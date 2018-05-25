using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
	public class RemindersPageModel : BasePageModel
    {
		public ObservableCollection<ReminderModel> Reminders { get; set; } = new ObservableCollection<ReminderModel>();

		public override void Init(object initData)
		{
			base.Init(initData);

			Reminders.Add(new ReminderModel());
			Reminders.Add(new ReminderModel());
		}

		public Command AddCommand => new Command(() =>
		{
			Reminders.Add(new ReminderModel());
		});
	}
}
