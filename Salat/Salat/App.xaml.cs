using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Salat.Services;
using Salat.Views;

namespace Salat
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new ItemsPage();

            //Background color
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);

            //Title color
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
