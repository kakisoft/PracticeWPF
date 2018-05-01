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
    /// ファイルエクスプローラ関連
    /// https://www.ipentec.com/document/csharp-shell-namespace-create-explorer-tree-view-control-and-linked-list-view
    /// https://www.doraxdora.com/blog/2018/02/14/post-3933/
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
