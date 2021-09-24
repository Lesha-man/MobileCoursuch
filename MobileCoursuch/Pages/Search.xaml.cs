using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileCoursuch.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        Client client;
        NavigationPage navigationPage;
        public Search(Client client, NavigationPage navigationPage)
        {
            InitializeComponent();
            this.client = client;
            Pictures.ItemsSource = client.Images;
            this. navigationPage = navigationPage;
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            await client.Search(((SearchBar)sender).Text);
            //client.Images.Add(client.Images[0]);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            UriImage image = (UriImage)((Image)sender).Source.BindingContext;
            //for (int i = 0; i < client.Images.Count; i++)
            //{
            //    if(client.Images[0].url == ((Image)sender).Source.BindingContext)

            //}

            await navigationPage.PushAsync(new ImagePage(image, client));
        }
    }
}