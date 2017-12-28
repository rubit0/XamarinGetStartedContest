using ContestGetStarted.Scenes;
using Urho;
using Color = Urho.Color;
using Xamarin.Forms;
using Animation = Xamarin.Forms.Animation;

namespace ContestGetStarted
{
	public partial class MainPage : ContentPage
	{
	    private bool _hasPresentedIntro;

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

	    protected override void OnSizeAllocated(double width, double height)
	    {
	        base.OnSizeAllocated(width, height);
            if(!_hasPresentedIntro)
                StartIntroAnimation();
	    }

	    private void StartIntroAnimation()
	    {
	        _hasPresentedIntro = true;

	        Circle.Opacity = 0f;
	        Circle.Scale = 0f;
	        TextThings.Opacity = 0f;
	        SightBlocker.Opacity = 1f;

	        var animationController = new Animation();

            var crossfadeTween = new Animation(v => SightBlocker.Opacity = v, 1d, 0d, Easing.SinIn);
            var textOpacityAnimation = new Animation(v => TextThings.Opacity = v, 0d, 1d);
            var circleOpacityTween = new Animation(v => Circle.Opacity = v, 1d, 0d, Easing.CubicIn);
            var circleScaleTween = new Animation(v => Circle.Scale = v, 0d, 20d, Easing.SinIn);

	        animationController.Add(0d, 0.2d, crossfadeTween);
            animationController.Add(0.5d, 1d, textOpacityAnimation);
            animationController.Add(0d, 0.75d, circleOpacityTween);
	        animationController.Add(0d, 0.75d, circleScaleTween);

	        animationController.Commit(this, "Animation",
	            16, 3000, null,
	            (t, f) =>
	            {
	                SightBlocker.IsEnabled = false;
	                Circle.IsEnabled = false;
	            },
	            () => false);
        }
    }
}
