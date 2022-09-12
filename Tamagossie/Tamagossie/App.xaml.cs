using System;
using Tamagossie.Services;
using Tamagossie.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagossie
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
