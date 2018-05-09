using System;
using System.ComponentModel;
using Android.Content;
using Android.Widget;
using MeditSolution.Droid.Effects;
using MeditSolution.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Xamarin.Forms.Button),typeof(RoundButtonRenderer))]
namespace MeditSolution.Droid.Effects
{
	public class RoundButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
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
				(Control as Android.Widget.Button).Elevation = 0;
            }
		}
  

	}
}
