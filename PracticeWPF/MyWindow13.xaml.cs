using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            AddMyEvent();
        }

        private void AddMyEvent()
        {
            buttonNumberType01.PreviewGotKeyboardFocus += (sender, e) => buttonNumberType_PreviewGotKeyboardFocus();
            buttonNumberType02.PreviewGotKeyboardFocus += (sender, e) => buttonNumberType_PreviewGotKeyboardFocus();
            buttonNumberType03.PreviewGotKeyboardFocus += (sender, e) => buttonNumberType_PreviewGotKeyboardFocus();
            buttonNumberType04.PreviewGotKeyboardFocus += (sender, e) => buttonNumberType_PreviewGotKeyboardFocus();
            buttonNumberType05.PreviewGotKeyboardFocus += (sender, e) => buttonNumberType_PreviewGotKeyboardFocus();

            buttonNumberType01.Click += (sender, e) => buttonNumberType_Click(sender);
            buttonNumberType02.Click += (sender, e) => buttonNumberType_Click(sender);
            buttonNumberType03.Click += (sender, e) => buttonNumberType_Click(sender);
            buttonNumberType04.Click += (sender, e) => buttonNumberType_Click(sender);
            buttonNumberType05.Click += (sender, e) => buttonNumberType_Click(sender);
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

        #region フォーカス位置に値をセット
        private TextBox forcusedInputObject = null;
        private int forcusedCaretIndex = 0;
        private void buttonNumberType_PreviewGotKeyboardFocus()
        {
            forcusedInputObject = FocusManager.GetFocusedElement(this) as TextBox;
        }

        private void buttonNumberType_Click(object sender)
        {
            try
            {
                if (forcusedInputObject == null) return;

                Button clickedButton = (Button)sender;

                Console.WriteLine("SelectionStart:" + forcusedInputObject.SelectionStart);
                Console.WriteLine("CaretIndex:" + forcusedInputObject.CaretIndex);

                //forcusedInputObject.Text += clickedButton.Tag; //末尾に追加

                forcusedCaretIndex = forcusedInputObject.CaretIndex;

                string leftCursorString  = forcusedInputObject.Text.Substring(0, forcusedCaretIndex);
                string rightCursorString = forcusedInputObject.Text.Substring(forcusedCaretIndex);

                forcusedInputObject.Text = leftCursorString + clickedButton.Tag + rightCursorString;

                //-----( フォーカスがボタンに移動しているので、元に戻す )-----
                FocusManager.SetFocusedElement(this, forcusedInputObject);
                //forcusedInputObject.Select(forcusedInputObject.Text.Length, 0); //末尾にカーソルを合わせる
                forcusedInputObject.Select(forcusedCaretIndex + 1, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
