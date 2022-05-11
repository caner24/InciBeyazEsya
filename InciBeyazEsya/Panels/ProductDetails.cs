using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MessagingToolkit.QRCode.Codec;
using ZXing;
using System.IO;
using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.Business.DepencyResolvers.Ninject;
using InciBeyazEsya.Entities.Concrate;

namespace InciBeyazEsya.Panels
{
    public partial class ProductDetails : UserControl
    {
        public ProductDetails()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
        }
        IProductService _productService;
        QRCodeDecoder _en = new QRCodeDecoder();
        FilterInfoCollection filtercollection;
        VideoCaptureDevice vcd;
        string productBarcode;
        int Id;

        public Image ByteArrayToImage(byte[] byteArray)
        {

            MemoryStream memoryStream = new MemoryStream(byteArray);
            Image returnImage = Image.FromStream(memoryStream);
            return returnImage;
        }

        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                BarcodeReader brd = new BarcodeReader();
                Result snc = brd.Decode((Bitmap)pictureBox1.Image);
                if (snc != null)
                {
                    productBarcode = snc.ToString();
                    if (productBarcode != null)
                    {
                        try
                        {
                            dataGridView1.DataSource = _productService.GetAll(productBarcode);
                            Id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                            pbxImage1.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[8].Value);
                            pbxImage2.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[9].Value);
                            pbxImage3.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[10].Value);
                            tbxId.Text = Id.ToString();
                            tbxName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            tbxDescription.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                            tbxAbility.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                            tbxPrices.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            tbxAmount.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                            tbxColor.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                            tbxMarka.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                            tbxCategory.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Bu barkod mevcut değildir ");
                            throw;
                        }


                    }
                    timer1.Stop();

                }
            }
        }

        // https://csharp.hotexamples.com/examples/AForge.Video.DirectShow/VideoCaptureDevice/-/php-videocapturedevice-class-examples.html
        private void button1_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(filtercollection[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame;
            vcd.Start();
            timer1.Start();
        }
        //// https://csharp.hotexamples.com/examples/AForge.Video.DirectShow/VideoCaptureDevice/-/php-videocapturedevice-class-examples.html
        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void ProductDetails_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = _productService.GetAll();
            filtercollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in filtercollection)
            {
                comboBox1.Items.Add(item.Name);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                tbxId.Text = Id.ToString();
                tbxName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                tbxDescription.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tbxAbility.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                tbxPrices.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                tbxAmount.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                tbxMarka.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                tbxColor.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                tbxCategory.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                pbxImage1.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[8].Value);
                pbxImage2.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[9].Value);
                pbxImage3.Image = ByteArrayToImage((byte[])dataGridView1.CurrentRow.Cells[10].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("Boşta kalan alanlar mevcut ! ");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Update(new Product { Marka = tbxMarka.Text.ToString(), Id = Convert.ToInt32(tbxId.Text), ProductName = tbxName.Text, ProductInfo = tbxDescription.Text.ToString(), ProductAbility = tbxAbility.Text.ToString(), UnitPrice = Convert.ToDouble(tbxPrices.Text), ProductColor = tbxColor.Text.ToString(), Category = tbxCategory.Text, Amount = Convert.ToInt32(tbxAmount.Text), ProductImage1 = ImageToByteArray(pbxImage1.Image), ProductImage2 = ImageToByteArray(pbxImage2.Image), ProductImage3 = ImageToByteArray(pbxImage3.Image) });
                MessageBox.Show("Güncelleme Başarili");
            }
            catch (Exception)
            {

                MessageBox.Show("Bir daha oluştu Lütfen Boş kıssımları doldurunuz . ");
            }

        }

        private void btnAddPhoto1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "png image (*.png)|*.png";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = dialog.FileName.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Doğru Resim Formati Seçiniz");
            }

        }

        private void btnAddPhoto2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "png image (*.png)|*.png";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pbxImage2.ImageLocation = dialog.FileName.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Doğru Resim Formati Seçiniz");
            }

        }

        private void btnAddPhoto3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "png image (*.png)|*.png";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pbxImage3.ImageLocation = dialog.FileName.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Doğru Resim Formati Seçiniz");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                Product product = _productService.GetById(Convert.ToInt32(tbxId.Text));
                MessageBox.Show(product.ProductName);
                File.Delete(Application.StartupPath + "\\Barcodes" + "\\" + product.ProductName + ".png");
                _productService.Delete(Convert.ToInt32(tbxId.Text));



            }
            catch (Exception)
            {

                MessageBox.Show("Bir hata oluştu ");
            }

        }
    }
}
