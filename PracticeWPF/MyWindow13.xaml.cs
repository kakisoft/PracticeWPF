using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// タッチパネル風の数量入力
    /// </summary>
    public partial class MyWindow13 : Window
    {
        #region バンディングリスト
        //「確保枚数テキスト」
        TextTypeControl numberOfTicketsCluster = new TextTypeControl();
        #endregion

        #region 画面制御用クラス
        /// <summary>
        /// テキスト制御用
        /// </summary>
        private class TextTypeControl : BindableBase
        {
            private string dispText;
            public string DispText
            {
                get { return this.dispText; }
                set { this.SetProperty(ref this.dispText, value); }
            }
        }
        #endregion

        #region バインド設定
        /// <summary>
        /// バインド設定
        /// </summary>
        private void SetBindConfig()
        {
            numberOfTickets.DataContext = numberOfTicketsCluster;
        }
        #endregion

        #region 初期処理
        /// <summary>
        /// 初期処理
        /// </summary>
        public MyWindow13()
        {
            InitializeComponent();
            InitializeThisWindowsParameter();
        }

        /// <summary>
        /// 画面固有の内容の初期設定
        /// </summary>
        private void InitializeThisWindowsParameter()
        {
            SetBindConfig();
            SetBindParameter();
        }
        #endregion

        #region バインドされたパラメータをセット
        /// <summary>
        /// バインドされたパラメータをセット
        /// </summary>
        private void SetBindParameter()
        {
            numberOfTicketsCluster.DispText = "0";
        }
        #endregion

        #region 数量制御
        //「＋」
        private void buttonAddTheNumberOfTickets_Click(object sender, RoutedEventArgs e) => AddTheNumberOfTickets(1);
        //「－」
        private void buttonSubtractTheNumberOfTickets_Click(object sender, RoutedEventArgs e) => AddTheNumberOfTickets(-1);
        //インクリメント or デクリメント
        private void AddTheNumberOfTickets(int addNumber)
        {
            int ticketOfNumber;
            if (int.TryParse(numberOfTicketsCluster.DispText, out ticketOfNumber) == false)
            {
                numberOfTicketsCluster.DispText = "0";
                return;
            }

            ticketOfNumber += addNumber;
            if (ticketOfNumber < 0)
            {
                ticketOfNumber = 0;
            }

            numberOfTicketsCluster.DispText = ticketOfNumber.ToString();
        }

        //数量ボタン
        private void buttonSetTheNumberOfTickets_Click(object sender, RoutedEventArgs e) => SetTheNumberOfTickets(((Button)sender).Content.ToString());
        //数量セット
        private void SetTheNumberOfTickets(string numberOfTicket)
        {
            numberOfTicketsCluster.DispText = numberOfTicket;
        }
        #endregion


        #region 数値入力のみに制限
        private void textBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 0-9のみ
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }
        private void textBoxPrice_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
        #endregion


        //*******************************************//
        #region バンディング用ベースクラス
        protected class BindableBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
            {
                if (Equals(field, value)) { return false; }
                field = value;
                var h = this.PropertyChanged;
                if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
                return true;
            }
        }
        #endregion

    }
}
