using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.Business.DepencyResolvers.Ninject;
using InciBeyazEsya.Entities.Concrate;
using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net.Mail;
using System.Drawing.Imaging;
using IncıBeyazEsya.Business.ValidationRules.FluentValidation;

namespace InciBeyazEsya.Panels
{
    public partial class ProductManeger : UserControl
    {
        public ProductManeger()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
        }
        MailMessage mesaj = new MailMessage();
        SmtpClient istemci = new SmtpClient();
        IProductService _productService;
        QRCodeEncoder _dc = new QRCodeEncoder();
        QRCodeDecoder _en = new QRCodeDecoder();
        Guid _barcode;
        static string tempNumber = "";
        Random random = new Random();
        int number;

        //https://www.youtube.com/watch?v=8tD5ANtUk1I
        void GetMail()
        {
            try
            {
                istemci.Credentials = new System.Net.NetworkCredential("incibeyaz215@gmail.com", "BECaner234");
                istemci.Port = 587;
                istemci.Host = "smtp.gmail.com";
                for (int i = 0; i < 5; i++)
                {
                    number = random.Next(1, 100);
                    tempNumber += number.ToString();
                }
                istemci.EnableSsl = true;
                mesaj.To.Add("cnr24clp@gmail.com");
                mesaj.From = new MailAddress("incibeyaz215@gmail.com");
                mesaj.Subject = "Onay Mail H.K";
                mesaj.Body = "Yaptiğiniz değişikliğin etkili olması için kodu giriniz : " + tempNumber.ToString();
                istemci.Send(mesaj);
            }
            catch (Exception)
            {

                MessageBox.Show("Bir hata oluştu lütfen sonra tekrar deneyiniz");
            }

        }

        //https://stackoverflow.com/questions/3801275/how-to-convert-image-to-byte-array

        public byte[] ImageToByteArray(System.Drawing.Image image)
        {
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
            else
            {
                throw new NotEmpty("Resim boş geçilemez");
            }

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                tempNumber = "";
                GetMail();
                string rePassword = Interaction.InputBox("Bilgi Girişi", "Lütfen mail adresine gelen onay kodunu giriniz : ", "Örn: 000000", 636, 244);
                if (rePassword == tempNumber)
                {
                    string file = tbxName.Text.ToString();
                    if (pbxBarcode.Image != null)
                    {
                        Image copy = pbxBarcode.Image;
                        copy.Save(Application.StartupPath + "\\Barcodes" + "\\" + file + ".png");
                    }
                    try
                    {
                        _productService.Add(new Product { ProductAbility = tbxAbility.Text.ToString(), ProductInfo = tbxDescription.Text.ToString(), ProductCodes = _barcode.ToString(), UnitPrice = Convert.ToDouble(tbxPrices.Text), ProductName = tbxName.Text.ToString(), Category = tbxCategory.Text.ToString(), ProductImage1 = ImageToByteArray(pictureBox1.Image), ProductImage2 = ImageToByteArray(pictureBox2.Image), ProductImage3 = ImageToByteArray(pictureBox3.Image), ProductColor = tbxColor.Text.ToString(), Amount = Convert.ToInt32(tbxStock.Text), Marka = tbxMarka.Text.ToString() });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Onay kodunuz yanlış girilmiştir ", "Hata !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Bir hata oluştu lütfen tekrar deneyiniz");
            }


        }

        private void btnCreateBarcodeClick(object sender, EventArgs e)
        {
            try
            {
                _barcode = Guid.NewGuid();
                string barcode = _barcode.ToString();
                pbxBarcode.Image = _dc.Encode(_barcode.ToString());
            }
            catch (Exception)
            {

                MessageBox.Show("Barkod yaratilirken bir hata oluştu lütfen tekrar deneyin ");
            }

        }

        private void btnAddPhoto1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png image (*.png)|*.png|jpg image (*.jpg)|*.jpg";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = dialog.FileName.ToString();
            }
        }

        private void btnAddPhoto2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png image (*.png)|*.png|jpg image (*.jpg)|*.jpg";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = dialog.FileName.ToString();
            }
        }

        private void btnAddPhoto3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png image (*.png)|*.png|jpg image (*.jpg)|*.jpg";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.ImageLocation = dialog.FileName.ToString();
            }
        }

        private void ProductManeger_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }
    }
}
