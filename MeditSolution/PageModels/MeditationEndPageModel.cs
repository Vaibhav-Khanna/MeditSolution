using System;
using MeditSolution.Resources;
using Xamarin.Forms;
using MeditSolution.Helpers;
using System.Threading.Tasks;
using System.Linq;
using MeditSolution.Models.DataObjects;

namespace MeditSolution.PageModels
{
    public class MeditationEndPageModel : BasePageModel
    {
        public string ImageHeader { get { return IsMeditationEnd ? "unlock.png" : "stones.png"; } }
        public string ButtonNext { get { return IsMeditationEnd ? AppResources.medoverdetail : AppResources.nextseance; } }
        public string ButtonEnd { get { return IsMeditationEnd ? AppResources.explorecatalogue : AppResources.backtocatalogue; } }
        public string Header { get { return IsMeditationEnd ? AppResources.medover : AppResources.congrats; } }
        public string SubHeader { get { return IsMeditationEnd ? "" : AppResources.congratsdetail; } }
        public string TextColor { get { return IsMeditationEnd ? "#9b9b9b" : "#50E3C2"; } }

        //public string Icon { get { return "lock.json"; } }

        public bool IsMeditationEnd;


        public string NextMeditation { get { return IsMeditationEnd ? AppResources.medoverdetail : ""; } }
        public string NextMeditatonName { get; set; }
        public string NextMeditatonDetail { get; set; }

        string meditationID { get; set; }
        string programId { get; set; }
        bool BlockNextAccess { get; set; } = false;

        public async override void Init(object initData)
        {
            base.Init(initData);

            if (initData is bool)
            {

                IsMeditationEnd = (bool)initData;

                if (IsLoading)
                    return;

                IsLoading = true;


                var next = await StoreManager.MeditationStore.GetNextMeditation();

                if (next != null)
                {
                    // meditation completed
                    if (next.otherMeditation != null)
                    {
                        // IsMeditationEnd = true;
                        NextMeditatonName = Settings.DeviceLanguage == "English" ? next.otherMeditation.label_en : next.otherMeditation.label;
                        NextMeditatonDetail = (next.otherMeditation.length / 60) + " min";
                        meditationID = next.otherMeditation._id;
                        programId = next.otherMeditation.programId;

                        //check here for subscription validation
                        var programs = await StoreManager.ProgramStore.GetItemsAsync();

                        var user = await StoreManager.UserStore.GetCurrentUser();

                        if (programs != null && programs.Any() && user.Subscription == Models.DataObjects.SubscriptionType.free)
                        {
                            var isGoodtoGo = programs.Where((arg) => arg.IsInitiation.Value || arg.IsTraining.Value).Select((arg) => arg.Id).Contains(programId);

                            if (!isGoodtoGo)
                            {
                                BlockNextAccess = true;
                            }
                        }
                    }
                    else if (next.levelUp != null) // session completed
                    {
                        //IsMeditationEnd = false;
                        NextMeditatonName = Settings.DeviceLanguage == "English" ? next.levelUp.label_en : next.levelUp.label;
                        NextMeditatonDetail = (next.levelUp.length / 60) + " min";
                        meditationID = next.levelUp._id;
                        programId = next.levelUp.programId;
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            DefaultNavigationBackgroundColor();
                        }

                        MessagingCenter.Send(this, "NextMeditation");
                        await CoreMethods.PopPageModel(null, modal: true);
                    }
                }
                else
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        DefaultNavigationBackgroundColor();
                    }

                    MessagingCenter.Send(this, "NextMeditation");
                    await CoreMethods.PopPageModel(null, modal: true);
                }


                IsLoading = false;
            }
            if (initData is Meditation)
            { // meditation non terminée -> passage à la seance suivante

                Meditation meditationInProgress = (Meditation)initData;
                IsMeditationEnd = false;

                //IsMeditationEnd = false;
                // NextMeditatonName = Settings.DeviceLanguage == "English" ? meditationInProgress.Label_EN : meditationInProgress.Label;
                // NextMeditatonDetail = (meditationInProgress.Length / 60) + " min";
                meditationID = meditationInProgress.Id;
                programId = meditationInProgress.ProgramId;

            }

        }


        public Command NextCommand => new Command(async () =>
        {
            if (BlockNextAccess)
            {
                MessagingCenter.Send(this, "NextMeditation");
                await CoreMethods.PopPageModel(true);
                return;
            }


            if (!string.IsNullOrEmpty(meditationID) && !string.IsNullOrEmpty(programId))
            {
                Dialog.ShowLoading();

                var user = StoreManager.UserStore.User;

                user.CurrentMeditationId = meditationID;
                user.CurrentProgramId = programId;

                await StoreManager.UserStore.UpdateCurrentUser(user);

                Dialog.HideLoading();

                MessagingCenter.Send(this, "NextMeditation");

                await CoreMethods.PopPageModel(true);
            }
        });

        public Command EndCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);
            await CoreMethods.SwitchSelectedTab<CatalogueTabPageModel>();
        });
    }
}
