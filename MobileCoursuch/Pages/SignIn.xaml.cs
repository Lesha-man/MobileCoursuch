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
    public partial class SignIn : ContentPage
    {
        NavigationPage navigationPage;
        LogInPage logInPage;
        Client client;
        public SignIn(Client client, NavigationPage navigationPage, LogInPage logInPage)
        {
            InitializeComponent();
            this.client = client;
            this.navigationPage = navigationPage;
            this.logInPage = logInPage;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool success = false;
            if (PasswordEntry.Text == ConfirmPasswordEntry.Text)
                success = await client.SignIn(LoginEntry.Text, PasswordEntry.Text, ConfirmPasswordEntry.Text, NameEntry.Text);
            if (success)
            {
                if(await DisplayAlert("success", "Successfully created an account, want to go to the login?", "Yes", "No"))
                {
                    await navigationPage.PushAsync(logInPage);
                }
            }
            else
                await DisplayAlert("wrong", "Wrong input", "Ok");
        }

        private void ConfirmPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConfirmPasswordEntry.BackgroundColor =
                PasswordEntry.Text == ConfirmPasswordEntry.Text ?
                 new Color(0.945, 0.945, 0.945)
                 :
                 new Color(0.945, 0.7, 0.7);
        }
    }
}