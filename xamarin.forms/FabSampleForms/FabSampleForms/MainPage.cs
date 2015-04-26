using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace FabSampleForms
{
	public class MainPage : ContentPage
	{
		private readonly FloatingActionButtonView fab;
		private readonly ListView list;
		private int appearingListItemIndex = 0;

		public MainPage()
		{
			Title = "Fab Sample XF";
			BackgroundColor = Color.White;

			var data = new List<string>();
			for(var i = 1; i <= 100; i++)
			{
				data.Add(i.ToString());
			}

			list = new ListView {
				VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = data,
			};

			fab = new FloatingActionButtonView() {
				ImageName = "ic_add.png",
				ColorNormal = Color.FromHex("ff3498db"),
				ColorPressed = Color.Black,
				ColorRipple = Color.FromHex("ff3498db"),
				Clicked = async (sender, args) => 
				{
					var animate = await this.DisplayAlert("Fab", "Hide and show the Fab?", "Sure", "Not now");
					if (!animate) return;

					fab.Hide();
					await Task.Delay(1500);
					fab.Show();
				},
			};

			// Main page layout
			var pageLayout = new StackLayout {
				Children = 
				{
					list
				}};

			var absolute = new AbsoluteLayout() { 
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.FillAndExpand };

			// Position the pageLayout to fill the entire screen.
			// Manage positioning of child elements on the page by editing the pageLayout.
			AbsoluteLayout.SetLayoutFlags(pageLayout, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(pageLayout, new Rectangle(0f, 0f, 1f, 1f));
			absolute.Children.Add(pageLayout);

			// Overlay the FAB in the bottom-right corner
			AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			absolute.Children.Add(fab);

			Content = absolute;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			list.ItemAppearing += List_ItemAppearing;
			list.ItemDisappearing += List_ItemDisappearing;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			list.ItemAppearing -= List_ItemAppearing;
			list.ItemDisappearing -= List_ItemDisappearing;
		}

		async void List_ItemDisappearing (object sender, ItemVisibilityEventArgs e)
		{
			await Task.Run(() =>
			{
				var items = list.ItemsSource as IList;
				if(items != null)
				{
					var index = items.IndexOf(e.Item);
					if (index < appearingListItemIndex)
					{
						Device.BeginInvokeOnMainThread(() => fab.Hide());
					}
					appearingListItemIndex = index;
				}
			});
		}

		async void List_ItemAppearing (object sender, ItemVisibilityEventArgs e)
		{
			await Task.Run(() =>
			{
				var items = list.ItemsSource as IList;
				if(items != null)
				{
					var index = items.IndexOf(e.Item);
					if (index < appearingListItemIndex)
					{
						Device.BeginInvokeOnMainThread(() => fab.Show());
					}
					appearingListItemIndex = index;
				}
			});
		}
	}
}

