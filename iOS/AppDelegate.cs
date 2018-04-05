using System;
using System.Collections.Generic;
using System.Linq;
using IoTBLE.Mobile;
using IoTBLE.Mobile.Data;
using Foundation;
using UIKit;
using IoTBLE.Mobile.Data.iOS;



namespace IoTBLETool.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {

          

			IoTBLEPlatformIOS.Init(new FreshIocWrapper()).WithSharedPreferences();

            //IoTBLEIoc.Container.Register<IUserPreferences> (() => new UserPreferencesIOS ());

            if (!global::Xamarin.Forms.Forms.IsInitialized) {
                //AndroidBLEService.InitializeBLEManager ();
            }
			IoTBLEIoc.Container.RegisterSingleton<IBluetoothManager> (() => new TouchBLEManager());
           	//BLEbundleManagerRegistry.Instance.RegisterbundleManager (bundlebundle.SERVICE_bundle, bundlebundleManager.Instance ());

            BLEbundleManagerRegistry.Instance.RegisterbundleManager(bundlebundle.SERVICE_bundle, bundlebundleManager.Instance());
            //DroidBluetoothPeripheral.GeneralDelay = 0;

            global::Xamarin.Forms.Forms.Init ();

            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif

            LoadApplication (new App ());

            return base.FinishedLaunching (app, options);
        }
    }
}
