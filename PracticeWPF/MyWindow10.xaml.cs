using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// ボタン内での改行（ContextにTextBlockを格納）
    /// </summary>
    public partial class MyWindow10 : Window
    {
        public MyWindow10()
        {
            InitializeComponent();


            //==========< ボタン内で改行を入れる >==========
            //(コード側で設定)
            var b = new TextBlock();
            b.Text = "TexoBlockのTextWrappingを使うと改行が楽だよ。";
            b.TextWrapping = TextWrapping.Wrap;
            Button02.Content = b;
        }
    }
}
