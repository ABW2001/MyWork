using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   string product, category, selling, cost, quantity, barcode;

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            //Generates random barcode with 10 numbers
            Random random = new Random();
            string barcode = "";

            for (int i = 0; i < 10; i++) 
            {
                barcode = barcode + random.Next(0, 9);
            }
            txtBarcode.Text = barcode;
        }

        private void btnBarCode_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            BarCodes barCodes = new BarCodes();
            barCodes.Show();
            this.Hide();
        }

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void btnProductList_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            ProductList productlList = new ProductList();
            productlList.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Receiving data from text edits
            category = cmbCategory.Text;
            product = txtName.Text;
            selling = txtSelling.Text;
            cost = txtCost.Text;
            quantity = txtQuantity.Text;
            barcode = txtBarcode.Text;

            string connectionString = "Data Source=DESKTOP-9IL3HAA;Initial Catalog=StockSystem;Integrated Security=True"; //Connection to database
            string query = "INSERT INTO dbo.Stock(ProductCategory, ProductName, SellingPrice, CostPrice, Quantity, Barcode)" +
                "VALUES('" + category + "', '" + product + "','" + selling + "','" + cost + "','" + quantity + "', +'" + barcode + "')"; //Insertion into database query
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open(); //Opens database connection
            int result = command.ExecuteNonQuery(); //Execution of query
            if (result > 0)  //Checks if the query was successful
            {
                MessageBox.Show("Product successfully saved");
            }
            else
            {
                MessageBox.Show("The product was not successsfully saved");
            }
            connection.Close();
        }
    }
}
