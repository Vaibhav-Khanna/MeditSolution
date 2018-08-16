using System;
using System.Collections.Generic;
using MeditSolution.PageModels;
using SkiaSharp;
using MeditSolution.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace MeditSolution.Pages
{
	public partial class SubscriptionPage : BasePage
    {

        public readonly string terms_EN = "Payment will be charged to iTunes Account at confirmation of purchase." + Environment.NewLine + Environment.NewLine + "Account will be charged for renewal within 24-hours prior to the end of the current period, and identify the cost of the renewal" + Environment.NewLine + Environment.NewLine + "Subscription automatically renews unless auto-renew is turned off at least 24-hours before the end of the current period" + Environment.NewLine + Environment.NewLine + "Subscriptions may be managed by the user and auto-renewal may be turned off by going to the user's Account Settings after purchase" + Environment.NewLine + Environment.NewLine + "Any unused portion of a free trial period, if offered, will be forfeited when the user purchases a subscription to that publication, where applicable" + Environment.NewLine + Environment.NewLine + "Note that if you buy a subscription through Apple iTunes, the payment is final, without refund(excluding withdrawal period). Your purchase is subject to the Apple Media Services Terms and Conditions https://www.apple.com/legal/internet-services/itunes/en/terms.html which defines the refund policy." + Environment.NewLine + Environment.NewLine + "For more details on the General Terms of Use, visit the CGU page of our website https://medit-solutions.com/cgu. " + Environment.NewLine + Environment.NewLine + "And if you have any questions about this, the MéditSolutions team is always there (with a smile) for your questions: contact@medit-solutions.com.";


        public readonly string terms_FR = "Le paiement sera débité du compte iTunes dès la confirmation de l'achat." + Environment.NewLine + Environment.NewLine + "24 heures avant la fin de période en cours, le compte sera facturé du montant identifié pour le renouvellement." + Environment.NewLine + Environment.NewLine + "L'abonnement est automatiquement renouvelé, sauf si le renouvellement automatique est désactivé au moins 24 heures avant la fin de la période en cours." + Environment.NewLine + Environment.NewLine +
                                                                                                                                 "Les abonnements peuvent être gérés par l'utilisateur. Le renouvellement automatique peut être désactivé en accédant aux paramètres du compte de l'utilisateur après l'achat." +
                                                                                                                                 Environment.NewLine + Environment.NewLine + "Tout le temps inutilisé, d'une période d'essai gratuite, sera perdu dès lors que l'utilisateur achète un abonnement." + Environment.NewLine + Environment.NewLine + "Notez que si vous achetez un abonnement via l'application d'Apple, iTunes, le paiement est définitif, sans remboursement (hors délai de rétractation). Votre achat est soumis aux conditions d'utilisation d'Apple Media Services https://www.apple.com/legal/internet-services/itunes/fr/terms.html qui définissent la politique de remboursement." + Environment.NewLine + Environment.NewLine + "Pour plus de détails sur les conditions générales d'utilisation, visitez la page CGU de notre site internet : https://medit-solutions.com/cgu." + Environment.NewLine + Environment.NewLine + "Et si vous avez des questions à ce sujet, l'équipe de MéditSolutions est toujours présente (et souriante) pour vos questions : contact@medit-solutions.com";

        public SubscriptionPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            lb_link1.Text = "https://medit-solutions.com/cgu";
            lb_link2.Text = "http://medit-solutions.com/politique-de-confidentialite/";
            lb_privacy.Text = Settings.DeviceLanguage == "English" ? "Terms & Conditions - " : "Conditions d'utilisation - ";
            lb_termslink.Text = Settings.DeviceLanguage == "English" ? "Privacy Policy - " : "Politique de confidentialité - ";
          
            if (Settings.DeviceLanguage == "English")
                lb_terms.Text = terms_EN;
            else lb_terms.Text = terms_FR;

        }
       
		void Handle_PaintSurfaceL(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("50e3c2")
			};

			canvas.DrawCircle(info.Width / 2, info.Height / 2, info.Width / 2, paint);

			if(Device.RuntimePlatform==Device.iOS)
			canvasViewL.HeightRequest = info.Width/2;
			else
			{
				canvasViewL.HeightRequest = (info.Width / 2);
			}
		}

		void Handle_PaintSurfaceR(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("50e3c2")
			};

			canvas.DrawCircle(info.Width / 2, info.Height / 2, info.Width / 2, paint);
            
			if (Device.RuntimePlatform == Device.iOS)
                canvasViewR.HeightRequest = info.Width / 2;
            else
            {
				canvasViewR.HeightRequest = (info.Width / 2);
            }
		}

		void Handle_TappedL(object sender, System.EventArgs e)
		{
			canvasViewL.Opacity = 1;
			stackL.Opacity = 0.44;

			canvasViewR.Opacity = 0.24;
			stackR.Opacity = 0.44;

			imgL.IsVisible = true;
			imgR.IsVisible = false;

			(BindingContext as SubscriptionPageModel).IsMonthlySelected = true;
			(BindingContext as SubscriptionPageModel).OptionSelectedCommand.Execute(null);

			btSub.BackgroundColor = (Color)Application.Current.Resources["primary"];
			btSub.BorderColor = (Color)Application.Current.Resources["primary"];
			btSub.TextColor = (Color)Application.Current.Resources["white"];
		}

		void Handle_TappedR(object sender, System.EventArgs e)
        {
			canvasViewR.Opacity = 1;
            stackR.Opacity = 0.44;
            
            canvasViewL.Opacity = 0.24;
            stackL.Opacity = 0.44;

			imgL.IsVisible = false;
			imgR.IsVisible = true;

			(BindingContext as SubscriptionPageModel).IsMonthlySelected = false;
			(BindingContext as SubscriptionPageModel).OptionSelectedCommand.Execute(null);

			btSub.BackgroundColor = (Color)Application.Current.Resources["primary"];
            btSub.BorderColor = (Color)Application.Current.Resources["primary"];
            btSub.TextColor = (Color)Application.Current.Resources["white"];
        }

    }
}
