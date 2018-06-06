using System;
using MeditSolution.Models.DataObjects;
using Xamarin.Forms;
using MeditSolution.Helpers;
using MeditSolution.PageModels;
using MeditSolution.Resources;

namespace MeditSolution.Models
{
	[PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CatalogueProgramModel
    {

		public CatalogueProgramModel(TabMeditationModel model, Meditation meditation)
		{
			Model = model;
			Meditation = meditation;
			Tint = model.Tint;
			Title = (meditation.Length.HasValue ? meditation.Length.Value / 60 : 0) + " min";
            
			int seanceCount = 0;

			if (meditation.Level1FrWoman != null)
				seanceCount += 1;
			if (meditation.Level2FrWoman != null)
				seanceCount += 1;
			if (meditation.Level3FrWoman != null)
				seanceCount += 1;

			SubTitle = seanceCount + (  " " + AppResources.seances);

			var user = new BasePageModel().StoreManager.UserStore.User;

			if(model.Program.IsTraining == true || model.Program.IsInitiation == true)
			{
				IsEnabled = true;
			}
			else if(model.Program.AvailableWithSubscription == true)
			{
				IsIncludedInSubscription = true;

				if (user.Subscription == SubscriptionType.free)
				{
					IsEnabled = false;
				}
				else
					IsEnabled = true;
			}
			else
			{
				if (user.Subscription == SubscriptionType.free)
				{
					IsEnabled = false;
				}
				else
				{
					if (model.Program.Price > 0)
					{
						if (user.PaidPrograms != null)
						{
							foreach (var item in user.PaidPrograms)
							{
								if (item.Id == model.Program.Id)
								{
									IsEnabled = true;
									break;
								}
								else
								{
									IsEnabled = false;
								}
							}
						}
						else
							IsEnabled = false;
					}
					else if (model.Program.Price == 0)
					{
						IsEnabled = true;
					}
				}
			}
		}
       
		public bool IsIncludedInSubscription { get; set; }

		public Meditation Meditation { get; set; } 

		public TabMeditationModel Model { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }    

		public bool IsEnabled { get; set; }

		public string Tint { get; set; } = "#ffffff";

		public string TextColor { get { return IsEnabled ? "#ffffff" : ("#9b9b9b"); } }

		public string BackColor { get { return IsEnabled ? Tint : "#ffffff"; } }

		public string BorderColor { get{ return IsEnabled ? Tint : "#d9d9d9"; } }
        
		public string Image { get { return IsEnabled ? "arrow.png" : "lock.png"; } }



    }
}
