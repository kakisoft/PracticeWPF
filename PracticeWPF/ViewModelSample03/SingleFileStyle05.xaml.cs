using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PracticeWPF.ViewModelSample03
{
    /// <summary>
    /// SingleFileStyle05.xaml の相互作用ロジック
    /// </summary>
    public partial class SingleFileStyle05 : Window
    {
        private VMNotice05 vm;

        public SingleFileStyle05()
        {
            InitializeComponent();

            this.vm = new VMNotice05();
            this.vm.WindowView = this;
            this.DataContext = this.vm;
        }
    }


    #region ******************************【   Model   】******************************
    /// <summary>
    /// 
    /// </summary>
    public class Notice05
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
    public class VMNotice05 : ViewModelBase05
    {
        public Window WindowView { get; set; }

        #region SaveCommand
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return this._saveCommand = this._saveCommand ?? new RelayCommand(this.SaveEnteredContent);
            }
        }

        private void SaveEnteredContent()
        {
            //MessageBox.Show("保存");

            NoticeSet.Add(new Notice05 { Id = 1, Title = "t1" });
            NoticeSet.Add(new Notice05 { Id = 2, Title = "t2" });



            //_noticeSet02 = new ObservableCollection<Notice05>();
            //_noticeSet02.Add(new Notice05 { Id = 1, Title = "t1", SubTitle = "sub1", Contents = "c1" });
            //_noticeSet02.Add(new Notice05 { Id = 1, Title = "t1", SubTitle = "sub1", Contents = "c1" });

            //NoticeSet02 = new ObservableCollection<Notice05>();

            //_noticeSet02 = new ObservableCollection<Notice05>();

            //_noticeSet02.Add(new Notice05 { Id = 1, Title = "t1", SubTitle = "sub1", Contents = "c1" });


            //-------- NG -----------
            //NoticeSet02.Clear();
            //NoticeSet02.Add(new Notice05 { Id = 1, Title = "t1", SubTitle = "sub1", Contents = "c1" });
            //NoticeSet02.Add(new Notice05 { Id = 2, Title = "t2", SubTitle = "sub2", Contents = "c2" });
            //NoticeSet02.Add(new Notice05 { Id = 3, Title = "t3", SubTitle = "sub3", Contents = "c3" });
            //-----------------------


            _noticeSet02.Clear();
            _noticeSet02.Add(new Notice05 { Id = 1, Title = "t1", SubTitle = "sub1", Contents = "c1" });
            _noticeSet02.Add(new Notice05 { Id = 2, Title = "t2", SubTitle = "sub2", Contents = "c2" });
            _noticeSet02.Add(new Notice05 { Id = 3, Title = "t3", SubTitle = "sub3", Contents = "c3" });
            base.OnPropertyChanged(nameof(this._noticeSet02));
        }
        #endregion

        #region CancelCommand
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return this._cancelCommand = this._cancelCommand ?? new RelayCommand(this.CancelInputContent);
            }
        }

        private void CancelInputContent()
        {
            MessageBox.Show("キャンセル");
        }
        #endregion

        #region AddButtonElementCommand
        private RelayCommand _addButtonElementCommand;
        public RelayCommand AddButtonElementCommand
        {
            get
            {
                return this._addButtonElementCommand = this._addButtonElementCommand ?? new RelayCommand(this.AddButtonElement);
            }
        }

        private void AddButtonElement()
        {

        }
        #endregion

        #region MyString01
        private string _myString01 = "初期値";
        public string MyString01
        {
            get
            {
                return this._myString01;
            }
            set
            {
                if (this._myString01 == value)
                {
                    return;
                }
                this._myString01 = value;
                base.OnPropertyChanged(nameof(this.MyString01));
            }
        }


        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return this._clearCommand = this._clearCommand ?? new RelayCommand(this.ClearInputContent, CanExecuteClearInput);
            }
        }

        private void ClearInputContent()
        {
            //base.no

            //MyString01 = "";
            _myString01 = "";

            base.OnPropertyChanged(nameof(this.MyString01));
        }


        private bool CanExecuteClearInput()
        {
            return false;
        }
        #endregion

        #region CloseCommand
        private RelayCommand<Window> _closeCommand;
        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return this._closeCommand = this._closeCommand ?? new RelayCommand<Window>(this.CloseWindow);
            }
        }

        private void CloseWindow(Window parameter)
        {
            //WindowView.Close();
            ((Window)parameter).Close();
        }
        #endregion




        #region property - Observable
        public ObservableCollection<Notice05> NoticeSet { get; set; } = new ObservableCollection<Notice05>();




        //propfull  タブ２回

        //public ObservableCollection<Notice05> NoticeSet02 { get; set; } = new ObservableCollection<Notice05>();


        private ObservableCollection<Notice05> _noticeSet02 = new ObservableCollection<Notice05>();
        //private ObservableCollection<Notice05> _noticeSet02;
        //private ObservableCollection<Notice05> _noticeSet02 = null;

        public ObservableCollection<Notice05> NoticeSet02
        {
            get
            {
                return _noticeSet02;
            }
            set
            {
                //if (this._noticeSet02 == value)
                //{
                //    return;
                //}
                //this._noticeSet02 = value;
                ////base.updateErrors(value);
                //base.OnPropertyChanged(nameof(this._noticeSet02));


                _noticeSet02 = value;
            }
        }



        #endregion
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
    public class ViewModelBase05 : INotifyPropertyChanged
    {
        /// <summary>
        /// 任意の型の引数を1つ受け付けるRelayCommand
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public event EventHandler CanExecuteChanged;

            public RelayCommand(Action execute) : this(execute, null)
            {

            }

            public RelayCommand(Action execute, Func<bool> canExecute)
            {
                if (execute == null)
                {
                    throw new ArgumentNullException("canExecute is null - " + this.GetType());
                }
                this._execute = execute;
                this._canExecute = canExecute;

            }

            public bool CanExecute(object parameter)
            {
                //return _canExecute == null ? true : _canExecute((T)parameter);
                return true;
                //return false;
            }

            public void Execute(object parameter)
            {
                _execute();
            }

        }




        #region parameter *****************************************************************************************************
        /// <summary>
        /// 任意の型の引数を1つ受け付けるRelayCommand
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class RelayCommand<T> : ICommand
        {
            #region delegate **************************************************************************************************
            /// <summary>
            /// 値無メソッド用
            /// </summary>
            private readonly Action<T> _execute;
            /// <summary>
            /// 対象物活性/非活性用
            /// </summary>
            private readonly Func<T, bool> _canExecute;
            /// <summary>
            /// RaiseCanExecuteChanged が呼び出されたときに生成されます。
            /// </summary>
            public event EventHandler CanExecuteChanged;
            #endregion

            #region constructor ***********************************************************************************************
            /// <summary>
            /// 新しいコマンドを作成します。
            /// </summary>
            /// <param name="execute">実行ロジック。</param>
            /// <param name="canExecute">実行ステータス ロジック。</param>
            public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
            {
                // nullの場合、例外
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }
            /// <summary>
            /// 常に実行可能な新しいコマンドを作成します。
            /// </summary>
            /// <param name="execute">実行ロジック。</param>
            public RelayCommand(Action<T> execute)
                : this(execute, null)
            {
                // none
            }
            #endregion

            #region ICommand member *******************************************************************************************
            /// <summary>
            /// 現在の状態でこの <see cref="RelayCommand"/> が実行できるかどうかを判定します。
            /// </summary>
            /// <param name="parameter">
            /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
            /// </param>
            /// <returns>このコマンドが実行可能な場合は true、それ以外の場合は false。</returns>
            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }

            /// <summary>
            /// 現在のコマンド ターゲットに対して <see cref="RelayCommand"/> を実行します。
            /// </summary>
            /// <param name="parameter">
            /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
            /// </param>
            public void Execute(object parameter)
            {
                _execute((T)parameter);
            }
            #endregion

            #region procedure *************************************************************************************************
            /// <summary>
            /// <see cref="CanExecuteChanged"/> イベントを発生させるために使用されるメソッド
            /// <see cref="CanExecute"/> の戻り値を表すために
            /// メソッドが変更されました。
            /// </summary>
            public void RaiseCanExecuteChanged()
            {
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, EventArgs.Empty);
                }
            }

            #endregion

        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged == null) return;

            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
    #endregion ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■

}
