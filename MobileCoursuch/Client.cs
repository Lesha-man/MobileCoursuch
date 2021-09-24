using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileCoursuch
{
    public class Client
    {
        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<UriImage> Images = new System.Collections.ObjectModel.ObservableCollection<UriImage>();
        private string Token;
        private readonly string HostUrl;
        private readonly string SignInUrl;
        private readonly string LogInUrl;
        private readonly string SearchUrl;
        private readonly string LastImagesUrl;
        private readonly string GetByIdUrl;
        private readonly string ImagesOfUserUrl;
        private readonly string UploadingUrl;

        public Client()
        {
            //Images.Add(new UriImage());
            //Images[0].url = new Uri("https://res.cloudinary.com/drexisxbu/image/upload/v1605870904/storage/2cY9K7KYA54hgoCQGQSBxHh0whj95Qup.jpg");
            //Images.Add(new UriImage());
            //Images[1].url = new Uri("https://img.etimg.com/thumb/width-640,height-480,imgsize-482493,resizemode-1,msid-68228307/news/politics-and-nation/how-central-european-state-serbia-contributed-to-making-of-uri/uri-indi.jpg");
            HostUrl = "https://cloud-image-store.herokuapp.com";
            SignInUrl = HostUrl + "/api/account/register";
            LogInUrl = HostUrl + "/api/account/login";
            SearchUrl = HostUrl + "/api/images/search";
            LastImagesUrl = HostUrl + "/api/images/last";
            GetByIdUrl = HostUrl + "/api/images";
            ImagesOfUserUrl = HostUrl + "/api/images/user";
            UploadingUrl = HostUrl + "/api/images/";
        }

        public async Task<bool> SignIn(string login, string password, string confirmPassword, string name)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>()
            {
                {"login", login },
                {"name", name },
                {"password", password },
                {"confirmPassword", confirmPassword },
            };
            FormUrlEncodedContent form = new FormUrlEncodedContent(pairs);
            HttpResponseMessage responseMessage = await client.PostAsync(SignInUrl, form);
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (result == "success")
            {
                return true;
            }
            return false;
        }

        public async Task<bool> LogIn(string login, string password)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>()
            {
                {"login", login },
                {"password", password },
            };
            FormUrlEncodedContent form = new FormUrlEncodedContent(pairs);
            HttpResponseMessage responseMessage = await client.PostAsync(LogInUrl, form);
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                Token = result;
                return true;
            }
            return false;
        }
        public async Task<bool> Search(string name)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(SearchUrl + "?name=" + name);
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                Images.Clear();
                foreach (var img in JsonConvert.DeserializeObject<ObservableCollection<UriImage>>(result))
                {
                    img.url = new Uri(img.url.AbsoluteUri.Insert(4, "s"));
                    Images.Add(img);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> Upload(string name, string fileName, byte[] image)
        {
            UriImage uriImage;
            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { new StringContent(name), "name" },
                { new StringContent(name), "tags[0]" },
                { new ByteArrayContent(image), "file", fileName }
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage responseMessage = await client.PostAsync(UploadingUrl, form);
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                uriImage = JsonConvert.DeserializeObject<UriImage>(result);
                return true;
            }
            return false;
        }
    }
}