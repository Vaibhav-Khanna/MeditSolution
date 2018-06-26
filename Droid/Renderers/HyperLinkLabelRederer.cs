using Android.Content;
using Android.Text.Util;
using MeditSolution.Controls;
using MeditSolution.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperlinkLabelRenderer))]
namespace MeditSolution.Droid.Renderers
{
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        public HyperlinkLabelRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            Linkify.AddLinks(Control, MatchOptions.All);
        }
    }
}
