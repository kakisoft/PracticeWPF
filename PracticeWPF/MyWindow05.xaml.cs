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
    /// テキストボックスの双方向バインディング２
    /// </summary>
    public partial class MyWindow05 : Window
    {
        public MyWindow05()
        {
            InitializeComponent();

            HogeClass HogeObject = new HogeClass();
            HogeObject.Hoge = "Try it.";
            stackpanel.DataContext = HogeObject;
        }

        public class HogeClass
        {
            public string Hoge { get; set; }
        }
    }
}
