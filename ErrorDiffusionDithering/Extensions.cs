using PaintDotNet;
using System;
using System.Numerics;

namespace ErrorDiffusionDithering
{
    public static class Extensions
    {
        public static double SquaredDistanceTo(this ColorBgra color1, ColorBgra color2)
        {
            return Math.Pow(color2.R - color1.R, 2) + Math.Pow(color2.G - color1.G, 2) + Math.Pow(color2.B - color1.B, 2);
        }

        public static byte ToByte(this float value)
        {
            return value < 0.0f ? (byte)0 : value > 255.0f ? (byte)255 : Convert.ToByte(value);
        }

        public static byte ToByte(this double value)
        {
            return value < 0.0 ? (byte)0 : value > 255.0 ? (byte)255 : Convert.ToByte(value);
        }

        public static ColorBgra ToColorBgra(this Vector3 color)
        {
            return ColorBgra.FromBgr(color.Z.ToByte(), color.Y.ToByte(), color.X.ToByte());
        }

        public static Vector3 ToVector3(this ColorBgra color)
        {
            return new Vector3(color.R, color.G, color.B);
        }
    }
}