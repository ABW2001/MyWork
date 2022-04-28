using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace FA2
{
    /// <summary>
    /// Interaction logic for BarCodes.xaml
    /// </summary>
    public partial class BarCodes : Window
    {
        string connectionString = "Data Source=DESKTOP-9IL3HAA;Initial Catalog=StockSystem;Integrated Security=True"; //Connection to database
        public BarCodes()
        {
            InitializeComponent();

            var productDataTable = new DataTable();
            productDataTable.Columns.Add("ProductName", typeof(string));//Adding of columns to datagrid
            productDataTable.Columns.Add("BarCode", typeof(string));

            var productListFromDB = new List<Product>();

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.Stock";
                var sqlCommand = new SqlCommand(query, myConnection);
                myConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.ProductName = reader["ProductName"].ToString();
                        product.BarCode = reader["BarCode"].ToString();
                        productListFromDB.Add(product);
                    }
                    myConnection.Close();
                }

            }
            foreach (Product product in productListFromDB)
            {
                productDataTable.Rows.Add(product.ProductName, product.BarCode);//Adds products  to datagrid
            }
            dataGridBarCode.ItemsSource = productDataTable.DefaultView;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            ProductList list = new ProductList();
            list.Show();
            this.Hide();
        }
    }
}
