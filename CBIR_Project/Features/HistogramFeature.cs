using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBIR_Project.Features
{
    class HistogramFeature
    {
        public static float[] Extract(Bitmap bmp, int gridSize = 4, int bins = 16)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int cellW = width / gridSize;
            int cellH = height / gridSize;

            List<float> featureVector = new List<float>();

            // Duyệt theo từng ô (cell)
            for (int gy = 0; gy < gridSize; gy++)
            {
                for (int gx = 0; gx < gridSize; gx++)
                {
                    int startX = gx * cellW;
                    int startY = gy * cellH;
                    int endX = Math.Min(startX + cellW, width);
                    int endY = Math.Min(startY + cellH, height);

                    int[] hist = new int[bins];
                    int pixelCount = 0;

                    // Tính histogram kênh độ sáng (grayscale)
                    for (int y = startY; y < endY; y++)
                    {
                        for (int x = startX; x < endX; x++)
                        {
                            Color c = bmp.GetPixel(x, y);
                            byte gray = (byte)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                            int bin = (int)((gray / 256.0) * bins);
                            if (bin >= bins) bin = bins - 1;
                            hist[bin]++;
                            pixelCount++;
                        }
                    }

                    // Chuẩn hóa histogram trong mỗi ô
                    for (int i = 0; i < bins; i++)
                        featureVector.Add(pixelCount > 0 ? (float)hist[i] / pixelCount : 0f);
                }
            }

            return featureVector.ToArray();
        }
    }
}
