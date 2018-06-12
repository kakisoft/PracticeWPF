using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PracticeWPF.ViewModelSample03
{

    #region ******************************【   View    】******************************
    /// <summary>
    /// MVVM SingleFileStyle02
    /// </summary>
    public partial class SingleFileStyle02 : Window
    {
        /// <summary>
        /// 対象ViewModel
        /// </summary>
        private VMNotice vm;


        public SingleFileStyle02()
        {
            InitializeComponent();


            //xaml内へ記述する事も可能
            this.vm = new VMNotice();
            this.DataContext = this.vm;


            TieleTextBox.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetClass"></param>
        public SingleFileStyle02(Notice targetClass) : this()
        {

        }
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
