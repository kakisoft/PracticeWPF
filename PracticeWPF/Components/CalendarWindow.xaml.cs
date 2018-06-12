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

namespace PracticeWPF.Components
{
    /// <summary>
    /// CalendarWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CalendarWindow : Window
    {
        #region プロパティ
        public List<DateTime?> SpecifiedDates { get; set; } = new List<DateTime?>();
        #endregion

        #region 初期化
        public CalendarWindow()
        {
            InitializeComponent();

            InitializeThisWindowsParameter();
        }

        public CalendarWindow(List<DateTime?> specifiedDates) : this()
        {
            SpecifiedDates = specifiedDates ?? new List<DateTime?>();
        }


        private void InitializeThisWindowsParameter()
        {
            SetFormEventProcess();
        }
        private void SetFormEventProcess()
        {
            ThisCalendar.SelectedDatesChanged += (sender, e) => AddSelectedDates(sender);

            SelectButton.Click += (sender, e) => SelectDateContents();
            CloseButton.Click += (sender, e) => CloseThisWindow();
        }
        #endregion

        #region 選択した日付を内部データに追加
        private void AddSelectedDates(object sender)
        {
            try
            {
                SpecifiedDates.Add(((Calendar)sender).SelectedDate);
                CloseThisWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region [選択]ボタン
        private void SelectDateContents()
        {

        }
        #endregion

        #region [閉じる]ボタン
        private void CloseThisWindow()
        {
            Close();
        }
        #endregion

    }
}
