using System;
using System.Collections.Generic;
using Tamagossie.ViewModels;
using Tamagossie.Views;
using Xamarin.Forms;

namespace Tamagossie
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
