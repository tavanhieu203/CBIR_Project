using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CBIR_Project.Features
{
    class FeatureExtractor
    {
        
         public static float[] ExtractAll(Bitmap bmp) // chạy cả 4 đặc trưng
        {
            var hsv = HistogramFeature.Extract(bmp);
            var lbp = LBPFeature.Extract(bmp);
            var gabor = GaborFeature.Extract(bmp);
            var edge = EdgeFeature.Extract(bmp);

            return hsv.Concat(lbp).Concat(gabor).Concat(edge).ToArray();
        }
        
        /*
        public static float[] ExtractAll(Bitmap bmp)
        {
            // Chỉ dùng đặc trưng cạnh để test
            //return EdgeFeature.Extract(bmp);
            //dùng đăc trưng LBP
            return LBPFeature.Extract(bmp);
        }*/
    }
}
