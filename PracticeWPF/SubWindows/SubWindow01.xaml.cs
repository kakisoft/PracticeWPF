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
        public MyWindow01.SubConfigParameters subConfigParametersCluster = new MyWindow01.SubConfigParameters();

        #region バインディング設定
        private void SetBindConfig()
        {
            subConfigParametersPanel.DataContext = subConfigParametersCluster;
        }
        private void ClearBindParameters()
        {
            subConfigParametersCluster.StringParam1 = "";
            subConfigParametersCluster.StringParam2 = "";
            subConfigParametersCluster.StringParam3 = "";
            subConfigParametersCluster.DateTimeParam1 = DateTime.Today;
            subConfigParametersCluster.DateTimeParam2 = null;
        }
        #endregion

        #region 初期化
        public SubWindow01()
        {
            InitializeComponent();

            InitializeThisWindowsParameters();
        }

        public SubWindow01(MyWindow01.SubConfigParameters subConfigParametersCluster)
        {
            InitializeComponent();

            this.subConfigParametersCluster = subConfigParametersCluster;
            InitializeThisWindowsParameters();
        }

        private void InitializeThisWindowsParameters()
        {
            AddThisWindowsEvent();
            SetBindConfig();
        }

        private void AddThisWindowsEvent()
        {
            resetSubWindowsParametersButton.Click += (sender, e) => ClearBindParameters();
            //closeSubWindowsParametersButton.Click += (sender, e) => ClearBindParameters();
        }
        #endregion

    }
}
