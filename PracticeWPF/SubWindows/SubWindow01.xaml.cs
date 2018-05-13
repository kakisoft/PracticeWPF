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

namespace PracticeWPF.SubWindows
{
    /// <summary>
    /// SubWindow01.xaml の相互作用ロジック
    /// </summary>
    public partial class SubWindow01 : Window
    {
        #region 初期化
        public SubWindow01()
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
            //buttonCreate.Click += (sender, e) => buttonCreate_Click();
        }
        #endregion

    }
}
