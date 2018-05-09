using System;
using MeditSolution.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ListView),typeof(ListViewRender))]
namespace MeditSolution.iOS.Renderers
{
	public class ListViewRender : ListViewRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if(Control!=null)
			this.Control.ShowsVerticalScrollIndicator = false;
		}
	}
}
