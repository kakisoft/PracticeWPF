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
    /// MyWindow02.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow02 : Window
    {
        public MyWindow02()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lv1.Items.Add(new string[] { "1", "2", "3" });
            lv1.Items.Add(new string[] { "4", "5", "6" });
            //lv1.Add(new TestProperty { Title = "title", Score = "score", Date = "data" });
            //lv1.Items.Add(new TestProperty { Title = "title", Score = "score", Date = "data" });

            string[] ss = (string[])lv1.Items[1];
            MessageBox.Show(ss[0]);
        }

        public class TestProperty
        {
            public string Title { get; set; }
            public string Score { get; set; }
            public string Date { get; set; }
        }
    }
}
