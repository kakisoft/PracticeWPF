using PracticeWPF.ViewModelSample;
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
    /// RelayCommand
    /// 
    /// 【参考サイト】
    /// http://sourcechord.hatenablog.com/entry/2014/01/13/200039
    /// http://sourcechord.hatenablog.com/entry/20130303/1362315081
    /// http://sourcechord.hatenablog.com/entry/2014/01/13/200039
    /// </summary>
    public partial class MyWindow29 : Window
    {
        public MyWindow29()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModelSample01();
        }
    }
}
