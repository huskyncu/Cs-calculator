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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace calculator
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        string nums = "";
        string decimalnum = "";
        string binarynum = "";
        public MainWindow()
        {
            InitializeComponent();
        }
      
        
        private void _0_Click(object sender, RoutedEventArgs e)
        {
            nums += "0";
            shownum.Text=nums;
        }
        private void _1_Click(object sender, RoutedEventArgs e)
        {
            nums += "1";
            shownum.Text = nums;
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            nums += "2";
            shownum.Text = nums;
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            nums += "3";
            shownum.Text = nums;
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            nums += "4";
            shownum.Text = nums;
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            nums += "5";
            shownum.Text = nums;
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            nums += "6";
            shownum.Text = nums;
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            nums += "7";
            shownum.Text = nums;
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            nums += "8";
            shownum.Text = nums;
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            nums += "9";
            shownum.Text = nums;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            string connString = "server=127.0.0.1;port=3306;user id=root;password=;database=test;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string sql = "SELECT * FROM calculator WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader data = cmd.ExecuteReader();
            int bol = 0;
            while (data.Read())
            {

                if (data["Dec_"].ToString() == decimalnum && data["Infix"].ToString() == nums)
                {
                    Window2 w2 = new Window2();
                    w2.Show();
                    bol = 1;
                }
            }
            conn.Close();
            if (bol == 0)
            {
                connString = "server=127.0.0.1;port=3306;user id=root;password=;database=test;charset=utf8;";
                MySqlConnection conn2 = new MySqlConnection(connString);
                conn2.Open();
                sql = $"INSERT INTO  calculator (`ID`, `Dec_`, `Bin`, `Infix`, `Postfix`, `Prefix`) VALUES (NULL, '{decimalnum}', '{binarynum}', '{shownum.Text}', '{text_postorder.Text}', '{text_preorder.Text}');";
                MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
                cmd2.ExecuteNonQuery();
                conn2.Close();
            }

        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            nums += "+";
            shownum.Text = nums;
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            nums += "-";
            shownum.Text = nums;
        }

        private void Times_Click(object sender, RoutedEventArgs e)
        {
            nums += "*";
            shownum.Text = nums;
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            nums += "/";
            shownum.Text = nums;
        }

        
        bool isOp(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
                return true;
            return false;
        }

        char compare(char opt, char si)
        {
            if ((opt == '+' || opt == '-') && (si == '*' || si == '/'))
                return '<';
            else if (opt == '#')
                return '<';
            return '>';
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            nums = "";
            shownum.Text = "";
            text_preorder.Text = "";
            text_postorder.Text= "";
            text_binary.Text = "";
            text_decimal.Text = "";
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            //postorder
            Stack<char> ops = new Stack<char>();
            Stack<char> lists = new Stack<char>();
            ops.Push('#');
            lists.Push('#');
            for (int i = 0; i < nums.Length; i++)
            {
                if (!isOp(nums[i]))
                    lists.Push(nums[i]);
                else
                {
                    char c = compare(ops.Peek(), nums[i]);
                    if (c == '<')
                        ops.Push(nums[i]);
                    else
                    {
                        lists.Push(ops.Peek());
                        ops.Pop();
                        ops.Push(nums[i]);
                    }
                }
            }
            while (ops.Peek() != '#')
            {
                lists.Push(ops.Peek());
                ops.Pop();
            }
            List<char> s1 = new List<char>();
            while (lists.Peek() != '#')
            {
                s1.Add(lists.Peek());
                lists.Pop();
            }
            for (int i = 0; i < s1.Count / 2; i++)
            {
                char temp = s1[i];
                s1[i] = s1[s1.Count - 1 - i];
                s1[s1.Count - 1 - i] = temp;
            }
            string postorderr = "";
            for (int i = 0; i < s1.Count; i++)
            {
                postorderr += s1[i];
            }
            text_postorder.Text = postorderr;
            //preorder
            ops.Clear();
            lists.Clear();
            ops.Push('#');
            lists.Push('#');
            for (int i = nums.Length-1; i >=0; i--)
            {
                if (!isOp(nums[i]))
                    lists.Push(nums[i]);
                else
                {
                    char c = compare(ops.Peek(), nums[i]);
                    if (c == '<')
                        ops.Push(nums[i]);
                    else
                    {
                        lists.Push(ops.Peek());
                        ops.Pop();
                        ops.Push(nums[i]);
                    }
                }
            }
            while (ops.Peek() != '#')
            {
                lists.Push(ops.Peek());
                ops.Pop();
            }
            s1.Clear();
            while (lists.Peek() != '#')
            {;
                s1.Add(lists.Peek());
                lists.Pop();
            }
            for (int i = 0; i < s1.Count / 2; i++)
            {
                char temp = s1[i];
                s1[i] = s1[s1.Count - 1 - i];
                s1[s1.Count - 1 - i] = temp;
            }
            string preorderr = "";
            for (int i = s1.Count-1; i >=0; i--)
            {
                preorderr += s1[i];
            }
            text_preorder.Text = preorderr;
            //calculate decimnal and binary
            List<char> tmp = new List<char>();
            List<int> arr = new List<int>();
            foreach (char c in nums)
            {
                if (c == '*' || c == '/' || c == '+' || c == '-')
                {
                    tmp.Add(c);
                }
                else
                {
                    arr.Add(c - '0');
                }
            }
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i] == '*')
                {
                    int a = (arr[i]) * (arr[i + 1]);
                    arr[i] = a;
                    arr[i + 1] = 0;
                    tmp[i] = '+';

                }
                if (tmp[i] == '/')
                {
                    int a = (arr[i]) / (arr[i + 1]);
                    arr[i] = a;
                    arr[i + 1] = 0;
                    tmp[i] = '+';
                }
            }
            int dec_num = arr[0];
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i] == '+')
                {
                    dec_num += (arr[i + 1]);
                }
                if (tmp[i] == '-')
                {
                    dec_num -= (arr[i + 1]);
                }
            }
            int m = dec_num;
            string rebin = "";
            binarynum = "";
            while (true)
            {
                int r = m % 2;
                m = m / 2;
                rebin += r.ToString();
                if (m == 0)
                {
                    break;
                }
            }
            for (int i = rebin.Length - 1; i >= 0; i--)
            {
                binarynum += rebin[i];
            }
            decimalnum = dec_num.ToString();
            text_decimal.Text = decimalnum;
            text_binary.Text = binarynum;
        }
    }
}
