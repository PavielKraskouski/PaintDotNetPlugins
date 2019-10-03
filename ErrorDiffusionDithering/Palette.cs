using PaintDotNet;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ErrorDiffusionDithering
{
    public class Palette
    {
        private List<ColorBgra> Colors { get; set; }

        public Palette(List<ColorBgra> colors) => Colors = colors;

        public ColorBgra FindClosestColor(ColorBgra color)
        {
            int index = 0;
            double minDistance = double.PositiveInfinity;
            for (int i = 0; i < Colors.Count; i++)
            {
                double distance = color.SquaredDistanceTo(Colors[i]);
                if (distance < minDistance)
                {
                    index = i;
                    minDistance = distance;
                }
            }
            return Colors[index];
        }

        public static Palette FromSurface(Surface surface, int paletteSize)
        {
            byte gridSize = (byte)Math.Ceiling(Math.Pow(paletteSize, 1.0 / 3.0));
            Partition[] partitions = new Partition[gridSize * gridSize * gridSize];
            double minR = 255; double minG = 255; double minB = 255;
            double maxR = 0; double maxG = 0; double maxB = 0;
            for (int y = 0; y < surface.Height; y++)
            {
                for (int x = 0; x < surface.Width; x++)
                {
                    Vector3 pixel = surface[x, y].ToVector3();
                    if (pixel.X < minR) minR = pixel.X;
                    if (pixel.Y < minG) minG = pixel.Y;
                    if (pixel.Z < minB) minB = pixel.Z;
                    if (pixel.X > maxR) maxR = pixel.X;
                    if (pixel.Y > maxG) maxG = pixel.Y;
                    if (pixel.Z > maxB) maxB = pixel.Z;
                }
            }
            double ki = gridSize / (maxR - minR);
            double kj = gridSize / (maxG - minG);
            double kk = gridSize / (maxB - minB);
            for (int y = 0; y < surface.Height; y++)
            {
                for (int x = 0; x < surface.Width; x++)
                {
                    Vector3 pixel = surface[x, y].ToVector3();
                    int i = Math.Min((int)((pixel.X - minR) * ki), gridSize - 1);
                    int j = Math.Min((int)((pixel.Y - minG) * kj), gridSize - 1);
                    int k = Math.Min((int)((pixel.Z - minB) * kk), gridSize - 1);
                    partitions[i + gridSize * (j + gridSize * k)].AddPixel(pixel);
                }
            }
            Array.Sort(partitions, (x, y) => y.PixelCount.CompareTo(x.PixelCount));
            List<ColorBgra> colors = new List<ColorBgra>();
            for (int i = 0; i < paletteSize; i++)
            {
                if (partitions[i].PixelCount != 0)
                    colors.Add(partitions[i].PixelAverage.ToColorBgra());
            }
            return new Palette(colors);
        }
    }
}