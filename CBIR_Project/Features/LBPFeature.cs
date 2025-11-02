using System;
using System.Drawing;
using System.Linq;

namespace CBIR_Project.Features
{
    class LBPFeature
    {
        /// <summary>
        /// Trích xuất đặc trưng LBP (Local Binary Pattern) từ ảnh đầu vào.
        /// Trả về vector 256 phần tử (histogram).
        /// </summary>
        public static float[] Extract(Bitmap image)
        {
            Bitmap gray = ToGray(image);
            int width = gray.Width;
            int height = gray.Height;

            // Histogram 256 phần tử
            float[] hist = new float[256];

            // Duyệt từng pixel (trừ viền)
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int center = gray.GetPixel(x, y).R;
                    int code = 0;

                    // 8 điểm lân cận (theo chiều kim đồng hồ)
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
            float sum = hist.Sum();
            if (sum > 0)
            {
                for (int i = 0; i < hist.Length; i++)
                    hist[i] /= sum;
            }

            return hist;
        }

        /// <summary>
        /// Chuyển ảnh màu sang grayscale.
        /// </summary>
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
