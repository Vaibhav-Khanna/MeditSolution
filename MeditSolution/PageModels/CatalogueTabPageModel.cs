using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeditSolution.Models;

namespace MeditSolution.PageModels
{
	public class CatalogueTabPageModel : BasePageModel
    {
		
		public ObservableCollection<Tab> Tabs { get; set; }
		public int SelectedIndex { get; set; } = 0;

		public override void Init(object initData)
		{
			base.Init(initData);

			Tabs = new ObservableCollection<Tab>();
            
			Tab t1;
			Tab t2;
			Tab t3;

			Tabs.Add(t1 = new Tab() { TabName = "Initiation", ColoumnCount = 1, Model = this });
			Tabs.Add(t2 = new Tab() { TabName = "Approndissement", ColoumnCount = 2, Model = this });
			Tabs.Add(t3 = new Tab() { TabName = "Autres", ColoumnCount = 2, Model = this });

			t1.Meditations.Add(new TabMeditationModel() { Image = "pinitiation.png", Tint = "#ebb967", Title = "Initiation", SubTitle = "3 programmes" });
			t1.Meditations.Add(new TabMeditationModel() { Image = "ptraining.png", Tint = "#7aceff", Title = "Entrainement", SubTitle = "3 programmes" });

			t2.Meditations.Add(new TabMeditationModel() { Image = "pbody.png", Tint = "#b8e986", Title = "Corps", SubTitle = "6 programmes" });
			t2.Meditations.Add(new TabMeditationModel() { Image = "pbreath.png", Tint = "#fac9b7", Title = "Respiration", SubTitle = "6 programmes" });
			t2.Meditations.Add(new TabMeditationModel() { Image = "pinstant.png", Tint = "#75e9cf", Title = "Instant présent", SubTitle = "6 programmes" });
			t2.Meditations.Add(new TabMeditationModel() { Image = "pthink.png", Tint = "#d591e3", Title = "Pensées", SubTitle = "6 programmes" });
			t2.Meditations.Add(new TabMeditationModel() { Image = "psound.png", Tint = "#f0e771", Title = "Sons", SubTitle = "6 programmes" });
			t2.Meditations.Add(new TabMeditationModel() { Image = "pemotion.png", Tint = "#6ea5e1", Title = "Émotions", SubTitle = "6 programmes" });

			t3.Meditations.Add(new TabMeditationModel() { Image = "pemergency.png", Tint = "#ea7c89", Title = "Urgences", SubTitle = "6 programmes" });
			t3.Meditations.Add(new TabMeditationModel() { Image = "psee.png", Tint = "#a6da6a", Title = "Visualisations", SubTitle = "6 programmes" });
			t3.Meditations.Add(new TabMeditationModel() { Image = "pheart.png", Tint = "#78eafa", Title = "Cohérence cardiaque", SubTitle = "6 programmes" });
			t3.Meditations.Add(new TabMeditationModel() { Image = "pkids.png", Tint = "#6686a9", Title = "Pour les petits", SubTitle = "6 programmes" });
			t3.Meditations.Add(new TabMeditationModel() { Image = "pgroom.png", Tint = "#fed083", Title = "Pour les plus grands", SubTitle = "6 programmes" });
          
		}


	}
}
