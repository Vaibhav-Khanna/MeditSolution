using System;
using Xamarin.Forms;
using MeditSolution.PageModels;

namespace MeditSolution.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ChatModel
    {
        public ChatModel(ChatPageModel model)
        {
            Model = model;
        }

        ChatPageModel Model { get; set; }

        public bool IsQuestion { get; set; }

        public string Question { get; set; }

        public string PositiveOption { get; set; }

        public string NegativeOption { get; set; }

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
