using IncıBeyazEsya.Business.Abstract;
using IncıBeyazEsya.Business.DepencyResolvers.Ninject;
using InciBeyazEsya.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InciBeyazEsya.Panels
{
    public partial class OrderDetailPage : UserControl
    {
        public OrderDetailPage()
        {
            InitializeComponent();
            _usersService = InstanceFactory.GetInstance<IUsersService>();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _townsService = InstanceFactory.GetInstance<ITownsService>();
            _districtsService = InstanceFactory.GetInstance<IDistrictsService>();
            _adressService = InstanceFactory.GetInstance<IAdressService>();
            _citiesService = InstanceFactory.GetInstance<ICitiesService>();
            _ınvoicesService = InstanceFactory.GetInstance<IInvoicesService>();
            _orderService = InstanceFactory.GetInstance<IOrdersService>();
            _orderDetailService = InstanceFactory.GetInstance<IOrderDetailsService>();
            _invoiceDetailService = InstanceFactory.GetInstance<IInvoicesDetailService>();
            _paymentService = InstanceFactory.GetInstance<IPaymentsService>();
            _storedProcedureService = InstanceFactory.GetInstance<IStoredProcedureService>();

        }
        string _male = " ";
        IUsersService _usersService;
        IProductService _productService;
        ITownsService _townsService;
        IDistrictsService _districtsService;
        IAdressService _adressService;
        ICitiesService _citiesService;
        IInvoicesService _ınvoicesService;
        IOrdersService _orderService;
        IInvoicesDetailService _invoiceDetailService;
        IOrderDetailsService _orderDetailService;
        IPaymentsService _paymentService;
        IStoredProcedureService _storedProcedureService;
        Guid _fiecheno;
        Guid _orderNumber;
        Guid _orderdetUniq;
        int _amount;
        int _ıd;
        double _unitPrice;
        double _normalPrice;
        string _paymentType = " ";
        int procAmount;
        Orders order;
        Adress adres;
        User user;
        Invoices invoice;
        Districts districts;
        OrdersDetails orderDetails;
        Product product;

        Product product1;
        Orders order1;
        Adress adres1;
        Districts districts1;
        User user1;
        OrdersDetails orderDetail1;
        Towns town1;
        Cities city1;
        Invoices invoice1;
        InvoiceDetail invoiceDet1;
        Payments payment;
        Districts district1;
        private void OrderDetailPage_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            cbxCities.DisplayMember = "City";
            cbxCities.ValueMember = "Id";
            cbxCities.DataSource = _citiesService.GetAll();



            tbxTextNumber1.MaxLength = 11;
            tbxTextNumber2.MaxLength = 11;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = _productService.GetAll();

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //dataGridView2.DataSource = _storedProcedureService.sqlCommand("b364ab39-7d19-4db5-9cbb-9e66e1a25d4e");

            dataGridView2.ColumnCount = 15;
            dataGridView2.Columns[0].Name = "ProductId";
            dataGridView2.Columns[1].Name = "OrderId";
            dataGridView2.Columns[2].Name = "AdresId";
            dataGridView2.Columns[3].Name = "CityId";
            dataGridView2.Columns[4].Name = "DistrictId";
            dataGridView2.Columns[5].Name = "TownsId";
            dataGridView2.Columns[6].Name = "UserId";
            dataGridView2.Columns[7].Name = "OrderDetailId";
            dataGridView2.Columns[8].Name = "InvoicesId";
            dataGridView2.Columns[9].Name = "TotalPrice";
            dataGridView2.Columns[10].Name = "Status_";
            dataGridView2.Columns[11].Name = "Date_";
            dataGridView2.Columns[12].Name = "NameSurname";
            dataGridView2.Columns[13].Name = "ProductName";
            dataGridView2.Columns[14].Name = "Amount";

        }
        void AddPayments()
        {
            _paymentService.Add(new Payments { Date_ = dtpOrderDate.Value, OrderId = order.Id, PaymentTotal = Convert.ToDouble(tbxTotal.Text), PaymentType = _paymentType });
        }
        void AddInvoiceDetail()
        {
            _invoiceDetailService.Add(new InvoiceDetail { ProductsId = Convert.ToInt32(tbxProductId.Text), InvoiceId = invoice.Id, UnitPrice = _normalPrice, Amount = Convert.ToInt32(tbxStock.Text), LineTotal = Convert.ToDouble(tbxTotal.Text), OrderDetailId = orderDetails.Id });
        }
        void AddOrderDetail()
        {
            _orderdetUniq = Guid.NewGuid();
            orderDetails = _orderDetailService.Add(new OrdersDetails { orderDetailUniq = _orderdetUniq, ProductId = Convert.ToInt32(tbxProductId.Text), UnitPrice = _normalPrice, Amount = Convert.ToInt32(tbxStock.Text), LineTotal = Convert.ToDouble(tbxTotal.Text), OrderId = order.Id });
            orderDetails = _orderDetailService.GetByName(orderDetails.orderDetailUniq.ToString());
        }

        void AddDistricts()
        {
            districts = _districtsService.Add(new Districts { District = tbxDistricts.Text.ToString(), TownsId = (int)cbxTowns.SelectedValue });
            districts = _districtsService.GetByName(districts.District);
        }
        void AddUser()
        {
            user = _usersService.Add(new User { NameSurname = tbxNameSurname.Text.ToString(), Email = tbxMail.Text.ToString(), Gender = _male, TelNumber1 = tbxTextNumber1.Text.ToString(), TelNumber2 = tbxTextNumber1.Text.ToString(), BırthDate = dtpBirthDate.Value, CreateDate = dtpOrderDate.Value });
            user = _usersService.GetByName(user.Email);
        }
        void AddAdress()
        {
            adres = _adressService.Add(new Adress { UserId = user.Id, CıtyId = (int)cbxCities.SelectedValue, TownsId = (int)cbxTowns.SelectedValue, DistrictsId = districts.Id, PostalCode = Convert.ToInt32(tbxPostalCode.Text), AdressText = rbxAdress.Text.ToString() });
            adres = _adressService.GetByName(adres.PostalCode.ToString());
        }
        void AddInvoices()
        {
            _fiecheno = Guid.NewGuid();
            invoice = _ınvoicesService.Add(new Invoices { OrderId = order.Id, Date_ = dtpOrderDate.Value, AdressId = adres.Id, CargoFıchneNo = _fiecheno.ToString(), TotalPrice = Convert.ToDouble(tbxTotal.Text) });
            invoice = _ınvoicesService.GetByName(invoice.CargoFıchneNo.ToString());
        }
        void AddOrders()
        {
            _orderNumber = Guid.NewGuid();
            order = _orderService.Add(new Orders { OrderNo = _orderNumber, UserId = user.Id, Date_ = dtpOrderDate.Value, TotalPrice = Convert.ToDouble(tbxTotal.Text), AddresId = adres.Id });
            order = _orderService.GetByName(order.OrderNo.ToString());
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (procAmount > 0)
            {
                try
                {
                    AddUser();
                    AddDistricts();
                    AddAdress();
                    AddOrders();
                    AddOrderDetail();
                    AddInvoices();
                    AddInvoiceDetail();
                    AddPayments();
                    MessageBox.Show("Siparişiniz başarili bir şekilde alindi");
                    tbxNameSurname.Clear();
                    tbxMail.Clear();
                    tbxTextNumber1.Clear();
                    tbxTextNumber2.Clear();
                    tbxProductId.Clear();
                    tbxStock.Clear();
                    tbxTotal.Clear();
                    tbxDistricts.Clear();
                    tbxPostalCode.Clear();
                    rbxAdress.Clear();
                }
                catch (Exception)
                {

                    MessageBox.Show("Bir Hata Oluştu Lütfen Tekrar Deneyiniz");
                }
            }
            else
            {
                MessageBox.Show("Seçili ürün stoğu yoktur ! ");
            }

        }

        private void rbxMale_CheckedChanged(object sender, EventArgs e)
        {
            _male = "E";
        }

        private void rbxFemale_CheckedChanged(object sender, EventArgs e)
        {
            _male = "K";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
   _ıd = (int)dataGridView1.CurrentRow.Cells[0].Value;
            tbxProductId.Text = _ıd.ToString();
            tbxTotal.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            _normalPrice = (double)dataGridView1.CurrentRow.Cells[2].Value;
            Product procAmounts = _productService.GetById(Convert.ToInt32(tbxProductId.Text));
            procAmount = procAmounts.Id;
            }
            catch (Exception)
            {

                MessageBox.Show(" Ürün bilgileri alınırken bir hata oluştu ");
            }
         

        }

        private void tbxStock_TextChanged(object sender, EventArgs e)
        {
            if (tbxStock.Text != "")
            {
                double sayi1 = Convert.ToDouble(tbxStock.Text);
                double sonuc = _normalPrice * sayi1;
                tbxTotal.Text = sonuc.ToString();
                sonuc = 0;
            }

        }
        private void cbxCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxTowns.DisplayMember = "Town";
            cbxTowns.ValueMember = "Id";
            cbxTowns.DataSource = _townsService.GetAll(cbxCities.SelectedValue.ToString());
        }

        private void rbtCart_CheckedChanged(object sender, EventArgs e)
        {
            _paymentType = "Cart";
        }

        private void rbtCash_CheckedChanged(object sender, EventArgs e)
        {
            _paymentType = "Cash";
        }
        int i = 0;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxGetProduct.Text != null)
                {
                    dataGridView2.Rows.Clear();
                    i = dataGridView2.Rows.Add();
                    SProcedure procedure = _storedProcedureService.sqlCommand(tbxInvoices.Text.ToString());
                    dataGridView2.Rows[i].Cells[0].Value = procedure.ProductId.ToString();
                    dataGridView2.Rows[i].Cells[1].Value = procedure.OrdersId.ToString();
                    dataGridView2.Rows[i].Cells[2].Value = procedure.AdressId.ToString();
                    dataGridView2.Rows[i].Cells[3].Value = procedure.CityId.ToString();
                    dataGridView2.Rows[i].Cells[4].Value = procedure.DistrictsId.ToString();
                    dataGridView2.Rows[i].Cells[5].Value = procedure.TownsId.ToString();
                    dataGridView2.Rows[i].Cells[6].Value = procedure.UserId.ToString();
                    dataGridView2.Rows[i].Cells[7].Value = procedure.OrderDetailId.ToString();
                    dataGridView2.Rows[i].Cells[8].Value = procedure.InvoicesId.ToString();
                    dataGridView2.Rows[i].Cells[9].Value = procedure.TotalPrice.ToString();
                    dataGridView2.Rows[i].Cells[10].Value = procedure.Status.ToString();
                    dataGridView2.Rows[i].Cells[11].Value = procedure.Date_.ToString();
                    dataGridView2.Rows[i].Cells[12].Value = procedure.NameSurname.ToString();
                    dataGridView2.Rows[i].Cells[13].Value = procedure.ProcName.ToString();
                    dataGridView2.Rows[i].Cells[14].Value = procedure.Amount.ToString();

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Barkodu hatali girdiniz ! ! ! ");
                dataGridView2.Rows.Clear();
            }
        }

        private void btnGetProduct_Click(object sender, EventArgs e)
        {
            if (tbxGetProduct.Text != "")
            {
                List<Product> getProduct = new List<Product>();
                getProduct.Add(_productService.GetByName(tbxGetProduct.Text.ToString()));
                dataGridView1.DataSource = getProduct.ToList();
            }
            else
            {
                dataGridView1.DataSource = _productService.GetAll();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                user1.NameSurname = tbxNameSurname.Text.ToString();
                user1.Email = tbxMail.Text.ToString();
                user1.TelNumber1 = tbxTextNumber1.Text.ToString();
                user1.TelNumber2 = tbxTextNumber1.Text.ToString();
                user1.BırthDate = dtpBirthDate.Value;
                _usersService.Update(user1);

                city1.City = cbxCities.Text.ToString();
                _citiesService.Update(city1);
                cbxCities.Text = city1.City;

                town1.Town = cbxTowns.Text.ToString();
                town1.CityId = city1.Id.ToString();
                _townsService.Update(town1);
                cbxTowns.Text = town1.Town;

                districts1.District = tbxDistricts.Text.ToString();
                districts1.TownsId = town1.Id;
                _districtsService.Update(districts1);

                adres1.PostalCode = Convert.ToInt32(tbxPostalCode.Text);
                adres1.AdressText = rbxAdress.Text;
                adres1.TownsId = town1.Id;
                adres1.CıtyId = city1.Id;
                adres1.UserId = user1.Id;
                adres1.DistrictsId = districts1.Id;
                _adressService.Update(adres1);

                order1.AddresId = adres1.Id;
                order1.Date_ = dtpOrderDate.Value;
                order1.UserId = user1.Id;
                _orderService.Update(order1);


                invoice1.AdressId = adres1.Id;
                invoice1.OrderId = order1.Id;
                invoice1.Date_ = dtpOrderDate.Value;
                _ınvoicesService.Update(invoice1);



                orderDetail1.ProductId = procId;
                orderDetail1.LineTotal = Convert.ToDouble(tbxTotal.Text);
                orderDetail1.UnitPrice = product1.UnitPrice;
                orderDetail1.OrderId = order1.Id;
                orderDetail1.Amount = Convert.ToInt32(tbxStock.Text);
                _orderDetailService.Update(orderDetail1);


                invoiceDet1 = _invoiceDetailService.GetById(orderDetailId);
                invoiceDet1.ProductsId = product1.Id;
                invoiceDet1.InvoiceId = invoice1.Id;
                invoiceDet1.Amount = Convert.ToInt32(tbxStock.Text);
                _invoiceDetailService.Update(invoiceDet1);

                payment.PaymentTotal = Convert.ToDouble(tbxTotal.Text);
                payment.Date_ = dtpOrderDate.Value;
                payment.PaymentType = _paymentType;
                _paymentService.Update(payment);

                MessageBox.Show("Başarili bir şekilde güncellenmiştir . ");
                tbxNameSurname.Clear();
                tbxMail.Clear();
                tbxTextNumber1.Clear();
                tbxTextNumber2.Clear();
                tbxProductId.Clear();
                tbxStock.Clear();
                tbxTotal.Clear();
                tbxDistricts.Clear();
                tbxPostalCode.Clear();
                rbxAdress.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu tekrar deneyiniz ! ");

            }


        }
        int orderId;
        int adressId;
        int districtId;
        int cityId;
        int orderDetailId;
        int userId;
        int townsId;
        int invoiceId;
        int procId;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            procId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            tbxProductId.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            product1 = _productService.GetById(procId);
            if (product1.Amount > 0)
            {
                orderId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value);

                order1 = _orderService.GetById(orderId);

                adressId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[2].Value);
                adres1 = _adressService.GetById(adressId);

                tbxPostalCode.Text = adres1.PostalCode.ToString();
                rbxAdress.Text = adres1.AdressText.ToString();

                districtId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[4].Value);
                districts1 = _districtsService.GetById(districtId);
                tbxDistricts.Text = districts1.District.ToString();

                userId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[6].Value);
                user1 = _usersService.GetById(userId);
                tbxNameSurname.Text = user1.NameSurname;
                tbxMail.Text = user1.Email;
                tbxTextNumber1.Text = user1.TelNumber1.ToString();
                tbxTextNumber2.Text = user1.TelNumber2.ToString();

                if (user1.Gender == "E")
                {
                    rbxMale.Checked = true;
                }
                else
                {
                    rbxFemale.Checked = true;
                }

                orderDetailId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[7].Value);
                orderDetail1 = _orderDetailService.GetById(orderDetailId);
                tbxStock.Text = orderDetail1.Amount.ToString();

                townsId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[5].Value);
                town1 = _townsService.GetById(townsId);
                cbxTowns.Text = town1.Town;

                cityId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[3].Value);
                city1 = _citiesService.GetById(cityId);
                cbxCities.Text = city1.City;

                invoiceId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[8].Value);
                invoice1 = _ınvoicesService.GetById(invoiceId);
                invoice1.Date_ = dtpOrderDate.Value;

                invoiceDet1 = _invoiceDetailService.GetById(orderDetailId);
                tbxTotal.Text = invoice1.TotalPrice.ToString();

                payment = _paymentService.GetById(orderDetailId);

                if (payment.PaymentType == "Cart")
                {
                    rbtCart.Checked = true;
                }
                else
                {
                    rbtCash.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("Seçili ürün stokta mevcut değil eklenemez !");
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _invoiceDetailService.Delete(invoiceDet1.Id);
                _orderDetailService.Delete(orderId);
                _ınvoicesService.Delete(invoiceId);

                _paymentService.Delete(payment.Id);
                _orderService.Delete(orderId);
                _adressService.Delete(adressId);
                _districtsService.Delete(districtId);


                _usersService.Delete(userId);
                MessageBox.Show("Ürün basarili bir şekilde silindi ! ");
            }
            catch (Exception)
            {

                MessageBox.Show("Ürün silerken bir hata oluştu");
            }



        }
    }
}
