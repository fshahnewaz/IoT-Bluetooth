using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTBLETool.ViewModel;
using FreshMvvm;
using Xamarin.Forms;

namespace IoTBLETool.Page
{
    public class BasePage<T> : BasePage where T : BasePageModel
    {
        public new T PageModel
        {
            get
            {
                return BindingContext as T;
            }
        }
    }

    public class BasePage : FreshBaseContentPage
    {
        public BasePageModel PageModel
        {
            get
            {
                return BindingContext as BasePageModel;
            }
        }

        public BasePage() : base ()
        {
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (PageModel != null)
            {
                
                SetBinding(TitleProperty, new Binding(nameof(BasePageModel.Title)));
            }
        }
