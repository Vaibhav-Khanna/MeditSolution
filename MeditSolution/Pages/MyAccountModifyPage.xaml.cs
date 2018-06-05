using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MeditSolution.Pages
{
	public partial class MyAccountModifyPage : BasePage
    {
        public MyAccountModifyPage()
        {
            InitializeComponent();

			etname.ReturnButton = Controls.ReturnButtonType.Next;
			etlastname.ReturnButton = Controls.ReturnButtonType.Next;
			etemail.ReturnButton = Controls.ReturnButtonType.Next;
			etpassword.ReturnButton = Controls.ReturnButtonType.Next;
			etconpass.ReturnButton = Controls.ReturnButtonType.Done;

			etname.NextView = etlastname;
			etlastname.NextView = etemail;
			etemail.NextView = etpassword;
			etpassword.NextView = etconpass;
        }
    }
}
