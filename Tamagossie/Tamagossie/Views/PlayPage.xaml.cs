using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagossie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        public PlayPage()
        {
            InitializeComponent();

            navBarLayout.Children.Add(navBarGrid,
        Constraint.RelativeToParent((parent) =>
        {
            return (parent.Width / 2f) - (navBarGrid.WidthRequest / 2);
        }),
        Constraint.RelativeToParent((parent) =>
        {
            return parent.Height - navBarGrid.HeightRequest;
        }), null, null);

            navBarLayout.Children.Add(gossieHolder,
         Constraint.RelativeToParent((parent) =>
         {
             return (parent.Width / 2f) - (gossieHolder.WidthRequest / 2);
         }), null, null, null);

            if (App.timers.ContainsKey("PlayCooldown"))
            {
                playButton.IsEnabled = false;
            }
        }

        public async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async void PlayGossieClicked(object sender, EventArgs e)
        {
            playButton.IsEnabled = false;
            back.IsEnabled = false;

            await ball.FadeTo(100, 100);
            await ball.TranslateTo(150, 150, 1000);
            await ball.TranslateTo(0, -300, 750);
            await ball.TranslateTo(-150, 50, 1000);
            await ball.TranslateTo(0, 10, 500);
            await ball.FadeTo(0, 100);

            Played();
        }

        private void Played()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!ball.IsAnimationPlaying && ball.Opacity == 0)
                {
                    back.IsEnabled = true;
                    PlayedGossie();
                    App.StartTimer("PlayCooldown", 60000).Elapsed += Cooldown_Over;
                }
            });
        }

        private void Cooldown_Over(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                playButton.SetValue(IsEnabledProperty, true);
            });
        }

        private void PlayedGossie()
        {
            App.SetCreatureStat(App.pHunger, -75d);
            App.SetCreatureStat(App.pThirst, -50d);
            App.SetCreatureStat(App.pBored, -150d);
            App.SetCreatureStat(App.pAlone, -50d);
            App.SetCreatureStat(App.pTired, 50d);
        }
    }
}