using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBIR_Project.Models
{
    public class ImageFeature //Lưu thông tin ảnh và vector đặc trưng tương ứng
    {
        public string ImagePath { get; set; }
        public float[] FeatureVector { get; set; }

        public ImageFeature(string path, float[] vector)
        {
            ImagePath = path;
            FeatureVector = vector;
        }
    }
}
