using System;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.Views.Templates
{
    public class CatalogueDetailSelector : DataTemplateSelector
    {
        public DataTemplate OldTemplate { get; set; }
        public DataTemplate NewTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((CatalogueProgramModel)item).Model.Level == 2 ? OldTemplate : NewTemplate;
        }
    }
}
