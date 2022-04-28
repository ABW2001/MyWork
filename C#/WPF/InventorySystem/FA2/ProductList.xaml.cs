using Microsoft.Build.Tasks.Deployment.Bootstrapper;
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
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        string connectionString = "Data Source=DESKTOP-9IL3HAA;Initial Catalog=StockSystem;Integrated Security=True";
        int ID;
        public ProductList()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        public void RefreshDataGrid() 
        {
            var productDataTable = new DataTable();
            productDataTable.Columns.Add("ProductCategory", typeof(string));
            productDataTable.Columns.Add("ProductName", typeof(string));
            productDataTable.Columns.Add("CostPrice", typeof(double));
            productDataTable.Columns.Add("SellingPrice", typeof(double));
            productDataTable.Columns.Add("Quantity", typeof(int));
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
                        product.ProductCategory = reader["ProductCategory"].ToString();
                        product.ProductName = reader["ProductName"].ToString();
                        product.CostPrice = double.Parse(reader["CostPrice"].ToString());
                        product.SellingPrice = double.Parse(reader["SellingPrice"].ToString());
                        product.Quantity = int.Parse(reader["Quantity"].ToString());
                        product.BarCode = reader["BarCode"].ToString();
                        productListFromDB.Add(product);
                    }
                    myConnection.Close();
                }

            }
            foreach (Product product in productListFromDB)
            {
                productDataTable.Rows.Add(product.ProductCategory, product.ProductName, product.CostPrice, product.SellingPrice, product.Quantity, product.BarCode);
            }
            ProductsDataGrid.ItemsSource = productDataTable.DefaultView;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string query = "DELETE FROM dbo.Stock WHERE ID = " + ID;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            try { cmd.ExecuteNonQuery(); MessageBox.Show("Deletion successful"); }
            catch (Exception ex){ MessageBox.Show("Deletion error: " + ex.Message); }
            finally { conn.Close(); RefreshDataGrid(); }             
        }
            

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string category = cmbCategory.Text;
            string productName = txtName.Text;
            string costPrice = txtSelling.Text;
            string sellingPrice = txtSelling.Text;
            string quantity = txtQuantity.Text;
            string barCode = txtBarcode.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE dbo.Stock SET ProductCategory = '" + category + "', ProductName = '" + productName + "', SellingPrice = '" + sellingPrice + 
                "', CostPrice = '" + costPrice + "', Quantity = '" + quantity + "', Barcode = '" + barCode + "' WHERE ID = " + ID;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            try { cmd.ExecuteNonQuery(); MessageBox.Show("Update Successful"); }
            catch (Exception ex) { MessageBox.Show("Update error: " + ex.Message); }
            finally { conn.Close(); RefreshDataGrid(); }
        }

        private void btnBarCode_Click(object sender, RoutedEventArgs e)
        {
            //Page navigation
            BarCodes bc = new BarCodes();
            bc.Show();
            this.Hide();
        }

        private void ProductsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string category = "", productName = "", barCode = "", quantity = "", sellingPrice = "", costPrice = "";

            //Receiving data from the selected data grid cells            
            var data = ProductsDataGrid.SelectedItem;
            if (data != null) //Prevents exception after deleting a cell
            {
                category = ((TextBlock)ProductsDataGrid.SelectedCells[0].Column.GetCellContent(data)).Text;
                productName = ((TextBlock)ProductsDataGrid.SelectedCells[1].Column.GetCellContent(data)).Text;
                costPrice = ((TextBlock)ProductsDataGrid.SelectedCells[2].Column.GetCellContent(data)).Text;
                sellingPrice = ((TextBlock)ProductsDataGrid.SelectedCells[3].Column.GetCellContent(data)).Text;
                quantity = ((TextBlock)ProductsDataGrid.SelectedCells[4].Column.GetCellContent(data)).Text;
                barCode = ((TextBlock)ProductsDataGrid.SelectedCells[5].Column.GetCellContent(data)).Text;
            }
            //SQL to receive the auto int primary key from dbo.Stock
            string query = "SELECT ID FROM Stock WHERE BarCode = " + barCode;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            
            conn.Open();
            try //Try prevents error from user selecting an empty row
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ID = int.Parse(reader["ID"].ToString());
                    }
                }
            }
            catch (Exception ex) 
            {
                //Do nothing
            }
            finally 
            { 
                conn.Close(); 
            }

            //Setting text edits to the data from the selected cells in the data grid
            cmbCategory.Text = category; 
            txtName.Text = productName;            
            txtCost.Text = costPrice;
            txtSelling.Text = sellingPrice;
            txtQuantity.Text = quantity;
            txtBarcode.Text = barCode;
            
        }
    }
    public class Product 
    {
        public string ProductCategory { get; set; } 
        public string ProductName { get; set; }
        public double SellingPrice { get; set; }
        public string BarCode { get; set; }
        public double CostPrice { get; set; }
        public int Quantity { get; set; }
    }
}
 