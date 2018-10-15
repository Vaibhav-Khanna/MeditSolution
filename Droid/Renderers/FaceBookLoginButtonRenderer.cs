//using System;
//using Android.App;
//using Android.Content;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using Object = Java.Lang.Object;
//using View = Android.Views.View;
////using Xamarin.Facebook;
//using MeditSolution.Controls;
//using MeditSolution;
//using MeditSolution.Droid;

//[assembly: ExportRenderer(typeof(FaceBookLoginButton), typeof(FacebookLoginButtonRenderer))]

//namespace MeditSolution.Droid
//{
//    public class FacebookLoginButtonRenderer : ButtonRenderer
//    {
//        private static Activity _activity;

//        public FacebookLoginButtonRenderer(Context context) : base(context)
//        {
            
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
//        {
//            base.OnElementChanged(e);

//            _activity = this.Context as Activity;

//            //DEBUG
//            //Xamarin.Facebook.Login.LoginManager.Instance.LogOut();

//            if (this.Control != null)
//            {
//                (Control as Android.Widget.Button)?.SetAllCaps(false);
//                (Control as Android.Widget.Button)?.SetPadding(70, 0, 70, 0);


//                Android.Widget.Button button = this.Control;
//                button.SetOnClickListener(ButtonClickListener.Instance.Value);
//            }

//            if (AccessToken.CurrentAccessToken != null)
//            {
//                App.PostSuccessFacebookAction(AccessToken.CurrentAccessToken.Token);
//            }
//        }

//        private class ButtonClickListener : Object, IOnClickListener
//        {
//            public static readonly Lazy<ButtonClickListener> Instance = new Lazy<ButtonClickListener>(() => new ButtonClickListener());

//            public void OnClick(View v)
//            {
//                var myIntent = new Intent(_activity, typeof(MeditSolution.Droid.FacebookActivity));
//                _activity.StartActivityForResult(myIntent, 0);
//            }
//        }
//    }
//}