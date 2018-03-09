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
    }

}
