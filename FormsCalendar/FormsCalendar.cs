using System;

using Xamarin.Forms;

namespace FormsCalendar
{
    public class App : Application
    {
        public static Size ScreenSize { get; set; }

        public App()
        {
            MainPage = new CalendarPage();
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
