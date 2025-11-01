using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CBIR_Project.Features
{
    public class EdgeFeature
    {
        public static float[] Extract(Bitmap bmp, int gridSize = 4)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            byte[,] gray = ToGrayscale(bmp);
            int[,] gx = Sobel(gray, true);
            int[,] gy = Sobel(gray, false);

            float[,] angle = new float[height, width];
            float[,] magnitude = new float[height, width];

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int dx = gx[y, x];
                    int dy = gy[y, x];
                    magnitude[y, x] = (float)Math.Sqrt(dx * dx + dy * dy);
                    angle[y, x] = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
                    if (angle[y, x] < 0) angle[y, x] += 180;
                }
            }

            int cellW = width / gridSize;
            int cellH = height / gridSize;
            List<float> featureVector = new List<float>();

            for (int gyCell = 0; gyCell < gridSize; gyCell++)
            {
                for (int gxCell = 0; gxCell < gridSize; gxCell++)
                {
                    int startX = gxCell * cellW;
                    int startY = gyCell * cellH;
                    int endX = Math.Min(startX + cellW, width);
                    int endY = Math.Min(startY + cellH, height);

                    int[] hist = new int[5]; // [0°, 45°, 90°, 135°, no edge]

                    for (int y = startY; y < endY; y++)
                    {
                        for (int x = startX; x < endX; x++)
                        {
                            float mag = magnitude[y, x];
                            float ang = angle[y, x];

                            if (mag < 20) hist[4]++;
                            else if (ang < 22.5 || ang >= 157.5) hist[0]++;
                            else if (ang < 67.5) hist[1]++;
                            else if (ang < 112.5) hist[2]++;
                            else hist[3]++;
                        }
                    }

                    int total = hist.Sum();
                    for (int i = 0; i < 5; i++)
                        featureVector.Add(total > 0 ? (float)hist[i] / total : 0f);
                }
            }

            return featureVector.ToArray();
        }

        private static byte[,] ToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width, h = bmp.Height;
            byte[,] gray = new byte[h, w];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    gray[y, x] = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                }
            return gray;
        }

        private static int[,] Sobel(byte[,] gray, bool isX)
        {
            int[,] result = new int[gray.GetLength(0), gray.GetLength(1)];
            int[,] kernel = isX
                ? new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } }
                : new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int y = 1; y < gray.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < gray.GetLength(1) - 1; x++)
                {
                    int sum = 0;
                    for (int ky = -1; ky <= 1; ky++)
                        for (int kx = -1; kx <= 1; kx++)
                            sum += gray[y + ky, x + kx] * kernel[ky + 1, kx + 1];
                    result[y, x] = sum;
                }
            }
            return result;
        }
    }
}

