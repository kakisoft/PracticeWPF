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
using System.Windows;

namespace PracticeWPF
{
    #region 定義情報：ComboBoxにEnumをバインド
    //クラスの外部に記述。
    public enum ColorEnum
    {
        White = 0,
        Blue = 1,
        Red = 2,
        Black = 3
    }

    //xaml側に、「xmlns:sys～」の追加が必要。
    #endregion

    /// <summary>
    /// Enum の色々な使い方
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

        #region 定義情報２
        enum Signal : int
        {
            [Display(Name = "赤")]
            Red = 0,

            [Display(Name = "黄")]
            Yellow = 1,

            [Display(Name = "青")]
            Blue = 2
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
            myButton02.Click += (sender, e) => MyButton02_Click();
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

        #region enum を foreachで回す
        private void MyButton02_Click()
        {
            foreach (int r in Enum.GetValues(typeof(Signal)))
            {
                Console.WriteLine(r);
            }
        }
        #endregion

        #region ComboBoxにEnumをバインド
        private void bttn_SelectRed_Click(object sender, RoutedEventArgs e)
        {
            comb_color.SelectedItem = ColorEnum.Red;
        }

        private void bttn_ShowSelectedColor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(comb_color.SelectedItem.ToString());
        }
        #endregion

    }


}
