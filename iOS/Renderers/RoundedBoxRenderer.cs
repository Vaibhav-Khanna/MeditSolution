using System;
using System.ComponentModel;
using MeditSolution.Controls;
using MeditSolution.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxRenderer))]
namespace MeditSolution.iOS.Renderers
{
	public class RoundedBoxRenderer : BoxRenderer
    {
        
        private RoundedBoxView _formControl
        {
            get { return Element as RoundedBoxView; }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
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
