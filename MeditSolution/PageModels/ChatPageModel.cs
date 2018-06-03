using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using MeditSolution.Models;
using MeditSolution.Resources;

namespace MeditSolution.PageModels
{
    public class ChatPageModel : BasePageModel
    {
        
        public ObservableCollection<ChatModel> ItemSource { get; set; }
        public bool ShowTypingAnimation { get; set; } = true;
        List<ChatModel> QuestionBank = new List<ChatModel>();


		public override void Init(object initData)
		{
			base.Init(initData);

			ItemSource = new ObservableCollection<ChatModel>();

			QuestionBank.Add(new ChatModel(this) { Question = "Bonjour et bienvenue sur Medit’Solutions. Je serais votre assistante tout au long de vos méditations.", IsQuestion = false });
			QuestionBank.Add(new ChatModel(this) { Question = "Connaissez-vous les bienfaits de la méditation ?", IsQuestion = true, PositiveOption = AppResources.yes, NegativeOption = AppResources.no });
			QuestionBank.Add(new ChatModel(this) { Question = "Êtes-vous un pratiquant régulier ?", IsQuestion = true, PositiveOption = AppResources.yes, NegativeOption = AppResources.no });
			QuestionBank.Add(new ChatModel(this) { Question = "Alors je vous propose de découvrir l’éventail des méditations de Medit’Solutions", IsQuestion = true, PositiveOption = AppResources.letsgo, NegativeOption = AppResources.nothanks });

			ProcessQuestions();
		}

        public async Task AskQuestion(ChatModel Question)
        {
            // Animating the bot
            ShowTypingAnimation = true;
            await Task.Delay(1000);
            ShowTypingAnimation = false;
            //

            ItemSource.Add(Question);
        }

        public Command QuestionAnswered => new Command((question) =>
        {
			if((question as ChatModel).NegativeOption == AppResources.nothanks && !(bool)(question as ChatModel).AnswerIsPositive)
            {
				QuestionBank.Add(new ChatModel(this) { Question = "Je peux vous aider a être plus assidu dans votre pratique ", IsQuestion = true, PositiveOption = AppResources.yes, NegativeOption = AppResources.no }); 
            }

            ProcessQuestions();
        });

        async void ProcessQuestions()
        {
            foreach (var item in QuestionBank)
            {
                if (ItemSource.Contains(item))
                    continue;
                else
                {
                    await AskQuestion(item);

                    if (item.IsQuestion)
                        break;
                }
            }
        }


        public Command SkipCommand => new Command(async() =>
        {
			await CoreMethods.PushPageModel<VideoPlayPageModel>();

			CoreMethods.RemoveFromNavigation<LoginPageModel>(true);
			CoreMethods.RemoveFromNavigation<TutorialPageModel>(true);
		    CoreMethods.RemoveFromNavigation<ChatPageModel>(true);
        });

	}
}
