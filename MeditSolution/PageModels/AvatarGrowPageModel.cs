using System;
using MeditSolution.Models.DataObjects;
using MeditSolution.Resources;
using Xamarin.Forms;

namespace MeditSolution.PageModels
{
    public class AvatarGrowPageModel : BasePageModel
    {

        User user;
        public string DaysMedidated { get; set; }

        public string Evolution { get; set; }
        public string Icon { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            user = StoreManager.UserStore.User;

            Evolution = AppResources.evolution + " " + (user.CurrentLevel + 1);

            DaysMedidated = AppResources.bravodetail2_1 + " " + user.RecordMaxDaysMeditation + " " + AppResources.bravodetail2_2;

            Icon = "level" + (user.CurrentLevel + 1) + ".json";

        }

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);
        });
    }
}
