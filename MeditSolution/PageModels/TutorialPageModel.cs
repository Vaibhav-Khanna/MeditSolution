using System;
using System.Collections.ObjectModel;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
    public class TutorialPageModel : BasePageModel
    {
        public ObservableCollection<TutorialModel> ItemsSource { get; set; }

        public TutorialPageModel()
        {
            ItemsSource = new ObservableCollection<TutorialModel>();

            ItemsSource.Add(new TutorialModel() { Image = "logo.png", Text ="" });
            ItemsSource.Add(new TutorialModel() { Image = "vector1.png", Text = "2 langues, 4 voix, + de 150 méditations dans le pur respect de la Pleine Conscience. Un parcours pédagogique pour vous emmener progressivement vers l'autonomie dans votre pratique" });
            ItemsSource.Add(new TutorialModel() { Image = "vector2.png", Text = "La cohérence cardiaque pour entraîner votre organisme à mieux faire face aux situations de stress, de douleurs et d'émotions négatives" });
            ItemsSource.Add(new TutorialModel() { Image = "vector3.png", Text = "Une section pour les enfants, petits et grands, pour les emmener vers l'autorégulation de leurs émotions" });
            ItemsSource.Add(new TutorialModel() { Image = "vector4.png", Text = "Un assistant personnel qui vous guide sur votre chemin et un avatar qui évolue avec vous; il est le reflet de votre propre évolution​" }); 
        }

        public Command LoginCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<LoginPageModel>(true,true,Device.RuntimePlatform == Device.iOS);
        });


        public Command SignUpCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<LoginPageModel>(false,true,Device.RuntimePlatform == Device.iOS);
        });
	}
}
