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
    /// MyWindow32.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow32 : Window
    {
        #region 初期化
        public MyWindow32()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
            AddMyEvent();
        }

        private void InitializeThisWindowsParameters()
        {

        }

        private void AddMyEvent()
        {
            //myButton01.Click += (sender, e) => MyButton01_Click();
            //myButton02.Click += (sender, e) => MyButton02_Click();
        }
        #endregion

        #region －１－
        private void MyButton01_Click()
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", @"C:\My Documents\My Pictures");
        }
        #endregion
    }
}
