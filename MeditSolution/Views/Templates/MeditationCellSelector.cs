using System;
using Xamarin.Forms;

namespace MeditSolution.Views.Templates
{
	public class MeditationCellSelector : DataTemplateSelector
	{
		public DataTemplate ConnectingLine { get; set; }
        public DataTemplate RoundedView { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
			return  (item is string) ? ConnectingLine : RoundedView;
        }
	}
}
