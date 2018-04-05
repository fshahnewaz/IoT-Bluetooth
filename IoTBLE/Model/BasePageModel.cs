using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

using FreshMvvm;

namespace IoTBLETool.ViewModel
{
    public class BasePageModel : FreshBasePageModel
    {
        /// <summary>
        /// Checks to make sure the value has been changed and then, if so, calls OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore">Backing store for the property being changed</param>
        /// <param name="value">New value to set.</param>
        /// <param name="propertyName">Name of the property being changed</param>
        /// <returns>true if new value is set and event is fired.  Otherwise, false. </returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            Debug.Assert(propertyName != null, "propertyName != null");

            if (Equals(backingStore, value)) return false;

            backingStore = value;
          
            this.RaisePropertyChanged(propertyName);
            return true;
        }
        
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


    }
}
