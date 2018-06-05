using System;
using MeditSolution.Controls;
using MeditSolution.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(TextFieldRender))]
namespace MeditSolution.iOS.Renderers
{
    public class TextFieldRender : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element!=null )
            {

                var label = (CustomEntry)Element;
                               
                if (Control != null && e.NewElement != null)
                {
                    var element = e.NewElement as CustomEntry;

                    if (element.ReturnButton == ReturnButtonType.Next)
                    {
                        Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;

                        Control.ShouldReturn += (textField) =>
                        {
                            element.OnNext();
                            return false;
                        };
                    }
                    else if (element.ReturnButton == ReturnButtonType.Done)
                    {
                        Control.ReturnKeyType = UIKit.UIReturnKeyType.Go;

                        Control.ShouldReturn += (textField) =>
                        {
                            element.OnDone();
                            return false;
                        };
                    }
                }

            }
        }
    }
}