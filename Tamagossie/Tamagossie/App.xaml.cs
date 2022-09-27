using System;
using System.Timers;
using Tamagossie.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagossie
{
    public partial class App : Application
    {
        private DateTime previousTime;
        private DateTime nowTime;

        public const string pLastLogin = "lastlogin";
        public const string pHunger = "hunger";
        public const string pThirst = "thirst";
        public const string pBored = "bored";
        public const string pAlone = "alone";
        public const string pTired = "tired";

        public App()
        {
            nowTime = DateTime.Now;
            LoadPreferences();

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //NavigationPage.SetHasNavigationBar(MainPage, false);

            var timer = new Timer()
            {
                Interval = 500,
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void LoadPreferences()
        {
            previousTime = Preferences.Get(pLastLogin, nowTime);

            Current.Properties[pLastLogin] = nowTime.Second - previousTime.Second;
            Current.Properties[pHunger] = Preferences.Get(pHunger, 340d);
            Current.Properties[pThirst] = Preferences.Get(pThirst, 340d);
            Current.Properties[pBored] = Preferences.Get(pBored, 0d);
            Current.Properties[pAlone] = Preferences.Get(pAlone, 170d);
            Current.Properties[pTired] = Preferences.Get(pTired, 0d);
        }

        private void SavePreferences()
        {
            Preferences.Set(pHunger, Current.Properties[pHunger].ToString());
            Preferences.Set(pThirst, Current.Properties[pThirst].ToString());
            Preferences.Set(pBored, Current.Properties[pBored].ToString());
            Preferences.Set(pAlone, Current.Properties[pAlone].ToString());
            Preferences.Set(pTired, Current.Properties[pTired].ToString());
        }

        private void ForceResetPreferences()
        {
            Preferences.Set(pHunger, 340d);
            Preferences.Set(pThirst, 340d);
            Preferences.Set(pBored, 0d);
            Preferences.Set(pAlone, 170d);
            Preferences.Set(pTired, 0d);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetCreatureStat(pHunger, -0.5d);
            SetCreatureStat(pThirst, -1d);
            SetCreatureStat(pBored, 0.25d);
            SetCreatureStat(pAlone, 0.2d);
            SetCreatureStat(pTired, 0.33d);
        }

        public static void SetCreatureStat(string _stat, double _toAdd)
        {
            if (((double)Current.Properties[_stat]) + _toAdd < 340d && ((double)Current.Properties[_stat]) + _toAdd > 0d)
            {
                Current.Properties[_stat] = ((double)Current.Properties[_stat]) + _toAdd;
            }
            else if(((double)Current.Properties[_stat]) + _toAdd > 340d)
            {
                Current.Properties[_stat] = 340d;
            }
            else if (((double)Current.Properties[_stat]) + _toAdd < 0d)
            {
                Current.Properties[_stat] = 0d;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            SavePreferences();
        }

        protected override void OnResume()
        {
        }
    }
}
