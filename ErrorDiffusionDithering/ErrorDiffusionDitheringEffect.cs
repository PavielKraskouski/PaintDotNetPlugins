using PaintDotNet;
using PaintDotNet.Effects;
using PaintDotNet.IndirectUI;
using PaintDotNet.PropertySystem;
using System.Drawing;

namespace ErrorDiffusionDithering
{
    public class ErrorDiffusionDitheringEffect : PropertyBasedEffect
    {
        private static readonly Image Icon = new Bitmap(typeof(ErrorDiffusionDitheringEffect), "ErrorDiffusionDithering.png");

        private int TargetPalette { get; set; }
        private int AutomaticPaletteSize { get; set; }
        private int DitheringAlgorithm { get; set; }
        private bool SerpentineScanning { get; set; }

        public ErrorDiffusionDitheringEffect() : base("Error diffusion dithering", Icon, "Stylize", new EffectOptions() { Flags = EffectFlags.Configurable, RenderingSchedule = EffectRenderingSchedule.None }) { }

        protected override PropertyCollection OnCreatePropertyCollection()
        {
            Property[] properties =
            {
                new StaticListChoiceProperty("Target palette", new object[] { 0, 1, 2, 3, 4, 5 }),
                new Int32Property("Automatic palette size", 16, 2, 32),
                new StaticListChoiceProperty("Dithering algorithm", new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }),
                new BooleanProperty("Serpentine scanning", true)
            };
            PropertyCollectionRule[] rules =
            {
                new ReadOnlyBoundToValueRule<object, StaticListChoiceProperty>("Automatic palette size", "Target palette", 5, true)
            };
            return new PropertyCollection(properties, rules);
        }

        protected override ControlInfo OnCreateConfigUI(PropertyCollection props)
        {
            ControlInfo configUI = CreateDefaultConfigUI(props);
            PropertyControlInfo targetPalette = configUI.FindControlForPropertyName("Target palette");
            targetPalette.SetValueDisplayName(0, "Black and white 2-color palette");
            targetPalette.SetValueDisplayName(1, "Microsoft Windows default 16-color palette");
            targetPalette.SetValueDisplayName(2, "Microsoft Windows default 20-color palette");
            targetPalette.SetValueDisplayName(3, "Apple Macintosh default 16-color palette");
            targetPalette.SetValueDisplayName(4, "RISC OS default 16-color palette");
            targetPalette.SetValueDisplayName(5, "Automatic palette");
            PropertyControlInfo ditheringAlgorithm = configUI.FindControlForPropertyName("Dithering algorithm");
            ditheringAlgorithm.SetValueDisplayName(0, "Floyd–Steinberg");
            ditheringAlgorithm.SetValueDisplayName(1, "Jarvis, Judice and Ninke");
            ditheringAlgorithm.SetValueDisplayName(2, "Fan");
            ditheringAlgorithm.SetValueDisplayName(3, "4-cell Shiau-Fan");
            ditheringAlgorithm.SetValueDisplayName(4, "5-cell Shiau-Fan");
            ditheringAlgorithm.SetValueDisplayName(5, "Stucki");
            ditheringAlgorithm.SetValueDisplayName(6, "Burkes");
            ditheringAlgorithm.SetValueDisplayName(7, "Sierra");
            ditheringAlgorithm.SetValueDisplayName(8, "Two-row Sierra");
            ditheringAlgorithm.SetValueDisplayName(9, "Sierra Lite");
            ditheringAlgorithm.SetValueDisplayName(10, "Atkinson");
            configUI.SetPropertyControlValue("Serpentine scanning", ControlInfoPropertyNames.DisplayName, string.Empty);
            configUI.SetPropertyControlValue("Serpentine scanning", ControlInfoPropertyNames.Description, "Serpentine scanning");
            return configUI;
        }

        protected override void OnSetRenderInfo(PropertyBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            TargetPalette = (int)newToken.GetProperty<StaticListChoiceProperty>("Target palette").Value;
            AutomaticPaletteSize = newToken.GetProperty<Int32Property>("Automatic palette size").Value;
            DitheringAlgorithm = (int)newToken.GetProperty<StaticListChoiceProperty>("Dithering algorithm").Value;
            SerpentineScanning = newToken.GetProperty<BooleanProperty>("Serpentine scanning").Value;
            base.OnSetRenderInfo(newToken, dstArgs, srcArgs);
        }

        protected override void OnRender(Rectangle[] renderRects, int startIndex, int length)
        {
            PdnRegion selection = EnvironmentParameters.GetSelection(SrcArgs.Bounds);
            Rectangle selectionBounds = selection.GetBoundsInt();
            Render(DstArgs.Surface, SrcArgs.Surface, selection, selectionBounds);
        }

        private void Render(Surface dst, Surface src, PdnRegion selection, Rectangle rect)
        {
            dst.CopySurface(src, selection);
            Algorithm algorithm = AlgorithmCollection.Algorithms[DitheringAlgorithm];
            Palette palette = TargetPalette < 5 ? PaletteCollection.Palettes[TargetPalette] : Palette.FromSurface(src, AutomaticPaletteSize);
            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (IsCancelRequested) return;
                bool leftToRight = !SerpentineScanning || y % 2 == 0;
                int x1 = leftToRight ? rect.Left : rect.Right - 1;
                int x2 = leftToRight ? rect.Right : rect.Left - 1;
                int step = leftToRight ? 1 : -1;
                for (int x = x1; x != x2; x += step)
                {
                    if (selection.IsVisible(x, y))
                    {
                        ColorBgra oldColor = dst[x, y];
                        ColorBgra newColor = palette.FindClosestColor(oldColor);
                        newColor.A = oldColor.A;
                        dst[x, y] = newColor;
                        int redError = oldColor.R - newColor.R;
                        int greenError = oldColor.G - newColor.G;
                        int blueError = oldColor.B - newColor.B;
                        for (int i = 0; i < algorithm.MatrixHeight; i++)
                        {
                            for (int j = 0; j < algorithm.MatrixWidth; j++)
                            {
                                int k = y + i;
                                int l = x + (j * step) - (algorithm.MatrixOffset * step);
                                double coefficient = algorithm.Matrix[i, j];
                                if (coefficient != 0 && selection.IsVisible(l, k))
                                {
                                    ColorBgra color = dst[l, k];
                                    double redOffset = redError * coefficient;
                                    double greenOffset = greenError * coefficient;
                                    double blueOffset = blueError * coefficient;
                                    color.R = (color.R + redOffset).ToByte();
                                    color.G = (color.G + greenOffset).ToByte();
                                    color.B = (color.B + blueOffset).ToByte();
                                    dst[l, k] = color;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}