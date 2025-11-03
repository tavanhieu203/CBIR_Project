using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace CBIR_Project.Features
{
    public static class GaborFeature
    {
        // Config (có thể thay đổi sau)
        private static readonly int[] KernelSizes = { 7, 11 };
        private static readonly double[] Orientations = { 0, Math.PI / 4, Math.PI / 2, 3 * Math.PI / 4 };

        public static float[] Extract(Bitmap bmp)
        {
            Bitmap gray = ToGray(bmp);
            byte[,] img = BitmapToArray(gray);
            gray.Dispose();

            float[] features = new float[KernelSizes.Length * Orientations.Length * 2];
            int idx = 0;

            Parallel.For(0, KernelSizes.Length, ks =>
            {
                int ksize = KernelSizes[ks];
                int half = ksize / 2;
                double sigma = 0.56 * ksize;
                double lambda = 3.0;
                double gamma = 0.5;

                foreach (double theta in Orientations)
                {
                    double cosT = Math.Cos(theta);
                    double sinT = Math.Sin(theta);

                    double mean = 0, variance = 0;
                    int count = 0;

                    for (int y = half; y < img.GetLength(1) - half; y++)
                    {
                        for (int x = half; x < img.GetLength(0) - half; x++)
                        {
                            double sum = 0;

                            for (int ky = -half; ky <= half; ky++)
                            {
                                for (int kx = -half; kx <= half; kx++)
                                {
                                    double xr = kx * cosT + ky * sinT;
                                    double yr = -kx * sinT + ky * cosT;

                                    double gabor = Math.Exp(-(xr * xr + gamma * gamma * yr * yr) / (2 * sigma * sigma))
                                                   * Math.Cos(2 * Math.PI * xr / lambda);

                                    sum += img[x + kx, y + ky] * gabor;
                                }
                            }

                            sum = Math.Abs(sum);
                            mean += sum;
                            variance += sum * sum;
                            count++;
                        }
                    }

                    mean /= count;
                    variance = Math.Sqrt(variance / count - mean * mean);

                    int safeIndex = System.Threading.Interlocked.Add(ref idx, 2) - 2;
                    features[safeIndex] = (float)mean;
                    features[safeIndex + 1] = (float)variance;
                }
            });

            return features;
        }


        private static Bitmap ToGray(Bitmap bmp)
        {
            Bitmap gray = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(gray))
            {
                var cm = new ColorMatrix(new float[][] {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0,0,0,1,0},
                    new float[] {0,0,0,0,1}
                });

                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, ia);
            }
            return gray;
        }

        private static byte[,] BitmapToArray(Bitmap bmp)
        {
            int w = bmp.Width, h = bmp.Height;
            byte[,] arr = new byte[w, h];

            var data = bmp.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                for (int y = 0; y < h; y++)
                {
                    byte* row = (byte*)data.Scan0 + y * data.Stride;
                    for (int x = 0; x < w; x++)
                        arr[x, y] = row[x * 3]; // vì đã grayscale dạng RGB = (g,g,g)
                }
            }

            bmp.UnlockBits(data);
            return arr;
        }
    }
}
