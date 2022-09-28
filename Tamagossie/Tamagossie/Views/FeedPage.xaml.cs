using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagossie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage
    {
        public FeedPage()
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

            navBarLayout.Children.Add(eatHolder,
              Constraint.RelativeToParent((parent) =>
              {
                  return (parent.Width / 2f) - (eatHolder.WidthRequest / 2);
              }),null, null, null);

            if (App.timers.ContainsKey("EatCooldown"))
            {
                feedButton.IsEnabled = false;
            }
        }

        public async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void FeedGossieClicked(object sender, EventArgs e)
        {
            feedButton.IsEnabled = false;
            back.IsEnabled = false;

            foodToEat.TranslateTo(0, -100, 1000);
            foodToEat.FadeTo(0, 1000);

            App.StartTimer("EatTimer", 1100).Elapsed += FoodEaten;
        }

        private void FoodEaten(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!foodToEat.IsAnimationPlaying && foodToEat.Opacity == 0)
                {
                    back.IsEnabled = true;
                    ResetAnimation();
                    FeedGossie();
                    App.StartTimer("EatCooldown", 10000).Elapsed += Cooldown_Over;
                }
            });
        }

        private void Cooldown_Over(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                feedButton.SetValue(IsEnabledProperty, true);
            });
        }

        private void ResetAnimation()
        {
            foodToEat.TranslateTo(0, 0, 1);
            foodToEat.FadeTo(100, 1);
        }

        private void FeedGossie()
        {
            App.SetCreatureStat(App.pHunger, 100d);
            App.SetCreatureStat(App.pBored, -20d);
            App.SetCreatureStat(App.pAlone, -20d);
            App.SetCreatureStat(App.pTired, 20d);
        }
    }
}