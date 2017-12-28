using Urho;
using Urho.Actions;

namespace ContestGetStarted.UrhoAssets.Actions
{
    public class MaterialColorTween : FiniteTimeAction
    {
        public string ShaderProperty { get; set; }
        public Color FromColor { get; set; }
        public Color ToColor { get; set; }
        public Material Material { get; set; }

        public MaterialColorTween(string shaderProperty, Color from, Color to, float duration, Material material = null) : base(duration)
        {
            ShaderProperty = shaderProperty;
            FromColor = from;
            ToColor = to;
            Material = material;
        }

        protected override ActionState StartAction(Node target)
        {
            return new ColorTween(this, target);
        }

        public override FiniteTimeAction Reverse()
        {
            return new MaterialColorTween(ShaderProperty, ToColor, FromColor, Duration, Material);
        }

        public class ColorTween : FiniteTimeActionState
        {
            private readonly MaterialColorTween _tween;

            public ColorTween(MaterialColorTween action, Node target) : base(action, target)
            {
                _tween = action;
                if (_tween.Material == null)
                    _tween.Material = target.GetComponent<StaticModel>()?.GetMaterial();
            }

            public override void Update(float time)
            {
                var color = InterpolateColor(_tween.FromColor, _tween.ToColor, time);
                _tween.Material.SetShaderParameter(_tween.ShaderProperty, color);
            }

            private static Color InterpolateColor(Color start, Color end, float time)
            {
                return new Color
                (
                    start.R + (end.R - start.R) * time,
                    start.G + (end.G - start.G) * time,
                    start.B + (end.B - start.B) * time,
                    start.A + (end.A - start.A) * time
                );
            }
        }
    }
}
