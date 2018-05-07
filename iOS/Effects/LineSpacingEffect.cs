using System;
using MeditSolution.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;

//[assembly: Xamarin.Forms.ResolutionGroupName("MeditSolution")]
//[assembly: Xamarin.Forms.ExportEffect(typeof(LineSpacingEffect), "LineSpacingEffect")]
namespace MeditSolution.iOS.Effects
{
    public class LineSpacingEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            //(Control as UILabel);
            var paragraphStyle = new NSMutableParagraphStyle()
            {
                LineSpacing = 15
            };

            var strin = new NSMutableAttributedString((Element as Label).Text);
            var style = UIStringAttributeKey.ParagraphStyle;
            var range = new NSRange(0, strin.Length);

            strin.AddAttribute(style, paragraphStyle, range);

            (Control as UILabel).AttributedText = strin;
        }

        protected override void OnDetached()
        {
            
        }
    }
}
