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
    /// ICommand －１－
    /// 
    /// 【参考サイト】
    /// http://www.atmarkit.co.jp/ait/articles/1011/09/news102.html
    /// </summary>
    public partial class MyWindow38 : Window
    {
        public MyWindow38()
        {
            InitializeComponent();
            
            this.DataContext = new CommandWindowViewModel01();
        }
    }









    // 外部から与えるデータ（＝ビューモデル）
    public class CommandWindowViewModel01
    {
        class RelayCommandOK : ICommand
        {
            //CanExecuteメソッド： コマンドが実行可能な状態にあるかどうかを判定する。
            public bool CanExecute(object parameter) { return true; }

            //CanExecuteChangedイベント： INotifyPropertyChangedインターフェイスのPropertyChangedイベントと同様、コマンド実行の可否が変化したことを通知するためのイベント。
            public event EventHandler CanExecuteChanged;

            //Executeメソッド： コマンドを実行する。
            public void Execute(object parameter)
            {
                MessageBox.Show("OK！");
            }
        }


        class RelayCommandNG : ICommand
        {
            //CanExecuteメソッド： コマンドが実行可能な状態にあるかどうかを判定する。
            public bool CanExecute(object parameter) { return true; }

            //CanExecuteChangedイベント： INotifyPropertyChangedインターフェイスのPropertyChangedイベントと同様、コマンド実行の可否が変化したことを通知するためのイベント。
            public event EventHandler CanExecuteChanged;

            //Executeメソッド： コマンドを実行する。
            public void Execute(object parameter)
            {
                MessageBox.Show("NG！");
            }
        }


        public ICommand OKCommand { get; private set; }
        public ICommand NGCommand { get; private set; }

        public CommandWindowViewModel01()
        {
            this.OKCommand = new RelayCommandOK();
            this.NGCommand = new RelayCommandNG();
        }
    }
}
