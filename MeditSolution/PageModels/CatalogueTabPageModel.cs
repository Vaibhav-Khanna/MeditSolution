using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using MeditSolution.Models.DataObjects;
using MeditSolution.Helpers;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
	public class CatalogueTabPageModel : BasePageModel
    {
		
		public ObservableCollection<Tab> Tabs { get; set; }
		IEnumerable<Program> programs;
		public int SelectedIndex { get; set; } = 0;
		Tab t1;
        Tab t2;
        Tab t3;

		public override void Init(object initData)
		{
			base.Init(initData);

			Tabs = new ObservableCollection<Tab>();
                            
			Tabs.Add(t1 = new Tab() { TabName = AppResources.Initiate, ColoumnCount = 1, Model = this });
			Tabs.Add(t2 = new Tab() { TabName = AppResources.subscribedprograms, ColoumnCount = 2, Model = this });
			Tabs.Add(t3 = new Tab() { TabName = AppResources.otherprograms, ColoumnCount = 2, Model = this });   
		}

		protected async override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);                      

			if (programs == null)
			{
				if (IsLoading)
					return;
				
				IsLoading = true;

				programs = await StoreManager.ProgramStore.GetItemsAsync();

                if (programs != null)
                    foreach (var item in programs)
                    {
                        if (item.IsInitiation == true || item.IsTraining == true)
                        {
                            t1.Meditations.Add(new TabMeditationModel(item) { Level = 1 });
                        }
                        else if (item.AvailableWithSubscription == true)
                        {
                            t2.Meditations.Add(new TabMeditationModel(item) { Level = 2 });
                        }
                        else
                        {
                            t3.Meditations.Add(new TabMeditationModel(item) { Level = 3 });
                        }
                    }
				else
					await ToastService.Show(AppResources.requestfailed);

				IsLoading = false;
			}            
		}

	}
}
