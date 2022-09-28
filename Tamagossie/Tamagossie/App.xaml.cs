using System;
using System.Collections.Generic;
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
        public const string pSleeping = "sleeping";

        public static Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

        public App()
        {
            LoadPreferences();
            UpdateStats();

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

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
            nowTime = DateTime.Now;
            previousTime = Preferences.Get(pLastLogin, nowTime);

            double totalSeconds = (nowTime - previousTime).TotalSeconds;
            Current.Properties[pLastLogin] = totalSeconds;
            Current.Properties[pHunger] = Preferences.Get(pHunger, 340d);
            Current.Properties[pThirst] = Preferences.Get(pThirst, 340d);
            Current.Properties[pBored] = Preferences.Get(pBored, 0d);
            Current.Properties[pAlone] = Preferences.Get(pAlone, 170d);
            Current.Properties[pTired] = Preferences.Get(pTired, 0d);
        }

        private void UpdateStats()
        {
            SetCreatureStat(pHunger, -((double)Current.Properties[pLastLogin] / 10d * 0.5d));
            SetCreatureStat(pThirst, -((double)Current.Properties[pLastLogin] / 10d * 1d));
            SetCreatureStat(pBored, ((double)Current.Properties[pLastLogin] / 10d * 0.25d));
            SetCreatureStat(pAlone, ((double)Current.Properties[pLastLogin] / 10d * 1d));
            SetCreatureStat(pTired, -((double)Current.Properties[pLastLogin] / 10d * 1d));
        }

        private void SavePreferences()
        {
            Preferences.Set(pLastLogin, DateTime.Now);
            Preferences.Set(pHunger, (double)Current.Properties[pHunger]);
            Preferences.Set(pThirst, (double)Current.Properties[pThirst]);
            Preferences.Set(pBored, (double)Current.Properties[pBored]);
            Preferences.Set(pAlone, (double)Current.Properties[pAlone]);
            Preferences.Set(pTired, (double)Current.Properties[pTired]);
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

        public static Timer StartTimer(string _name, double _waitTime)
        {
            timers.Add(_name,
             new Timer()
             {
                 Interval = _waitTime,
                 AutoReset = false
             });

            timers[_name].Elapsed += (sender, e) => TimerFinished(sender, e, _name);

            timers[_name].Start();
            return timers[_name];
        }

        private static void TimerFinished(object sender, ElapsedEventArgs e, string _name)
        {
            timers.Remove(_name);
        }

        public static void SetCreatureStat(string _stat, double _toAdd)
        {
            if (((double)Current.Properties[_stat]) + _toAdd < 340d && ((double)Current.Properties[_stat]) + _toAdd > 0d)
            {
                Current.Properties[_stat] = ((double)Current.Properties[_stat]) + _toAdd;
            }
            else if (((double)Current.Properties[_stat]) + _toAdd > 340d)
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
