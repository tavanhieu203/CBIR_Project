using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBIR_Project.Features
{
    class FeatureExtractor
    {
        public double[] ExtractFeatures(string imagePath)
        {
            Bitmap img = new Bitmap(imagePath);
            var lbpVector = LBPFeature.ExtractLBPFeatures(img);
            return lbpVector;
        }
    }
}
