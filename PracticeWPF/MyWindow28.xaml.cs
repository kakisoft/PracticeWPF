using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
using PracticeWPF.Common;

namespace PracticeWPF
{
    /// <summary>
    /// MyWindow28.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow28 : Window
    {
        #region 定義情報１
        public enum Season
        {
            [Display(Name = "It's autumn")]
            Autumn,

            [Display(Name = "It's winter")]
            Winter,

            [Display(Name = "It's spring")]
            Spring,

            [Display(Name = "It's summer")]
            Summer
        }
        #endregion

        #region 初期化
        public MyWindow28()
        {
            InitializeComponent();
            InitializeThisWindowsConfig();
        }
        private void InitializeThisWindowsConfig()
        {
            SetInitParameters();

            AddMyEvent();
        }

        private void SetInitParameters()
        {
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
        }
        #endregion

        #region Display Name Attribute
        private void MyButton01_Click()
        {
            Season Season = Season.Summer;

            var seasonDisplayName = Season.GetAttribute<DisplayAttribute>();
            Console.WriteLine("Which season is it?");
            Console.WriteLine(seasonDisplayName.Name);
        }
        #endregion
    }
}
