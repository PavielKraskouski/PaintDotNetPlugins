using PaintDotNet;
using PaintDotNet.Effects;
using PaintDotNet.IndirectUI;
using PaintDotNet.PropertySystem;
using System.Drawing;

namespace TrippyRainbow
{
    public class TrippyRainbowEffect : PropertyBasedEffect
    {
        private static readonly Image Icon = new Bitmap(typeof(TrippyRainbowEffect), "TrippyRainbow.png");
        private static readonly ColorBgra[] RainbowColors =
        {
            ColorBgra.FromBgr(255, 0, 255),
            ColorBgra.FromBgr(255, 0, 0),
            ColorBgra.FromBgr(255, 255, 0),
            ColorBgra.FromBgr(0, 255, 0),
            ColorBgra.FromBgr(0, 255, 255),
            ColorBgra.FromBgr(0, 0, 255)
        };

        private double Offset { get; set; }

        public TrippyRainbowEffect() : base("Trippy rainbow", Icon, "Color", new EffectOptions() { Flags = EffectFlags.Configurable }) { }

        protected override PropertyCollection OnCreatePropertyCollection()
        {
            Property[] properties = new Property[]
            {
                new DoubleProperty("Offset", 0.0, 0.0, 1.0)
            };
            return new PropertyCollection(properties);
        }

        protected override ControlInfo OnCreateConfigUI(PropertyCollection props)
        {
            ControlInfo configUI = CreateDefaultConfigUI(props);
            configUI.SetPropertyControlValue("Offset", ControlInfoPropertyNames.SliderLargeChange, 0.25);
            configUI.SetPropertyControlValue("Offset", ControlInfoPropertyNames.SliderSmallChange, 0.05);
            configUI.SetPropertyControlValue("Offset", ControlInfoPropertyNames.UpDownIncrement, 0.01);
            return configUI;
        }

        protected override void OnSetRenderInfo(PropertyBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            Offset = newToken.GetProperty<DoubleProperty>("Offset").Value;
            base.OnSetRenderInfo(newToken, dstArgs, srcArgs);
        }

        protected override void OnRender(Rectangle[] renderRects, int startIndex, int length)
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                Render(DstArgs.Surface, SrcArgs.Surface, renderRects[i]);
            }
        }

        private void Render(Surface dst, Surface src, Rectangle rect)
        {
            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (IsCancelRequested) return;
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    ColorBgra pixel = src[x, y];
                    double intensity = pixel.GetIntensity();
                    double warmness = intensity * (RainbowColors.Length - 1) + Offset * RainbowColors.Length;
                    int integer = (int)warmness;
                    double fraction = warmness - integer;
                    ColorBgra color1 = RainbowColors[integer % RainbowColors.Length];
                    ColorBgra color2 = RainbowColors[(integer + 1) % RainbowColors.Length];
                    ColorBgra result = ColorBgra.Lerp(color1, color2, fraction);
                    result.A = pixel.A;
                    dst[x, y] = result;
                }
            }
        }
    }
}