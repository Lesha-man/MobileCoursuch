using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileCoursuch
{
    public partial class App : Application
    {
        DetailPage detail = new DetailPage();
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage(detail);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
