using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace WPF_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string sql;
        NpgsqlCommand command;
        private NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Port=5432;DataBase=test;Username=postgres;Password=1234");

        public MainWindow()
        {
            InitializeComponent();
            con.Open();
            sql = "SELECT * FROM users";
            command = new NpgsqlCommand(sql, con);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
            DataGrid1.DataContext = dt;
            DataGrid1.ItemsSource = dt.AsDataView();
            con.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            sql = $"INSERT INTO users (username, password) VALUES ('{username.Text}', '{password.Text}')";
            command = new NpgsqlCommand(sql, con);
            command.ExecuteNonQuery();
            sql = "SELECT * FROM users";
            command = new NpgsqlCommand(sql, con);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));
            DataGrid1.DataContext = dt;
            DataGrid1.ItemsSource = dt.AsDataView();
            con.Close();
        }

    }
}
