using System.Windows;

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
