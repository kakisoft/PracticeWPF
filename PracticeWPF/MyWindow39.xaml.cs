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
    /// MyWindow39.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow39 : Window
    {
        public MyWindow39()
        {
            InitializeComponent();


            this.DataContext = new CommandWindowViewModel02();
        }
    }





    // 外部から与えるデータ（＝ビューモデル）
    public class CommandWindowViewModel02
    {
        public ICommand OKCommand { get; private set; }
        public ICommand NGCommand { get; private set; }

        public CommandWindowViewModel02()
        {
            this.OKCommand = new RelayCommandOK();
            this.NGCommand = new RelayCommandNG();
        }





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

    }

}
