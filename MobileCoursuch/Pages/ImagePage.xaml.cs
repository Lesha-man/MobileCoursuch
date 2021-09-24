using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.Net;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace MobileCoursuch.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        public IDownloadFile File;
        bool isDownloading = true;

        Client client;
        UriImage image;
        public ImagePage(UriImage image, Client client/*, NavigationPage navigationPage*/)
        {
            InitializeComponent();
            CrossDownloadManager.Current.CollectionChanged += (sender, e) =>
                Debug.WriteLine(
                "[DownloadManager]" + e.Action);
            Title = image.name;
            this.image = image;
            this.client = client;
            ImageElement.Source.BindingContext = image;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DownloadFile(image.url.AbsoluteUri);
        }
        public async void DownloadFile(String FileName)
        {
            await Task.Yield();
            await Task.Run(() =>
            {
                var downloadManager = CrossDownloadManager.Current;
                var file = downloadManager.CreateDownloadFile(FileName);
                downloadManager.Start(file, true);
                //while (isDownloading)
                //{
                //    isDownloading = IsDownloading(file);
                //}
            });
            if(!isDownloading)
            {
                await DisplayAlert("File Status", "File Downloaded", "Ok");
            }
        }
    }
}