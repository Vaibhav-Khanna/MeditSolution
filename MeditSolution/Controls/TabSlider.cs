using System;
using System.Collections;
using MeditSolution.Models;
using Xamarin.Forms;

namespace MeditSolution.Controls
{
	public class TabSlider : ContentView
    {

        public static readonly BindableProperty TabsProperty =
            BindableProperty.Create("Tabs", typeof(IEnumerable), typeof(TabSlider), defaultValue: null);


        public IEnumerable Tabs
        {
            get
            {
                return (IEnumerable)GetValue(TabsProperty);
            }
            set
            {
                SetValue(TabsProperty, value);
                OnPropertyChanged(nameof(Tabs));
            }
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create("SelectedIndex", typeof(int), typeof(TabSlider), 0);


		public Color BackColor { get; set; } = Color.White;

		public Color SelectedTextColor { get; set; } = Color.Black;

		public Color TextColor { get; set; } = Color.FromHex("bfbfbf");


        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        StackLayout Container;
		ScrollView scrollview;
        TapGestureRecognizer tap_tabgesture = new TapGestureRecognizer();   // tab tap gesture handler
        

        public TabSlider()
        {
            InitView();
        }

        void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)  // handle change of tabviews
        {
            if (Tabs == null)
                return;

            Container.Children.Clear();
            foreach (var item in Tabs)
            {
				Container.Children.Add(GenerateTab(((Tab)item).TabName));
            }

			SelectTab();
        }


        void InitView()                 // Initialize View
        {
			Container = new StackLayout() { Spacing = 40, Padding = new Thickness(20, Device.RuntimePlatform == Device.iOS ? 10 : 15, 20, Device.RuntimePlatform == Device.iOS ? 10 : 15), Margin = 0, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.StartAndExpand, Orientation = StackOrientation.Horizontal };

            tap_tabgesture.Tapped += (sender, e) =>
            {
                var selected_tab = sender as Label;

                if (Container.Children.Contains(selected_tab))
                {
                    var index = Container.Children.IndexOf(selected_tab);
                    SelectedIndex = index;
                }
            };
            
            scrollview = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
				BackgroundColor = BackColor,
				Content = Container,
				VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                Margin = 0
            };

            Content = scrollview;
        }



        Label GenerateTab(string name)
        {
			var label = new Label { FontSize = 17, FontAttributes = FontAttributes.Bold, FontFamily = "SFUIText-Medium", Text = name, TextColor = TextColor };
            label.GestureRecognizers.Add(tap_tabgesture);
            return label;
        }


        void SelectTab()
        {
            if (Container.Children.Count == 0)
                return;

            foreach (Label item in Container.Children)
            {
				item.TextColor = TextColor;
            }

            var new_selected = (Label)Container.Children[SelectedIndex];

            if (new_selected != null)
            {
				new_selected.TextColor = SelectedTextColor;
                scrollview.ScrollToAsync(new_selected, ScrollToPosition.Center, true); 
            }
        }


        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case "Tabs":
                    Tabs_CollectionChanged(null, null);
                    break;

                case "SelectedIndex":
                    {
                        SelectTab();
                        break;
                    }
            }
        }

    }
}
