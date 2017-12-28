using System.Threading.Tasks;
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

		    Badge.Opacity = 0f;
		    Circle.Opacity = 0f;
		    Circle.Scale = 0f;
		    TextThings.Opacity = 0f;
		    SightBlocker.Opacity = 1f;
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
            var options = new ApplicationOptions("Data");

	        var scene = await this.Surface.Show<MainScene>(options);
            scene.SetBackgroundColor(Color.White);

	        if (!_hasPresentedIntro)
	            StartAnimations();
        }

	    private async void StartAnimations()
	    {
	        _hasPresentedIntro = true;

	        await Task.Delay(400);
            BadgeAnimation();
	        await Task.Delay(200);
            BackdropAnimations();
	    }

	    private void BackdropAnimations(uint duration = 3000)
	    {
            var crossfadeTween = new Animation(v => SightBlocker.Opacity = v, 1d, 0d, Easing.SinIn);
            var textOpacityAnimation = new Animation(v => TextThings.Opacity = v, 0d, 1d);
            var circleOpacityTween = new Animation(v => Circle.Opacity = v, 1d, 0d, Easing.CubicIn);
            var circleScaleTween = new Animation(v => Circle.Scale = v, 0d, 20d, Easing.SinIn);

            var animationController = new Animation
            {
                { 0d, 0.25d, crossfadeTween },
                { 0.5d, 1d, textOpacityAnimation },
                { 0d, 0.6d, circleOpacityTween },
                { 0d, 0.65d, circleScaleTween }
            };

            animationController.Commit(this, 
                "MainAnimation",
	            16, duration, null,
	            (t, f) =>
	            {
	                SightBlocker.IsEnabled = false;
	                Circle.IsEnabled = false;
                },
	            () => false);
	    }

	    private void BadgeAnimation(uint duration = 2000)
	    {
            Badge.RotationX = 100;
            Badge.RotationY = 120;

	        var fadeTween = new Animation(v => Badge.Opacity = v, 0d, 1d, Easing.Linear);
	        var rotateYTween = new Animation(v => Badge.RotationY = v, 120d, 0d, Easing.SpringOut);
	        var rotateXTween = new Animation(v => Badge.RotationX = v, 100d, 0d, Easing.SpringOut);

	        var animationController = new Animation
	        {
	            {0d, 0.4d, fadeTween},
	            {0d, 1d, rotateYTween},
	            {0d, 0.7d, rotateXTween}
	        };

	        animationController.Commit(this, 
                "BadgeAnimation", 
                16, duration, null, null, 
                ()=> false);
        }
    }
}
