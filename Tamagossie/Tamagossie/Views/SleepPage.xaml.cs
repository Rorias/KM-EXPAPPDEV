using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagossie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepPage : ContentPage
    {
        public SleepPage()
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

            navBarLayout.Children.Add(sleepHolder,
        Constraint.RelativeToParent((parent) =>
        {
            return (parent.Width / 2f) - (sleepHolder.WidthRequest / 2);
        }), null, null, null);

        }

        public async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async void SleepGossieClicked(object sender, EventArgs e)
        {
            sleepButton.IsEnabled = false;
            back.IsEnabled = false;

            await gossie.TranslateTo(20, 20, 1000);
            await gossie.FadeTo(0, 200);

            Tucked();
        }

        private void Tucked()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!gossie.IsAnimationPlaying && gossie.Opacity == 0)
                {
                    back.IsEnabled = true;
                    TuckedGossie();
                }
            });
        }

        private void TuckedGossie()
        {
            App.SetCreatureStat(App.pAlone, 50d);
            App.SetCreatureStat(App.pTired, -25d);
            Application.Current.Properties[App.pSleeping] = true;
            Application.Current.Properties[App.pSleepTime] = DateTime.Now;
        }
    }
}