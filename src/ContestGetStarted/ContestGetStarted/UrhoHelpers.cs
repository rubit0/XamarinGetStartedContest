using System.Linq;
using Urho;

namespace ContestGetStarted
{
    public static class SceneHelpers
    {
        public static Application Context { get; set; }

        public static T GetComponent<T>(this Node node) where T : Component
        {
            return node.Components.FirstOrDefault(c => c is T) as T;
        }

        public static Vector3 CreateUniformVector3(float value)
        {
            return new Vector3(value, value, value);
        }

        public static StaticModel CreateSimpleBox(this Scene scene, string name)
        {
            var node = scene.CreateChild(name: name);
            node.Position = new Vector3(0f, 0f, 5f);
            var model = node.CreateComponent<StaticModel>();
            model.Model = CoreAssets.Models.Box;

            return model;
        }

        public static StaticModel LoadModel(this Scene scene, string name, string modelPath, string materialPath = null)
        {
            var node = scene.CreateChild(name: name);
            node.Position = new Vector3(0f, 0f, 0f);
            var model = node.CreateComponent<StaticModel>();
            model.Model = Context.ResourceCache.GetModel(modelPath);
            if (materialPath != null)
                model.Material = Context.ResourceCache.GetMaterial(materialPath);

            return model;
        }

        public static Camera CreateMainCamera(this Scene scene)
        {
            var node = scene.CreateChild(name: "MainCamera");
            var cam = node.CreateComponent<Camera>();

            return cam;
        }

        public static Light CreateMainLight(this Scene scene)
        {
            var lightNode = scene.CreateChild(name: "Light");
            lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
            var light = lightNode.CreateComponent<Light>();

            return light;
        }
    }
}
