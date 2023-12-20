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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Логика взаимодействия для BDGrid.xaml
    /// </summary>
    public partial class BDGrid : Page
    {
        public BDGrid()
        {
            InitializeComponent();
        }

        private void edit_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditUsers();
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddUser();
        }

        private void Refresh_click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=USER\\BATOSHA; Initial Catalog=mvvm; Integrated Security=True");
            connection.Open();
            string cmd = "SELECT Id, Username, Password, Name, AccessLvl FROM users";
            SqlDataAdapter dataAdp = new SqlDataAdapter(cmd, connection);
            DataTable dt = new DataTable("users");
            dataAdp.Fill(dt);

            // Очистите содержимое DataGridView
            DG.ItemsSource = null;

            DG.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        public void DeleteUsername(int Id)
        {
            SqlConnection connection = new SqlConnection("Data Source=USER\\BATOSHA;Initial Catalog=mvvm;Integrated Security=True");
            string cmd = "DELETE FROM [users] WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(cmd, connection);
            deleteCommand.Parameters.AddWithValue("@Id", Id);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem != null && DG.SelectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)DG.SelectedItem;
                int Id = Convert.ToInt32(row["Id"]);
                DeleteUsername(Id);
            }
        }
        
    }
}
