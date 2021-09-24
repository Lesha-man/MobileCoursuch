using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileCoursuch.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        Client client;

        public LogInPage(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await client.LogIn(LoginEntry.Text, PasswordEntry.Text))
            {
                await DisplayAlert("success", "Successfully login", "Ok");
            }
            else
                await DisplayAlert("wrong", "Wrong input", "Ok");
        }
    }
}