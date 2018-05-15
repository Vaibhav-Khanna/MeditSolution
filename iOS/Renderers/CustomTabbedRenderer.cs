using System;
using MeditSolution.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(TabbedPage),typeof(CustomTabbedRenderer))]
namespace MeditSolution.iOS.Renderers
{
	public class CustomTabbedRenderer : TabbedRenderer
    {
		
		public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;

            // Go through our elements and change the icons
            var tabs = Element as TabbedPage;
            
			if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item, string icon)
        {
            if (item == null || icon == null)
                return;
                         
            // Set the font for the title.
            //item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("MuseoSans-500", 10), TextColor = Color.FromHex("#757575").ToUIColor() }, UIControlState.Normal);
            //item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("MuseoSans-500", 10), TextColor = Color.FromHex("#2dc9d7").ToUIColor() }, UIControlState.Selected);

            // Moves the titles up just a bit.
            item.TitlePositionAdjustment = new UIOffset(0, -3);
        }
    }
}
