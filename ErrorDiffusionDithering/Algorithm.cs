namespace ErrorDiffusionDithering
{
    public class Algorithm
    {
        public double[,] Matrix { get; private set; }
        public int MatrixOffset { get; private set; }
        public int MatrixWidth { get; private set; }
        public int MatrixHeight { get; private set; }

        public Algorithm(double[,] matrix, int matrixOffset)
        {
            Matrix = matrix;
            MatrixOffset = matrixOffset;
            MatrixWidth = matrix.GetLength(1);
            MatrixHeight = matrix.GetLength(0);
        }
    }
}