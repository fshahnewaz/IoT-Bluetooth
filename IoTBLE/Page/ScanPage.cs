using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTBLETool.ViewModel;
using Xamarin.Forms;

namespace IoTBLETool.Page
{
    public class ScanPage : BasePage<ScanPageModel>
    {

		private ListView list;

		public ScanPage()
        {
            var stack = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15
            };

			list = new ListView();
            list.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(ScanPageModel.Items)));
	
            list.ItemTemplate = new DataTemplate(typeof(ScanItemTemplate));

            stack.Children.Add(list);
            Content = stack;

        }

		protected override void OnAppearing()
        {
            base.OnAppearing();
			list.ItemTapped += List_ItemSelected;
        }
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			list.ItemTapped -= List_ItemSelected;
		}

		void List_ItemSelected(object sender, ItemTappedEventArgs e)
		{

			var selected = e?.Item as ScanItem;
			this.PageModel.OnSelectedCommand.Execute(selected);

		}

        private class ScanItemTemplate : Viebundleell
        {
            public ScanItemTemplate()
            {
				var lbl = new Label()
				{
					HorizontalOptions = LayoutOptions.StartAndExpand,
					IsEnabled = true,
					FontSize = 16,
					VerticalTextAlignment = TextAlignment.Center,
			

                };
                lbl.SetBinding(Label.TextProperty, new Binding(nameof(ScanItem.Title)));

                View = lbl;
            }
        }
    }
}
