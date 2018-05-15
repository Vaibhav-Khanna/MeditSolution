using System;
using System.ComponentModel;
using Android.Content;
using MeditSolution.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(ScrollView),typeof(DefaultScrollViewRenderer))]
namespace MeditSolution.Droid.Renderers
{
	public class DefaultScrollViewRenderer : ScrollViewRenderer
    {

        public DefaultScrollViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;

        }

		private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{


			if (ChildCount > 0)
			{
				GetChildAt(0).HorizontalScrollBarEnabled = false;
				GetChildAt(0).VerticalScrollBarEnabled = false;
			}

		}

    }
}
