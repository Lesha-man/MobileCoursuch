using MobileCoursuch.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCoursuch
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Client client;
        Search searchPage;
        LogInPage logInPage;
        SignIn signInPage;
        UploadPage uploadPage;
        View[] menuItems;
        NavigationPage navigationPage;

        public MainPage(DetailPage detail)
        {
            InitializeComponent();
            client = new Client();
            navigationPage = new NavigationPage(new DetailPage());
            searchPage = new Search(client, navigationPage);
            logInPage = new LogInPage(client);
            signInPage = new SignIn(client, navigationPage, logInPage);
            uploadPage = new UploadPage(client);
            navigationPage.BarBackgroundColor = Color.Black;
            Detail = navigationPage;
            menuItems = MenuLayout.Children.ToArray();
            Deanime(192, 50);
        }

        private void MasterDetailPage_IsPresentedChanged(object sender, EventArgs e)
        {
            OpenClose();
        }

        public void OpenClose()
        {
            if (menuItems[0].TranslationX != 0)
            {
                Anime(155, 40);
            }
            else
            {
                Deanime(192, 50);
            }
        }
        void Anime(int start, int grow)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].TranslateTo(0, 0, (uint)(start + i * grow));
            }
        }
        void Deanime(int start, int grow)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].TranslationX = -start - i * grow;
            }
        }
        private async void HomeButton_Clicked(object sender, EventArgs e)
        {
            await navigationPage.PopToRootAsync();
            IsPresented = false;
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            await navigationPage.PushAsync(searchPage);
            IsPresented = false;
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            await navigationPage.PushAsync(logInPage); 
            IsPresented = false;
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            await navigationPage.PushAsync(signInPage);
            IsPresented = false;
        }

        private async void UploadImage_Clicked(object sender, EventArgs e)
        {
            await navigationPage.PushAsync(uploadPage);
            IsPresented = false;
        }
    }
}
