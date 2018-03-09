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
    /// MyWindow07.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow07 : Window
    {
        // バインディングリスト
        List<CustomParameter> MyCustomParameters = new List<CustomParameter>();

        public MyWindow07()
        {
            InitializeComponent();

            SetBindParameter();
        }

        private void SetBindParameter()
        {
            MyCustomParameters.Add(new CustomParameter { Id = 0, Text = "チェック１", Value = "value1", IsChecked = false, });
            MyCustomParameters.Add(new CustomParameter { Id = 1, Text = "チェック２", Value = "value2", IsChecked = false, });
            MyCustomParameters.Add(new CustomParameter { Id = 2, Text = "チェック３", Value = "value3", IsChecked = false, });
            MyCustomParameters.Add(new CustomParameter { Id = 3, Text = "チェック４", Value = "value4", IsChecked = false, });
            MyCustomParameters.Add(new CustomParameter { Id = 4, Text = "チェック５", Value = "value5", IsChecked = false, });

            MyStackPanel02.DataContext = MyCustomParameters;
        }

        private void GetSelectedItems_Click(object sender, RoutedEventArgs e)
        {
            MyRichTextBox01.Document.Blocks.Clear();
            MyRichTextBox02.Document.Blocks.Clear();
            MyRichTextBox03.Document.Blocks.Clear();

            // All
            foreach (var myCustomParameter in MyCustomParameters)
            {
                MyRichTextBox01.AppendText(myCustomParameter.Value);
            }

            // チェックしたものだけをフィルタリング
            foreach (var myCustomParameter in MyCustomParameters.Where(n => n.IsChecked))
            {
                MyRichTextBox02.AppendText(myCustomParameter.Value);
            }

            // チェックかつ、Idが３以上でフィルタリング
            foreach (var myCustomParameter in MyCustomParameters
                                                .Where(n => n.IsChecked)
                                                .Where(n => n.Id > 3)
                                                )
            {
                MyRichTextBox03.AppendText(myCustomParameter.Value);
            }
        }
    }

    public class CustomParameter
    {
        /// <value>一意の識別子</value>
        public int Id { get; set; }
        /// <value>画面上の値（画面上は「Ａ（全角）」で、内部的には「A(半角)」といったケースに対応するため。）</value>
        public string Text { get; set; }
        /// <value>内部処理にて使用する値</value>
        public string Value { get; set; }
        /// <value>ON/OFF を表す値</value>
        public bool IsChecked { get; set; }
    }


}
