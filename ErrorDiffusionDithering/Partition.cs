using System.Numerics;

namespace ErrorDiffusionDithering
{
    public struct Partition
    {
        public int PixelCount { get; private set; }
        public Vector3 PixelSum { get; private set; }
        public Vector3 PixelAverage { get => PixelSum / PixelCount; }

        public void AddPixel(Vector3 pixel)
        {
            PixelCount++;
            PixelSum += pixel;
        }
    }
}