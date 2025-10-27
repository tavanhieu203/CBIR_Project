using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CBIR_Project.Core;
using CBIR_Project.Features;

namespace CBIR_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string queryImagePath = string.Empty;
        private string datasetPath = Path.Combine(Application.StartupPath, "Data", "Dataset");

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                queryImagePath = ofd.FileName;
                picQuery.Image = Image.FromFile(queryImagePath);
            }
        }

        private void btnExtractFeatures_Click(object sender, EventArgs e)
        {
            var extractor = new FeatureExtractor();
            extractor.ExtractDatasetFeatures(datasetPath);
            MessageBox.Show("✅ Đã trích đặc trưng cho toàn bộ dataset!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(queryImagePath))
            {
                MessageBox.Show("Vui lòng chọn ảnh truy vấn trước!");
                return;
            }

            var watch = Stopwatch.StartNew();
            var extractor = new FeatureExtractor();
            var queryVector = extractor.ExtractFeatures(queryImagePath);

            var results = SimilarityCalculator.SearchTopSimilar(queryVector, "Data/features.csv", topN: 10);
            watch.Stop();

            flowResults.Controls.Clear();
            foreach (var r in results)
            {
                PictureBox pic = new PictureBox
                {
                    Image = Image.FromFile(r.ImagePath),
                    Width = 120,
                    Height = 120,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Margin = new Padding(8)
                };
                flowResults.Controls.Add(pic);
            }

            statusBar.Items[0].Text = $"Tìm kiếm hoàn tất trong {watch.ElapsedMilliseconds} ms";
        }
    }
}
