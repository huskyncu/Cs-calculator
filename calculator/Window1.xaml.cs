using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using System.Data;
namespace calculator
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=test;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string sql = "SELECT * FROM calculator WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            string txt = "";
            MySqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                txt = "id:" + data["ID"].ToString() + " dec:" + data["Dec_"].ToString() + " bin:" + data["Bin"].ToString() +
                " infix:" + data["Infix"].ToString() + " postfix:" + data["Postfix"].ToString() + " prefix:" + data["Prefix"].ToString();
                datasbox.Text += txt;
                datasbox.Text += "\n";
            }
            conn.Close();
        }

        private void delb_Click(object sender, RoutedEventArgs e)
        {
            string delid = delbox.Text;
            string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=test;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string sql = $"DELETE FROM calculator WHERE id='{delid}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            delbox.Text = "";
            connString = "server=127.0.0.1;port=3306;user id=root;password=;database=test;charset=utf8;";
            MySqlConnection conn2 = new MySqlConnection(connString);
            conn2.Open();
            sql = "SELECT * FROM calculator WHERE 1";
            MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
            string txt = "";
            datasbox.Text = "";
            MySqlDataReader data = cmd2.ExecuteReader();
            while (data.Read())
            {
                txt = "id:" + data["ID"].ToString() + " dec:" + data["Dec_"].ToString() + " bin:" + data["Bin"].ToString() +
                " infix:" + data["Infix"].ToString() + " postfix:" + data["Postfix"].ToString() + " prefix:" + data["Prefix"].ToString();
                datasbox.Text += txt;
                datasbox.Text += "\n";
            }
            conn2.Close();
        }

        private void delbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
