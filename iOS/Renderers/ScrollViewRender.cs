using System;
using MeditSolution.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollViewRender))]
namespace MeditSolution.iOS.Renderers
{
	public class ScrollViewRender : ScrollViewRenderer
    {
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
           
			this.ShowsHorizontalScrollIndicator = false;
			this.ShowsVerticalScrollIndicator = false;
		}
	}
}
