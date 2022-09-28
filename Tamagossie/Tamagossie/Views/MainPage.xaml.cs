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

            CheckDisallowedButtons();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //double sleepSeconds = (DateTime.Now - (DateTime)Application.Current.Properties[App.pSleepTime]).TotalSeconds;
                //GoneSince.Text = sleepSeconds.ToString();
                hungerbar.WidthRequest = (double)Application.Current.Properties[App.pHunger];
                thirstbar.WidthRequest = (double)Application.Current.Properties[App.pThirst];
                boredbar.WidthRequest = (double)Application.Current.Properties[App.pBored];
                alonebar.WidthRequest = (double)Application.Current.Properties[App.pAlone];
                tiredbar.WidthRequest = (double)Application.Current.Properties[App.pTired];
                CheckDisallowedButtons();
            });
        }

        private void CheckDisallowedButtons()
        {
            if ((bool)Application.Current.Properties[App.pSleeping])
            {
                foodNav.SetValue(IsEnabledProperty, false);
                thirstNav.SetValue(IsEnabledProperty, false);
                playNav.SetValue(IsEnabledProperty, false);
                sleepNav.SetValue(IsEnabledProperty, false);
                return;
            }
            else
            {
                foodNav.SetValue(IsEnabledProperty, true);
                thirstNav.SetValue(IsEnabledProperty, true);
                playNav.SetValue(IsEnabledProperty, true);
                sleepNav.SetValue(IsEnabledProperty, true);
            }

            if ((double)Application.Current.Properties[App.pHunger] < 75d || (double)Application.Current.Properties[App.pThirst] < 50d ||
                (double)Application.Current.Properties[App.pAlone] < 75d || (double)Application.Current.Properties[App.pTired] > 320d)
            {
                playNav.SetValue(IsEnabledProperty, false);
            }
            else
            {
                playNav.SetValue(IsEnabledProperty, true);
            }

            if ((double)Application.Current.Properties[App.pAlone] < 20d)
            {
                foodNav.SetValue(IsEnabledProperty, false);
            }
            else
            {
                foodNav.SetValue(IsEnabledProperty, true);
            }

            if ((double)Application.Current.Properties[App.pAlone] < 10d)
            {
                thirstNav.SetValue(IsEnabledProperty, false);
            }
            else
            {
                thirstNav.SetValue(IsEnabledProperty, true);
            }

            if ((double)Application.Current.Properties[App.pTired] > 170d)
            {
                sleepNav.SetValue(IsEnabledProperty, true);
            }
            else
            {
                sleepNav.SetValue(IsEnabledProperty, false);
            }
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