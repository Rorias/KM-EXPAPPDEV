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
    public partial class ThirstPage : ContentPage
    {
        public ThirstPage()
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

            navBarLayout.Children.Add(drinkHolder,
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width / 2f;
            }), null, null, null);

            if (App.timers.ContainsKey("DrinkCooldown"))
            {
                drinkButton.IsEnabled = false;
            }
        }
        public async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async void DrinkGossieClicked(object sender, EventArgs e)
        {
            drinkButton.IsEnabled = false;
            back.IsEnabled = false;

            await drink.FadeTo(100, 1);

            for (int i = 0; i < 30; i++)
            {
                await drink.ScaleXTo(-1, 50);
                await drink.ScaleXTo(1, 50);
            }

            await drink.FadeTo(0, 200);

            Drank();
        }

        private void Drank()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!drink.IsAnimationPlaying && drink.Opacity == 0)
                {
                    back.IsEnabled = true;
                    WaterGossie();
                    App.StartTimer("DrinkCooldown", 10000).Elapsed += Cooldown_Over;
                }
            });
        }

        private void Cooldown_Over(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                drinkButton.SetValue(IsEnabledProperty, true);
            });
        }

        private void WaterGossie()
        {
            App.SetCreatureStat(App.pThirst, 100d);
            App.SetCreatureStat(App.pBored, +10d);
            App.SetCreatureStat(App.pAlone, -10d);
            App.SetCreatureStat(App.pTired, 10d);
        }
    }
}