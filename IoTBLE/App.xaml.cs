using IoTBLETool.Page;
using IoTBLETool.ViewModel;
using FreshMvvm;
using Xamarin.Forms;

namespace IoTBLETool
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();
			var scan = FreshPageModelResolver.ResolvePageModel<ScanPageModel>();

			var nav = new FreshNavigationContainer(scan, "ScanPage");

			nav.BarBackgroundColor = Color.FromHex("004593");
           	//nav.Init("menu", null);
            //nav.AddPage<ScanPageModel>("scan", null);
            //nav.AddPage<SettingsPageModel>("settings", null);
            MainPage = nav;
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
