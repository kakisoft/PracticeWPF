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
        #region ******************************【   View    】******************************
        /// <summary>
        /// 対象ViewModel
        /// </summary>
        private VMNotice vm;

        public MyWindow39()
        {
            InitializeComponent();

            this.vm = new VMNotice();
            this.DataContext = this.vm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetClass"></param>
        public MyWindow39(Notice targetClass) : this()
        {

        }
        #endregion






        #region ******************************【   Model   】******************************
        /// <summary>
        /// 
        /// </summary>
        public class Notice
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string SubTitle { get; set; }
            public DateTime? PostingStartDate { get; set; }
            public DateTime? PostingEndDate { get; set; }
            public string Contents { get; set; }
            public int? NumberOfNotifications { get; set; }
            public bool IsDeleted { get; set; }
        }
        #endregion







        #region ******************************【 ViewModel 】******************************
        public class VMNotice : ViewModelBase
        {
            private RelayCommand _saveCommand;
            public RelayCommand SaveCommand
            {
                get
                {
                    return this._saveCommand = this._saveCommand ?? new RelayCommand(this.SaveEnteredContent);
                }
            }

            private RelayCommand _cancelCommand;
            public RelayCommand CancelCommand
            {
                get
                {
                    return this._cancelCommand = this._cancelCommand ?? new RelayCommand(this.CancelInputContent);
                }
            }

            private void SaveEnteredContent()
            {
                MessageBox.Show("保存");
            }

            private void CancelInputContent()
            {
                MessageBox.Show("キャンセル");
            }
        }
        #endregion






        #region ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">対象モデル as class</typeparam>
        /// <remarks>
        /// <para>https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters</para>
        /// </remarks>
        public class ViewModelBase
        {
            /// <summary>
            /// 任意の型の引数を1つ受け付けるRelayCommand
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public class RelayCommand : ICommand
            {
                private readonly Action _execute;
                //private readonly Func<bool> _canExecute;

                public event EventHandler CanExecuteChanged;

                public RelayCommand(Action execute)
                {
                    // nullの場合、例外
                    _execute = execute ?? throw new ArgumentNullException(nameof(execute));

                    Console.WriteLine(nameof(execute));  // 常に「execute」。意味あるんかいな。
                }

                public bool CanExecute(object parameter)
                {
                    //return _canExecute == null ? true : _canExecute((T)parameter);
                    return true;
                }

                public void Execute(object parameter)
                {
                    _execute();
                }

            }
        }
        #endregion ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■
    }


}
