using Xamarin.Forms;
using System.Threading.Tasks;

namespace FabSampleForms
{
	public class MainPage : ContentPage
	{
		private readonly FloatingActionButtonView fab;

		public MainPage()
		{
			Title = "Fab Sample XF";
			BackgroundColor = Color.White;

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
				} 
			};

			// Main page layout
			var pageLayout = new StackLayout {
				Children = 
				{
					new Label {
						VerticalOptions = LayoutOptions.CenterAndExpand,
						XAlign = TextAlignment.Center,
						TextColor = Color.Black,
						Text = "Welcome to Xamarin Forms!" 
					}
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
	}
}

