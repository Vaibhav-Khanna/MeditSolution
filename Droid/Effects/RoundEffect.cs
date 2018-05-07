using System;
using Android.Content;
using Android.Widget;
using MeditSolution.Droid.Effects;
using MeditSolution.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Xamarin.Forms.Button),typeof(RoundButtonRenderer))]
namespace MeditSolution.Droid.Effects
{
    public class RoundButtonRenderer : ButtonRenderer
    {
        public RoundButtonRenderer(Context context) : base(context)
        {
            
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
            base.OnElementChanged(e);

            if (Control != null)
            {
                (Control as Android.Widget.Button)?.SetAllCaps(false);
                (Control as Android.Widget.Button)?.SetPadding(0, 0, 0, 0);
            }
		}
	}
}
