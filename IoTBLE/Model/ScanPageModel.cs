using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using IoTBLE.Mobile;

namespace IoTBLETool.ViewModel
{
    public class ScanItem
    {
        public string Title { get; set; }
        public string Id { get; set; }
		public IBluetoothPeripheral Peripheral { get; set; }

		public override bool Equals(object obj)
		{
			return Id.Equals(((ScanItem)obj).Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
    }

    public class ScanPageModel : BasePageModel
    {
		//private bundlebundleManager manager;
        private bundlebundleManager manager;
        private CancellationTokenSource _backgroundScanCancel = new CancellationTokenSource();
		public bool BluetoothEnabled { get; private set; }

		public ScanPageModel()
        {
       
            Items = new ObservableCollection<ScanItem>()
            {
                new ScanItem() {Title = "Test1", Id = "1"},
           
            };
        }
        
        public ObservableCollection<ScanItem> Items { get; set; }

        private ICommand _selectedCommand;
        public ICommand OnSelectedCommand
        {
            get
            {
                if (_selectedCommand == null)
                {
                    _selectedCommand = new Command<ScanItem>(async (item) =>
                    {
						if (_backgroundScanCancel != null)
							_backgroundScanCancel.Cancel();
                        await CoreMethods.PushPageModel<MonitorPageModel>(item);
                    });
                }
                return _selectedCommand;
            }
        }

		public override void Init(object initData)
		{
			base.Init(initData);
			BluetoothEnabled = true; //start with true.
		}


		

		object locker = new object();
		void Manager_bundleDiscovery(object sender, bundlebundleManager.bundleDiscoveredEventArgs e)//changed for bundle
		{

			var title = " " + e.Peripheral.Name + " " + e.Peripheral.Address + " (" + e.Peripheral.RSSI + ")";
			var item = new ScanItem() { Peripheral = e.Peripheral, Id = e.Peripheral.Address, Title = title };
			lock(locker)
			{
				int index = Items.IndexOf(item);

				if (index >= 0)
					Items[index].Title = title; //for RSSI updates
				else
					Items.Add(item);
			}
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			base.ViewIsDisappearing(sender, e);
			_backgroundScanCancel.Cancel();
		}

		private async void BackgroundScanLoop(CancellationToken token)
		{

			while (1 == 1)
			{

				try
				{
					if (token.IsCancellationRequested)
							return;
					Items.Clear();

					if (!BluetoothEnabled)
					{
						Title = "Please Enable Bluetooth";
						continue;
					}

					manager.ScanForNewbundles();
					Title = "Scanning";
					if (token.IsCancellationRequested)
						return;
					
					await Task.Delay(manager.GetConfigurationTemplate().ScanTime, token);

					if (token.IsCancellationRequested)
						return;
					Title = "Sleeping";
					await Task.Delay(manager.GetConfigurationTemplate().ScanInterval, token);
				}
				catch (Exception e)
				{
					if (e as TaskCanceledException != null)
						;//this occurs when bluetooth is shutdown at the system level
					else
						throw e;
					//TODO log exception
				}

			}
		}


		private async void MonitorBLEEnablement(CancellationToken token)
		{

			while (!token.IsCancellationRequested)
			{

				try
				{
					await Task.Delay(5000);
                    var _platformBLEManager = IoTBLEIoc.Container.Resolve<IBluetoothManager>();
					if(_platformBLEManager != null)
						BluetoothEnabled = _platformBLEManager.IsBluetoothEnabled();

				}
				catch (Exception e)
				{
					//TODO
					throw e;
				}

			}
		}



    }
}
