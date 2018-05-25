using System;
using Android.Widget;
using MeditSolution.Droid.Effects;
using MeditSolution.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ResolutionGroupName("medit")]
[assembly: Xamarin.Forms.ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace MeditSolution.Droid.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null)
            {
                (Control as EditText).Background = Control.Context.Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry);
                (Control as EditText).SetPadding(50, 0, 0, 0);
                (Control as EditText).Gravity = Android.Views.GravityFlags.CenterVertical;
            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}
