using System;
using Foundation;
using MeditSolution.Controls;
using MeditSolution.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperlinkLabelRenderer))]
namespace MeditSolution.iOS.Renderers
{
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            Control.UserInteractionEnabled = true;

            Control.TextColor = UIColor.Blue;

            var gesture = new UITapGestureRecognizer();

            gesture.AddTarget(() =>
            {
                var url = new NSUrl(Control.Text);

                if (UIApplication.SharedApplication.CanOpenUrl(url))
                    UIApplication.SharedApplication.OpenUrl(url);
            });

            Control.AddGestureRecognizer(gesture);
        }
    }
}
