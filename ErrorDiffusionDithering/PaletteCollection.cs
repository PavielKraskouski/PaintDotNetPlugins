using PaintDotNet;
using System.Collections.Generic;

namespace ErrorDiffusionDithering
{
    public static class PaletteCollection
    {
        public static readonly Palette[] Palettes =
        {
            new Palette(new List<ColorBgra>
            {
                ColorBgra.Black,
                ColorBgra.White
            }),
            new Palette(new List<ColorBgra>
            {
                ColorBgra.Black,
                ColorBgra.Maroon,
                ColorBgra.Green,
                ColorBgra.Olive,
                ColorBgra.Navy,
                ColorBgra.Purple,
                ColorBgra.Teal,
                ColorBgra.Silver,
                ColorBgra.Gray,
                ColorBgra.Red,
                ColorBgra.Lime,
                ColorBgra.Yellow,
                ColorBgra.Blue,
                ColorBgra.Fuchsia,
                ColorBgra.Aqua,
                ColorBgra.White
            }),
            new Palette(new List<ColorBgra>
            {
                ColorBgra.Black,
                ColorBgra.Maroon,
                ColorBgra.Green,
                ColorBgra.Olive,
                ColorBgra.Navy,
                ColorBgra.Purple,
                ColorBgra.Teal,
                ColorBgra.Silver,
                ColorBgra.FromBgr(192, 220, 192),
                ColorBgra.FromBgr(240, 202, 166),
                ColorBgra.FromBgr(240, 251, 255),
                ColorBgra.FromBgr(164, 160, 160),
                ColorBgra.Gray,
                ColorBgra.Red,
                ColorBgra.Lime,
                ColorBgra.Yellow,
                ColorBgra.Blue,
                ColorBgra.Fuchsia,
                ColorBgra.Aqua,
                ColorBgra.White
            }),
            new Palette(new List<ColorBgra>
            {
                ColorBgra.White,
                ColorBgra.FromBgr(5, 243, 251),
                ColorBgra.FromBgr(3, 100, 255),
                ColorBgra.FromBgr(7, 9, 221),
                ColorBgra.FromBgr(132, 8, 242),
                ColorBgra.FromBgr(165, 0, 71),
                ColorBgra.FromBgr(211, 0, 0),
                ColorBgra.FromBgr(234, 171, 2),
                ColorBgra.FromBgr(20, 183, 31),
                ColorBgra.FromBgr(18, 100, 0),
                ColorBgra.FromBgr(5, 44, 86),
                ColorBgra.FromBgr(58, 113, 144),
                ColorBgra.Silver,
                ColorBgra.Gray,
                ColorBgra.FromBgr(64, 64, 64),
                ColorBgra.Black
            }),
            new Palette(new List<ColorBgra>
            {
                ColorBgra.White,
                ColorBgra.FromBgr(221, 221, 221),
                ColorBgra.FromBgr(187, 187, 187),
                ColorBgra.FromBgr(153, 153, 153),
                ColorBgra.FromBgr(119, 119, 119),
                ColorBgra.FromBgr(85, 85, 85),
                ColorBgra.FromBgr(51, 51, 51),
                ColorBgra.Black,
                ColorBgra.FromBgr(153, 68, 0),
                ColorBgra.FromBgr(0, 238, 238),
                ColorBgra.FromBgr(0, 204, 0),
                ColorBgra.FromBgr(0, 0, 221),
                ColorBgra.FromBgr(187, 238, 238),
                ColorBgra.FromBgr(0, 136, 85),
                ColorBgra.FromBgr(0, 187, 255),
                ColorBgra.FromBgr(255, 187, 0)
            }),
        };
    }
}