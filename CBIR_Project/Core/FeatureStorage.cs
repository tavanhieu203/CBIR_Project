using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CBIR_Project.Models;

namespace CBIR_Project.Core
{
    public static class FeatureStorage
    {
        public static void SaveToCsv(List<ImageFeature> features, string filePath)
        {
            var sb = new StringBuilder();
            foreach (var f in features)
            {
                sb.Append(f.ImagePath);
                sb.Append(",");
                sb.Append(string.Join(",", f.FeatureVector));
                sb.AppendLine();
            }
            File.WriteAllText(filePath, sb.ToString());
        }

        public static List<ImageFeature> LoadFromCsv(string filePath)
        {
            var list = new List<ImageFeature>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                string path = parts[0];
                float[] vector = parts.Skip(1).Select(float.Parse).ToArray();
                list.Add(new ImageFeature(path, vector));
            }
            return list;
        }
    }
}
