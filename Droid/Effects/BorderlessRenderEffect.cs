using System;
using MeditSolution.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportEffect(typeof(BorderlessRenderEffect), "BorderlessEffect")]
namespace MeditSolution.Droid.Effects
{
	public class BorderlessRenderEffect : PlatformEffect
    {
       
		protected override void OnAttached()
		{
			if(Control!=null)
			{
				Control.Background = null;
                
			}
		}

		protected override void OnDetached()
		{
			throw new NotImplementedException();
		}
	}
}
