using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBIR_Project.Features
{
    class LBPFeature
    {
        /// <summary>
        /// Tính đặc trưng LBP cho một ảnh (grayscale)
        /// </summary>
        public static double[] ExtractLBPFeatures(Bitmap image)
        {
            // Chuyển ảnh sang grayscale nếu chưa
            Bitmap gray = ToGray(image);

            int width = gray.Width;
            int height = gray.Height;

            // Histogram 256 phần tử
            double[] hist = new double[256];

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int center = gray.GetPixel(x, y).R;
                    int code = 0;

                    // 8 điểm lân cận theo thứ tự vòng quanh
                    code |= (gray.GetPixel(x - 1, y - 1).R >= center ? 1 : 0) << 7;
                    code |= (gray.GetPixel(x, y - 1).R >= center ? 1 : 0) << 6;
                    code |= (gray.GetPixel(x + 1, y - 1).R >= center ? 1 : 0) << 5;
                    code |= (gray.GetPixel(x + 1, y).R >= center ? 1 : 0) << 4;
                    code |= (gray.GetPixel(x + 1, y + 1).R >= center ? 1 : 0) << 3;
                    code |= (gray.GetPixel(x, y + 1).R >= center ? 1 : 0) << 2;
                    code |= (gray.GetPixel(x - 1, y + 1).R >= center ? 1 : 0) << 1;
                    code |= (gray.GetPixel(x - 1, y).R >= center ? 1 : 0) << 0;

                    hist[code]++;

                }
            }

            // Chuẩn hóa histogram
            double sum = 0;
            foreach (var h in hist) sum += h;
            if (sum > 0)
            {
                for (int i = 0; i < hist.Length; i++)
                    hist[i] /= sum;
            }

            return hist;
        }

        private static Bitmap ToGray(Bitmap bmp)
        {
            Bitmap gray = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int g = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            return gray;
        }
    }
}

