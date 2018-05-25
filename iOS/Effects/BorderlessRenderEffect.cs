using System;
using MeditSolution.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using MeditSolution.iOS.Effects;

[assembly: Xamarin.Forms.ResolutionGroupName("medit")]
[assembly: Xamarin.Forms.ExportEffect(typeof(BorderlessRenderEffect), "BorderlessEffect")]
namespace MeditSolution.iOS.Effects
{
	public class BorderlessRenderEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			if(Control!=null)
			{
				(Control as UITextField).BorderStyle = UITextBorderStyle.None;
			}	
		}

		protected override void OnDetached()
		{
			
		}
	}
}
