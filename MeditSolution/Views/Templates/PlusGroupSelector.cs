using System;
using Xamarin.Forms;

namespace MeditSolution.Views.Templates
{
	public class PlusGroupSelector : DataTemplateSelector
    {
		public DataTemplate Group { get; set; }
		public DataTemplate Cell { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
			return (item is string) ? Group : Cell;
        }
    }
}
