using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Timers;

namespace Tamagossie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            navBarLayout.WidthRequest = WidthRequest;

            navBarLayout.Children.Add(navBarGrid,
                Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2f) - (navBarGrid.WidthRequest / 2);
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - navBarGrid.HeightRequest;
                }), null, null);

            Timer timer = new Timer()
            {
                Interval = 500,
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                GoneSince.Text = Application.Current.Properties[App.pLastLogin].ToString();
                hungerbar.WidthRequest = (double)Application.Current.Properties[App.pHunger];
                thirstbar.WidthRequest = (double)Application.Current.Properties[App.pThirst];
                boredbar.WidthRequest = (double)Application.Current.Properties[App.pBored];
                alonebar.WidthRequest = (double)Application.Current.Properties[App.pAlone];
                tiredbar.WidthRequest = (double)Application.Current.Properties[App.pTired];
            });
        }

        public async void OnFeedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FeedPage());
        }

        public async void OnThirstClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThirstPage());
        }

        public async void OnPlayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage());
        }

        public async void OnSleepClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SleepPage());
        }
    }
}