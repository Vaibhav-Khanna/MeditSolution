using System;
using Xamarin.Forms;

namespace MeditSolution.Service
{
    public interface IAndroidStatusBarColor
    {
		void ChangeColor(Color color,bool selfDark = false);
    }
}
