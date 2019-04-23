namespace ErrorDiffusionDithering
{
    public static class AlgorithmCollection
    {
        public static readonly Algorithm[] Algorithms =
        {
            // Floyd–Steinberg
            new Algorithm(new double[,]
            {
                { 0, 0, 7.0 / 16.0 },
                { 3.0 / 16.0, 5.0 / 16.0, 1.0 / 16.0 }
            }, 1),
            // Jarvis, Judice and Ninke
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 7.0 / 48.0, 5.0 / 48.0 },
                { 1.0 / 16.0, 5.0 / 48.0, 7.0 / 48.0, 5.0 / 48.0, 1.0 / 16.0 },
                { 1.0 / 48.0, 1.0 / 16.0, 5.0 / 48.0, 1.0 / 16.0, 1.0 / 48.0 }
            }, 2),
            // Fan
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 7.0 / 16.0 },
                { 1.0 / 16.0, 3.0 / 16.0, 5.0 / 16.0, 0 }
            }, 2),
            // 4-cell Shiau-Fan
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 1.0 / 2.0 },
                { 1.0 / 8.0, 1.0 / 8.0, 1.0 / 4.0, 0 }
            }, 2),
            // 5-cell Shiau-Fan
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 0, 1.0 / 2.0 },
                { 1.0 / 16.0, 1.0 / 16.0, 1.0 / 8.0, 1.0 / 4.0, 0 }
            }, 3),
            // Stucki
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 4.0 / 21.0, 2.0 / 21.0 },
                { 1.0 / 21.0, 2.0 / 21.0, 4.0 / 21.0, 2.0 / 21.0, 1.0 / 21.0 },
                { 1.0 / 42.0, 1.0 / 21.0, 2.0 / 21.0, 1.0 / 21.0, 1.0 / 42.0 }
            }, 2),
            // Burkes
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 1.0 / 4.0, 1.0 / 8.0 },
                { 1.0 / 16.0, 1.0 / 8.0, 1.0 / 4.0, 1.0 / 8.0, 1.0 / 16.0 }
            }, 2),
            // Sierra
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 5.0 / 32.0, 3.0 / 32.0 },
                { 1.0 / 16.0, 1.0 / 8.0, 5.0 / 32.0, 1.0 / 8.0, 1.0 / 16.0 },
                { 0, 1.0 / 16.0, 3.0 / 32.0, 1.0 / 16.0, 0 }
            }, 2),
            // Two-row Sierra
            new Algorithm(new double[,]
            {
                { 0, 0, 0, 1.0 / 4.0, 3.0 / 16.0 },
                { 1.0 / 16.0, 1.0 / 8.0, 3.0 / 16.0, 1.0 / 8.0, 1.0 / 16.0 }
            }, 2),
            // Sierra Lite
            new Algorithm(new double[,]
            {
                { 0, 0, 1.0 / 2.0 },
                { 1.0 / 4.0, 1.0 / 4.0, 0 }
            }, 1),
            // Atkinson
            new Algorithm(new double[,]
            {
                { 0, 0, 1.0 / 8.0, 1.0 / 8.0 },
                { 1.0 / 8.0, 1.0 / 8.0, 1.0 / 8.0 , 0 },
                { 0, 1.0 / 8.0, 0, 0 }
            }, 1)
        };
    }
}