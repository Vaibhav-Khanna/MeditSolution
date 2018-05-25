using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using MeditSolution.Controls;
using MeditSolution.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
namespace MeditSolution.Droid.Renderers
{
	public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, View>
	{
		public RoundedBoxViewRenderer(Context context):base(context)
		{
			
		}

		private RoundedBoxView _formControl
		{
			get { return Element; }
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
		{
			base.OnElementChanged(e);

			this.InitializeFrom(_formControl);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			this.UpdateFrom(_formControl, e.PropertyName);
		}
	}
}
