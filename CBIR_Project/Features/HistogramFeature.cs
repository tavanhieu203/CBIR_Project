using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CBIR_Project.Features
{
    class HistogramFeature
    {
        public static unsafe float[] Extract(Bitmap bmp, int gridSize = 4, int bins = 16)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int cellW = width / gridSize;
            int cellH = height / gridSize;

            List<float> featureVector = new List<float>();

            // Khóa bitmap để truy cập trực tiếp dữ liệu
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height),
                                           ImageLockMode.ReadOnly,
                                           PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            byte* ptr = (byte*)data.Scan0;

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

                    for (int y = startY; y < endY; y++)
                    {
                        byte* row = ptr + y * stride;
                        for (int x = startX; x < endX; x++)
                        {
                            byte* pixel = row + x * 3;
                            byte b = pixel[0];
                            byte g = pixel[1];
                            byte r = pixel[2];

                            // Tính gray nhanh
                            int gray = (int)(0.299f * r + 0.587f * g + 0.114f * b);
                            int bin = gray * bins / 256;
                            hist[bin]++;
                            pixelCount++;
                        }
                    }

                    // Chuẩn hóa
                    for (int i = 0; i < bins; i++)
                        featureVector.Add(pixelCount > 0 ? (float)hist[i] / pixelCount : 0f);
                }
            }

            bmp.UnlockBits(data);
            return featureVector.ToArray();
        }
    }
}
