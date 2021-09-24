using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using Plugin.Media.Abstractions;
using System.IO.Abstractions;

namespace MobileCoursuch.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPage : ContentPage
    {
        Client client;

        public UploadPage(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                byte[] buffer = new byte[9999999];
                byte[] imageb = new byte[stream.Read(buffer, 0, 9999999)];
                imageb = buffer.Take(imageb.Length).ToArray();
                await client.Upload(UploadEntry.Text, result.FileName, imageb);
            }
            //IFileReader fileReader = DependencyService.Get<IFileReader>();
            //string path = result.FullPath.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Last();
            //await client.Upload(UploadEntry.Text, result.FileName, fileReader.ReadAllByteS(path + "/" + result.FileName));
        }
    }
}