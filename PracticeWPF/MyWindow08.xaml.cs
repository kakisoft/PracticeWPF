using System;
using System.Windows;

namespace PracticeWPF
{
    /// <summary>
    /// チェックボックスとのバインディング
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
