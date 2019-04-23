using PaintDotNet;
using PaintDotNet.Effects;
using PaintDotNet.PropertySystem;
using System.Drawing;

namespace Threshold
{
    public class ThresholdEffect : PropertyBasedEffect
    {
        private static readonly Image Icon = new Bitmap(typeof(ThresholdEffect), "Threshold.png");

        private int Threshold { get; set; }

        public ThresholdEffect() : base("Threshold", Icon, "Color", new EffectOptions() { Flags = EffectFlags.Configurable }) { }

        protected override PropertyCollection OnCreatePropertyCollection()
        {
            Property[] properties = new Property[]
            {
                new Int32Property("Threshold", 128, 0, 256)
            };
            return new PropertyCollection(properties);
        }

        protected override void OnSetRenderInfo(PropertyBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            Threshold = newToken.GetProperty<Int32Property>("Threshold").Value;
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
                    double brightness = pixel.GetIntensityByte();
                    if (brightness < Threshold) dst[x, y] = ColorBgra.FromBgra(0, 0, 0, pixel.A);
                    else dst[x, y] = ColorBgra.FromBgra(255, 255, 255, pixel.A);
                }
            }
        }
    }
}