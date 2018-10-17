using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using MeditSolution.Models;
using MeditSolution.Resources;
using MeditSolution.Helpers;
using MeditSolution.Controls;

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

			Settings.HasToCompleteChat = true;

			ItemSource = new ObservableCollection<ChatModel>();
            ItemSource.Add(new ChatModel(this) { Question = AppResources.MeditationChat0, IsQuestion = false } );

			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat1, ID = 1, NextQuestionIfNegative = 2, NextQuestionIfPositive = 2, IsQuestion = true });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat2, ID = 2, IsQuestion = true, NextQuestionIfPositive = 4, NextQuestionIfNegative = 3, });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat3, ID = 3, IsQuestion = false, ActionId = "1" });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat4, ID = 4, IsQuestion = true, NextQuestionIfNegative = 5, NextQuestionIfPositive = 7 });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat5, ID = 5, IsQuestion = true, NextQuestionIfNegative = 6, NextQuestionIfPositive = -1, ActionId = "2" });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat6, ID = 6, IsQuestion = false, ActionId = "3" });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat7, ID = 7, IsQuestion = true, NextQuestionIfNegative = 8, NextQuestionIfPositive = -1, ActionId = "4" });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat8, ID = 8, IsQuestion = true, NextQuestionIfPositive = 10, NextQuestionIfNegative = 9 });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat9, ID = 9, IsQuestion = false, ActionId = "5" });
			QuestionBank.Add(new ChatModel(this) { Question = AppResources.MeditationChat10, ID = 10, IsQuestion = false, ActionId = "4" });

			ProcessQuestions(null);
		}

		public async Task AskQuestion(ChatModel Question)
		{
			// Animating the bot
			ShowTypingAnimation = true;
			await Task.Delay(1000);
			ShowTypingAnimation = false;
			//

			ItemSource.Add(Question);

			if (!Question.IsQuestion)
			{
				// perfrom action based on action id
				ShowTypingAnimation = true;
				await Task.Delay(2000);
				ShowTypingAnimation = false;

				PerformAction(Question.ActionId);
				return;
			}
		}

		public Command QuestionAnswered => new Command((question) =>
		{
			ProcessQuestions(question as ChatModel);
		});

		async void ProcessQuestions(ChatModel previousQuestion)
		{
			if (previousQuestion == null)
			{
				await AskQuestion(QuestionBank.First());
				return;
			}

			if (previousQuestion.AnswerIsPositive.Value)
			{
				if (previousQuestion.NextQuestionIfPositive == -1)
				{
					// perfrom action based on action id
					PerformAction(previousQuestion.ActionId);
				}
				else
					await AskQuestion(QuestionBank.Where((arg) => arg.ID == previousQuestion.NextQuestionIfPositive).First());
			}
			else
			{
				await AskQuestion(QuestionBank.Where((arg) => arg.ID == previousQuestion.NextQuestionIfNegative).First());
			}
		}

		public Command SkipCommand => new Command(async () =>
		{
			await CoreMethods.PushPageModel<VideoPlayPageModel>();

			CoreMethods.RemoveFromNavigation<LoginPageModel>(true);
			CoreMethods.RemoveFromNavigation<TutorialPageModel>(true);
			CoreMethods.RemoveFromNavigation<ChatPageModel>(true);
		});

		async void PerformAction(string actionID)
		{
			Dialog.ShowLoading();

			switch (actionID)
			{
				case "1": // welcome video,Initiate
					{
						await SetInitiationMeditation(true);

						await CoreMethods.PushPageModel<VideoPlayPageModel>();

						Settings.HasToCompleteChat = false;

						break;
					}
				case "2": // commentmediter,Initiation
					{
						await SetInitiationMeditation(true);

                        //var videos = await StoreManager.VideoStore.GetItemsAsync();

                        //var video = videos?.Where((arg) => arg.Name_EN?.ToLower()?.Trim() == "how to meditate")?.First();

                        //if(video!=null)
                        //{
                        //	await CoreMethods.PushPageModel<VideoPlayPageModel>(new Tuple<bool, VideoModel>(true,new VideoModel(video)));
                        //}

                        await CoreMethods.PushPageModel<VideoPlayPageModel>();

                        Settings.HasToCompleteChat = false;

						break;
					}
				case "3": // pourquoiMediter,initiation
					{
						await SetInitiationMeditation(true);

                        //var videos = await StoreManager.VideoStore.GetItemsAsync();

                        //                  var video = videos?.Where((arg) => arg.Name_EN?.ToLower()?.Trim() == "why meditate")?.First();

                        //if (video != null)
                        //{
                        //    await CoreMethods.PushPageModel<VideoPlayPageModel>(new Tuple<bool, VideoModel>(true, new VideoModel(video)));
                        //}

                        await CoreMethods.PushPageModel<VideoPlayPageModel>();

                        Settings.HasToCompleteChat = false;

						break;
					}
				case "4": // No video,Training
					{
						await SetInitiationMeditation(false);

                        await CoreMethods.PushPageModel<VideoPlayPageModel>();

                        Settings.HasToCompleteChat = false;

						//Device.BeginInvokeOnMainThread(() =>
						//{
						//	Application.Current.MainPage = TabNavigator.GenerateTabPage();
						//});

						break;
					}
				case "5": //Open Reminders,training
					{
						await SetInitiationMeditation(false);

						App.OpenReminders = true;

                        await CoreMethods.PushPageModel<VideoPlayPageModel>();

                        Settings.HasToCompleteChat = false;

						//Device.BeginInvokeOnMainThread(() =>
						//{
						//	Application.Current.MainPage = TabNavigator.GenerateTabPage();
						//});

						break;
					}
				default:
					break;
			}

			Dialog.HideLoading();
		}

		async Task SetInitiationMeditation(bool IsInitiate)
		{
			var programs = await StoreManager.ProgramStore.GetItemsAsync();

            programs = programs?.OrderBy((arg) => arg.programOrder);

			var program = IsInitiate ? programs?.Where((arg) => arg.IsInitiation.HasValue && arg.IsInitiation.Value)?.First() : programs?.Where((arg) => arg.IsTraining.HasValue && arg.IsTraining.Value)?.First();

            if (program != null)
            {
                var meditations = await StoreManager.MeditationStore.GetMeditationsByProgramId(program.Id);

                var user = StoreManager.UserStore.User;
                user.CurrentProgramId = meditations?.First()?.ProgramId;
                user.CurrentMeditationId = meditations?.First().Id;
                               
				await StoreManager.UserStore.UpdateCurrentUser(user);
            }
		}
	}
}
