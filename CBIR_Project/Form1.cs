using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CBIR_Project.Features;
using CBIR_Project.Models;
using CBIR_Project.Core;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CBIR_Project
{
    public partial class Form1 : Form
    {
        private string queryImagePath;
        private Bitmap queryImage;
        private float[] queryFeatureVector;
        private List<ImageFeature> datasetFeatures = new List<ImageFeature>();

        public Form1()
        {
            InitializeComponent();
        }

        // 1. Chọn ảnh truy vấn
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                queryImagePath = ofd.FileName;
                queryImage = new Bitmap(queryImagePath);

                // Copy riêng cho UI – tránh shared bitmap
                picQuery.Image = (Bitmap)queryImage.Clone();

                lblStatus.Text = "⏳ Đang trích đặc trưng ảnh truy vấn...";
                btnChooseImage.Enabled = false;
                btnSearch.Enabled = false;

                Task.Run(() =>
                {
                    // Copy riêng cho xử lý
                    using (Bitmap processingImage = (Bitmap)queryImage.Clone())
                    {
                        float[] features = FeatureExtractor.ExtractAll(processingImage);

                        Invoke(new Action(() =>
                        {
                            queryFeatureVector = features;
                            lblStatus.Text = "✅ Đã trích xong đặc trưng ảnh truy vấn.";
                            btnChooseImage.Enabled = true;
                            btnSearch.Enabled = true;
                        }));
                    }
                });
            }
        }


        // 2. Trích đặc trưng dataset
        private void btnExtractFeatures_Click(object sender, EventArgs e)
        {
            string datasetPath = Path.Combine(Application.StartupPath, @"..\..\Data\Dataset");
            string csvPath = Path.Combine(Application.StartupPath, @"..\..\Data\features.csv");

            if (!Directory.Exists(datasetPath))
            {
                MessageBox.Show("❌ Thư mục Dataset không tồn tại.");
                return;
            }

            string[] allFiles = Directory.GetFiles(datasetPath, "*.jpg")
                                         .Concat(Directory.GetFiles(datasetPath, "*.png"))
                                         .Concat(Directory.GetFiles(datasetPath, "*.jpeg"))
                                         .ToArray();

            var existingFeatures = File.Exists(csvPath)
                ? FeatureStorage.LoadFromCsv(csvPath)
                : new List<ImageFeature>();

            var existingPaths = new HashSet<string>(existingFeatures.Select(f => f.ImagePath));
            var newFiles = allFiles.Where(path => !existingPaths.Contains(path)).ToList();

            btnExtractFeatures.Enabled = false;
            lblStatus.Text = "⏳ Đang trích đặc trưng...";

            Task.Run(() =>
            {
                var newFeatures = new ConcurrentBag<ImageFeature>();
                int count = 0;

                Parallel.ForEach(newFiles, path =>
                {
                    try
                    {
                        using (Bitmap bmp = new Bitmap(path))
                        using (Bitmap clone = (Bitmap)bmp.Clone()) // ✅ clone để tránh locked
                        {
                            float[] vector = FeatureExtractor.ExtractAll(clone);
                            newFeatures.Add(new ImageFeature(path, vector));
                        }

                        Interlocked.Increment(ref count);
                        Invoke(new Action(() =>
                        {
                            lblStatus.Text = $"⏳ Đang xử lý ảnh mới {count}/{newFiles.Count}...";
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Lỗi ảnh: {path} - {ex.Message}");
                    }
                });

                Invoke(new Action(() =>
                {
                    datasetFeatures = existingFeatures.Concat(newFeatures).ToList();
                    FeatureStorage.SaveToCsv(datasetFeatures, csvPath);
                    lblStatus.Text = $"✅ Đã cập nhật {datasetFeatures.Count} đặc trưng vào CSV.";
                    btnExtractFeatures.Enabled = true;
                }));
            });
        }




        // 3. Tìm kiếm ảnh giống nhất
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (queryFeatureVector == null || datasetFeatures.Count == 0)
            {
                MessageBox.Show("⚠️ Vui lòng chọn ảnh truy vấn và trích đặc trưng trước.");
                return;
            }

            var top5 = datasetFeatures
                .Select(f => new
                {
                    Path = f.ImagePath,
                    Score = SimilarityCalculator.Cosine(queryFeatureVector, f.FeatureVector)
                })
                .OrderByDescending(x => x.Score) // .OrderBy(x => x.Score) dung cho Euclidan cang nho cang giong 
                .Take(5)
                .ToList();

            flowResults.Controls.Clear();

            foreach (var item in top5)
            {
                PictureBox pic = new PictureBox();
                pic.Image = new Bitmap(item.Path);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Width = 120;
                pic.Height = 120;

                Label lbl = new Label();
                lbl.Text = $"Score: {item.Score:F4}";
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Width = 120;

                Panel panel = new Panel();
                panel.Width = 130;
                panel.Height = 150;
                panel.Controls.Add(pic);
                panel.Controls.Add(lbl);
                lbl.Top = pic.Bottom + 2;

                flowResults.Controls.Add(panel);
            }

            lblStatus.Text = "✅ Đã tìm thấy top 5 ảnh giống nhất.";
        }


    }
}
