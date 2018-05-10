using System;
using Android.OS;
using Android.Views;
using MeditSolution.Droid.Service;
using MeditSolution.Service;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(AndroidStatusBarColor))]
namespace MeditSolution.Droid.Service
{
	public class AndroidStatusBarColor : IAndroidStatusBarColor
	{

		public void ChangeColor(Color color,bool selfDark = false)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				if (Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity != null)
				{
					Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
					if (selfDark)
						Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(ColorUtil.darken(color.ToAndroid(),12));
					else
						Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(color.ToAndroid());
				}
			}
		}
	}

	public class ColorUtil
	{

		public static Android.Graphics.Color darken(Android.Graphics.Color baseColor, int amount)
		{
			float[] hsv = new float[3];

			Android.Graphics.Color.ColorToHSV(baseColor, hsv);

			float[] hsl = hsv2hsl(hsv);
            
			hsl[2] -= amount / 100f;
			if (hsl[2] < 0)
				hsl[2] = 0f;

			hsv = hsl2hsv(hsl);

			return Android.Graphics.Color.HSVToColor(hsv);
		}

		private static float[] hsv2hsl(float[] hsv)
		{
			float hue = hsv[0];
			float sat = hsv[1];
			float val = hsv[2];


			float nhue = (2f - sat) * val;
			float nsat = sat * val / (nhue < 1f ? nhue : 2f - nhue);
			if (nsat > 1f)
				nsat = 1f;

			return new float[]{
                //[hue, saturation, lightness]
                //Range should be between 0 - 1
                hue, //Hue stays the same

                // check nhue and nsat logic
                nsat,

				nhue / 2f //Lightness is (2-sat)*val/2
                //See reassignment of hue above
        };
		}
        
		private static float[] hsl2hsv(float[] hsl)
		{
			float hue = hsl[0];
			float sat = hsl[1];
			float light = hsl[2];

			sat *= light < .5 ? light : 1 - light;

			return new float[]{
                //[hue, saturation, value]
                //Range should be between 0 - 1

                hue, //Hue stays the same
                2f * sat / (light + sat), //Saturation
                light + sat //Value
               };
		}
	}
}