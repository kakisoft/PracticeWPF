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

namespace PracticeWPF
{
    /// <summary>
    /// MyWindow08.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow08 : Window
    {
        private User _user1;
        //private User _user2;
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool Ismarried { get; set; }
        }

        public MyWindow08()
        {
            InitializeComponent();

            SetBindParameter();
        }

        private void SetBindParameter()
        {
            _user1 = new User { Name = "kaki", Age = 35, Ismarried = true };

            MyStackPanel01.DataContext = _user1;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("myCheckBox01:" + _user1.Ismarried);
            Console.WriteLine("myText01:" + _user1.Name);
        }

        //双方向バインディングのチェック
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            _user1.Name = "no name";
            _user1.Ismarried = false;
        }
    }
}
