using System;
using Xamarin.Forms;
using MeditSolution.PageModels;
using MeditSolution.Resources;

namespace MeditSolution.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ChatModel
    {
        public ChatModel(ChatPageModel model)
        {
            Model = model;
        }

		public int ID { get; set; }

        ChatPageModel Model { get; set; }

		public int NextQuestionIfPositive { get; set; }

		public int NextQuestionIfNegative { get; set; }

        public bool IsQuestion { get; set; }

		public string ActionId { get; set; }

        public string Question { get; set; }

		public string PositiveOption { get; set; } = AppResources.yes;

		public string NegativeOption { get; set; } = AppResources.no;

        public bool? AnswerIsPositive { get; set; } = null;

        public Command AnswerCommand => new Command((str) =>
        {
            if (AnswerIsPositive != null)
                return;

            if ((str as string) == PositiveOption)
                AnswerIsPositive = true;
            else
                AnswerIsPositive = false;

            Model.QuestionAnswered.Execute(this);
        });

    }
}
