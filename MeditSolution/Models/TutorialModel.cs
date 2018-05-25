using System;
namespace MeditSolution.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class TutorialModel
    {
        public string Image { get; set; }

        public string Text { get; set; }
    }
}
