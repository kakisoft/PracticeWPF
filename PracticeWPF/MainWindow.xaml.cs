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

namespace PracticeWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region サンプルクラス
        private User _user;
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool IsMarried { get; set; }
        }

        private void CreateSampleInstance()
        {
            _user = new User
            {
                Name = "Kaki",
                Age = 30,
                IsMarried = true,
            };
            //this.DataContext = _user;
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //バインディング
            this.DataContext = new { X = 10, Y = 20 };
        }

        private void MyWindow01button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myWindow01 = new MyWindow01();
                myWindow01.Show(); //呼び出し元はアクティブ状態
                //myWindow01.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyWindow02button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myWindow02 = new MyWindow02();
                //myWindow02.Show();
                myWindow02.ShowDialog(); //別ウィンドウが閉じるまで、呼び出し元は非アクティブ
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyWindow03button_Click(object sender, RoutedEventArgs e)
        {
            new MyWindow03().ShowDialog();
        }

        private void MyWindow04button_Click(object sender, RoutedEventArgs e) => new MyWindow04().ShowDialog();
        private void MyWindow05button_Click(object sender, RoutedEventArgs e) => new MyWindow05().ShowDialog();
        private void MyWindow06button_Click(object sender, RoutedEventArgs e) => new MyWindow06().ShowDialog();
        private void MyWindow07button_Click(object sender, RoutedEventArgs e) => new MyWindow07().ShowDialog();
        private void MyWindow08button_Click(object sender, RoutedEventArgs e) => new MyWindow08().ShowDialog();
        private void MyWindow09button_Click(object sender, RoutedEventArgs e) => new MyWindow09().ShowDialog();
        private void MyWindow10button_Click(object sender, RoutedEventArgs e) => new MyWindow10().ShowDialog();
        private void MyWindow11button_Click(object sender, RoutedEventArgs e) => new MyWindow11().ShowDialog();
        private void MyWindow12button_Click(object sender, RoutedEventArgs e) => new MyWindow12().ShowDialog();
    }

}
