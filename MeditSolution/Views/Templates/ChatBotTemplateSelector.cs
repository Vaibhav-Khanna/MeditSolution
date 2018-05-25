using System;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.Views.Templates
{
    public class ChatBotTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BotTemplate { get; set; }
        public DataTemplate PersonTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ChatModel)item).IsQuestion ? PersonTemplate : BotTemplate;
        }
    }
}
