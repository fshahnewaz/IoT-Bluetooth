using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using IoTBLETool.ViewModel;
using Xamarin.Forms;

namespace IoTBLETool.Page
{
    public class SettingsPage : BasePage<SettingsPageModel>
    {
        public SettingsPage()
        {
            var lbl = new Label();
            lbl.SetBinding(Label.TextProperty, new Binding(nameof(SettingsPageModel.Title)));
            Content = new StackLayout
            {
                Children = {
                    lbl
                }
            };
        }
    }
}
