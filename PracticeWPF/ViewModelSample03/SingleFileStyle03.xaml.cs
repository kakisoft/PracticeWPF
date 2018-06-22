using System;
using System.Collections.Generic;
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
    #region ******************************【   View    】******************************
    /// <summary>
    /// SingleFileStyle03.xaml の相互作用ロジック
    /// </summary>
    public partial class SingleFileStyle03 : Window
    {
        private VMNotice03 vm;

        public SingleFileStyle03()
        {
            InitializeComponent();

            this.vm = new VMNotice03();
            this.vm.WindowView = this;
            this.DataContext = this.vm;
        }
    }
    #endregion



    #region ******************************【   Model   】******************************
    /// <summary>
    /// 
    /// </summary>
    public class Notice03
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
    public class VMNotice03 : ViewModelBase03
    {
        public Window WindowView { get; set; }


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








        /// <summary>
        /// 
        /// </summary>
        private string _myVmString01 = "初期値";
        public string MyVmString01
        {
            get
            {
                return this._myVmString01;
            }
            set
            {
                if (this._myVmString01 == value)
                {
                    return;
                }
                this._myVmString01 = value;
                base.OnPropertyChanged(nameof(this.MyVmString01));


                Console.WriteLine("=================================");
                Console.WriteLine((nameof(this.MyVmString01)));    //MyVmString01
                Console.WriteLine((nameof(this._myVmString01)));   //_myVmString01
                Console.WriteLine("=================================");
            }
        }


        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return this._clearCommand = this._clearCommand ?? new RelayCommand(this.ClearInputContent);
            }
        }

        private void ClearInputContent()
        {
            //この書き方は良くない。setメソッドにて notifyPropertyChanged を実行しているため、値の変更がはいるたびに getが走る。
            //（1000行のループがあれば、2000回の処理が走る）
            //　「privateの変数に対して処理をし、最後に notifyPropertyChanged を走らせる」という記述にすると、負荷が少なくなる。
            MyVmString01 = "";



            for (int i = 0; i < 10; i++)
            {
                _myVmString01 += "x";
            }
            //MyVmString01 += "x";
            base.OnPropertyChanged(nameof(this.MyVmString01));
        }
















        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return this._closeCommand = this._closeCommand ?? new RelayCommand(this.CloseWindow);
            }
        }

        private void CloseWindow()
        {
            WindowView.Close();
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
    public class ViewModelBase03 : INotifyPropertyChanged
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
            }

            public void Execute(object parameter)
            {
                _execute();
            }

        }





        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged == null) return;

            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
    #endregion ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■

}
