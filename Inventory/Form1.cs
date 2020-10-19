using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing.Text;

namespace Inventory
{
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string qty) : base(qty)
        {

        }
    }
    public class StringFormatException : Exception
    {
        public StringFormatException(string name) : base(name)
        {

        }
    }
    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string price) : base(price)
        {

        }
    }
    public partial class frmAddProduct : Form
    {
        BindingSource showProductList;
        private int _Quantity;
        private double _SellPrice;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                    "Beverages",
                    "Bread/Bakery",
                    "Canned/Jarred Goods",
                    "Dairy",
                    "Frozen Goods",
                    "Meat",
                    "Personal Care",
                    "Other"
            };
            foreach (String categ in ListOfProductCategory)
            {
                cbCategory.Items.Add(categ);

            }
        }
            public string Product_Name(string name)
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException("Insert an accurate name of a product.");
                }

                return name;
            }
            public int Quantity(string qty)
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException("Input a correct amount");
                }
                return Convert.ToInt32(qty);
            }
            public double SellingPrice(string price)
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException("Input the correct price");
                }

                return Convert.ToDouble(price);
            }

            private void btnAddProduct_Click(object sender, EventArgs e)
            {
                try
                {
                    _ProductName = Product_Name(txtProductName.Text);
                    _Category = cbCategory.Text;
                    _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                    _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                    _Description = richTxtDescription.Text;
                    _Quantity = Quantity(txtQuantity.Text);
                    _SellPrice = SellingPrice(txtSellPrice.Text);
                    showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice,
                        _Quantity, _Description));
                    gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    gridViewProductList.DataSource = showProductList;
                }
                catch (StringFormatException se)
                {
                    MessageBox.Show(se.Message);
                }
                catch (NumberFormatException ne)
                {
                    MessageBox.Show(ne.Message);
                }
                catch (CurrencyFormatException ce)
                {
                    MessageBox.Show(ce.Message);
                }
            }
        }
    }


          

       
        
                
       
