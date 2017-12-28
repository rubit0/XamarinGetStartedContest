using System;
using System.Threading.Tasks;
using ContestGetStarted.UrhoAssets.Actions;
using Urho;
using Urho.Actions;
using Color = Urho.Color;

namespace ContestGetStarted.Scenes
{
    public class MainScene : Urho.Application
    {
        private Scene _rootScene;

        public MainScene(ApplicationOptions options) : base(options)
        {
        }

        public MainScene(IntPtr handle) : base((IntPtr) handle)
        {
        }

        public void SetBackgroundColor(Color color)
        {
            Renderer.GetViewport(0).SetClearColor(color);
        }

        protected MainScene(UrhoObjectFlag emptyFlag) : base(emptyFlag)
        {
        }

        protected override void Start()
        {
            base.Start();
            SceneHelpers.Context = this;
            CreateScene();
        }

        private void CreateScene()
        {
            //Scene
            _rootScene = new Scene();
            _rootScene.CreateComponent<Octree>();

            //Camera
            var camera = _rootScene.CreateMainCamera();
            camera.Orthographic = true;

            //Setup Viewport
            var viewport = new Viewport(this.Context, _rootScene, camera.GetComponent<Camera>());
            Renderer.SetViewport(0, viewport);

            //Configure post effects
            viewport.RenderPath.Append(CoreAssets.PostProcess.Bloom);
            viewport.RenderPath.SetShaderParameter("BloomMix", new Vector2(2f, 2f));

            viewport.RenderPath.Append(CoreAssets.PostProcess.Blur);
            viewport.RenderPath.SetShaderParameter("BlurRadius", 5f);
            viewport.RenderPath.SetShaderParameter("BlurSigma", 5f);

            //Light
            var light = _rootScene.CreateMainLight();
            light.LightType = LightType.Directional;
            light.Node.SetDirection(new Vector3(0f, 0f, 20f));

            //Xamarin Logo
            var xamModel = _rootScene.LoadModel("Logo", "Models/XamarinLogo.mdl", "Materials/XamarinLogoMat.xml");
            xamModel.Node.Position = new Vector3(-6.5f, 0f, 15f);
            xamModel.Node.Scale = new Vector3(75f, 75f, 75f);
            xamModel.Node.Rotation = new Quaternion(0f, 90f, 0f);
            xamModel.Material.SetTechnique(0, CoreAssets.Techniques.NoTextureAlpha);

            AnimateXamLogo(xamModel);
        }

        private async void AnimateXamLogo(StaticModel model)
        {
            var originalColor = model.Material.GetShaderParameter("MatDiffColor");
            var startColor = originalColor;
            startColor.A = 0f;

            var fadeIn = new MaterialColorTween("MatDiffColor", startColor, originalColor, 5f);

            var rotation = new RepeatForever(new RotateBy(6f, 0f, 360f, 0f));

            await Task.WhenAll(
                model.Node.RunActionsAsync(new EaseIn(fadeIn, 1f)),
                model.Node.RunActionsAsync(rotation));
        }
    }
}
