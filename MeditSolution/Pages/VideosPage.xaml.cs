using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class VideosPage : BasePage
    {
        public VideosPage()
        {
            InitializeComponent();
        }

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			(BindingContext as VideosPageModel).PlayCommand.Execute(e.Item);
			list.SelectedItem = null;
		}


    }
}
