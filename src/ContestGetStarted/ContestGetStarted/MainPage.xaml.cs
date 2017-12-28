using System;
using ContestGetStarted.Scenes;
using Urho;
using Color = Urho.Color;
using Xamarin.Forms;

namespace ContestGetStarted
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
            var options = new ApplicationOptions("Data");

	        var scene = await this.Surface.Show<MainScene>(options);
            scene.SetBackgroundColor(Color.White);
	    }
	}
}
