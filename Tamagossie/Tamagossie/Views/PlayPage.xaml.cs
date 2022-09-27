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
        }

        public async void OnMainClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}