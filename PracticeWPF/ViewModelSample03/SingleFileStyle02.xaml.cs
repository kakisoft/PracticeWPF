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
            this.vm = new VMNotice(new Notice());
            this.DataContext = this.vm;


            TieleTextBox.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetClass"></param>
        public SingleFileStyle02(Notice targetClass) : this()
        {
            // 更新時
            if (targetClass.Id != 0)
            {
                this.vm.Id       = targetClass.Id;
                this.vm.Title    = targetClass.Title;
                this.vm.SubTitle = targetClass.SubTitle;

                this.vm.PostingStartDate   = targetClass.PostingStartDate;
                this.vm.PostingStartHour   = targetClass.PostingStartDate == null ? "00" : targetClass.PostingStartDate.Value.ToString("HH");
                this.vm.PostingStartMinute = targetClass.PostingStartDate == null ? "00" : targetClass.PostingStartDate.Value.ToString("mm");

                this.vm.PostingEndDate   = targetClass.PostingEndDate;
                this.vm.PostingEndHour   = targetClass.PostingEndDate == null ? "00" : targetClass.PostingEndDate.Value.ToString("HH");
                this.vm.PostingEndMinute = targetClass.PostingEndDate == null ? "00" : targetClass.PostingEndDate.Value.ToString("mm");

                this.vm.Contents              = targetClass.Contents;
                this.vm.NumberOfNotifications = targetClass.NumberOfNotifications;
                this.vm.IsDeleted             = targetClass.IsDeleted;
            }
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
    public class VMNotice : ViewModelBase<Notice>
    {
        #region 定義情報

        private const int DEFAULT_PARAM01 = 0;

        private static class NOTIFICATION_CONTENT
        {
            public const string INFO = @"お知らせ情報";
            public const string REGISTRATION_ERROR = @"失敗";
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>システム担当者情報の保存に失敗しました。</remarks>
            public const string ERROR_MESSAGE01 = @"{0}の{1}に{2}しました。";
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>システム担当者情報を保存しました。</remarks>
            public const string ERROR_MESSAGE02 = @"{0}を{1}しました。";
        }
        #endregion

        #region property
        /// <summary>
        /// コード
        /// </summary>
        public long Id
        {
            get
            {
                return base.ModelClass.Id;
            }
            set
            {
                if (base.ModelClass.Id == value)
                {
                    return;
                }
                base.ModelClass.Id = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// タイトル
        /// </summary>
        [Required(ErrorMessage = "値を入力してください。")]
        public string Title
        {
            get
            {
                return base.ModelClass.Title;
            }
            set
            {
                if (base.ModelClass.Title == value)
                {
                    return;
                }
                base.ModelClass.Title = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// サブタイトル
        /// </summary>
        [Required(ErrorMessage = "サブタイトルを入力してください。")]
        public string SubTitle
        {
            get
            {
                return base.ModelClass.SubTitle;
            }
            set
            {
                if (base.ModelClass.SubTitle == value)
                {
                    return;
                }
                base.ModelClass.SubTitle = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(SubTitle));
            }
        }

        /// <summary>
        /// 適用期間
        /// </summary>
        public DateTime? PostingStartDate
        {
            get
            {
                return base.ModelClass.PostingStartDate;
            }
            set
            {
                if (base.ModelClass.PostingStartDate == value)
                {
                    return;
                }
                base.ModelClass.PostingStartDate = value;
                this.SaveCommand.RaiseCanExecuteChanged();
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingStartDate));
            }
        }

        /// <summary>
        /// 掲載時開始時
        /// </summary>
        private string _postingStartHour = "00";
        /// <summary>
        /// 掲載時開始時
        /// </summary>
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2桁入力です。")]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3])$", ErrorMessage = "入力値に誤りがあります。")]
        public string PostingStartHour
        {
            get
            {
                return this._postingStartHour;
            }
            set
            {
                if (this._postingStartHour == value)
                {
                    return;
                }
                this._postingStartHour = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingStartHour));
            }
        }

        /// <summary>
        /// 掲載時開始分
        /// </summary>
        private string _postingStartMinute = "00";
        /// <summary>
        /// 掲載時開始分
        /// </summary>
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2桁入力です。")]
        [RegularExpression(@"^([0-5][0-9])$", ErrorMessage = "入力値に誤りがあります。")]
        public string PostingStartMinute
        {
            get
            {
                return this._postingStartMinute;
            }
            set
            {
                if (this._postingStartMinute == value)
                {
                    return;
                }
                this._postingStartMinute = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingStartMinute));
            }
        }

        /// <summary>
        /// 掲載時 終了年月日
        /// </summary>
        public DateTime? PostingEndDate
        {
            get
            {
                return base.ModelClass.PostingEndDate;
            }
            set
            {
                if (base.ModelClass.PostingEndDate == value)
                {
                    return;
                }
                base.ModelClass.PostingEndDate = value;
                this.SaveCommand.RaiseCanExecuteChanged();
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingEndDate));
            }
        }

        /// <summary>
        /// 掲載時開始時
        /// </summary>
        private string _postingEndHour = "00";
        /// <summary>
        /// 掲載時開始時
        /// </summary>
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2桁入力です。")]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3])$", ErrorMessage = "入力値に誤りがあります。")]
        public string PostingEndHour
        {
            get
            {
                return this._postingEndHour;
            }
            set
            {
                if (this._postingEndHour == value)
                {
                    return;
                }
                this._postingEndHour = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingEndHour));
            }
        }

        /// <summary>
        /// 掲載時開始分
        /// </summary>
        private string _postingEndMinute = "00";
        /// <summary>
        /// 掲載時開始分
        /// </summary>
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2桁入力です。")]
        [RegularExpression(@"^([0-5][0-9])$", ErrorMessage = "入力値に誤りがあります。")]
        public string PostingEndMinute
        {
            get
            {
                return this._postingEndMinute;
            }
            set
            {
                if (this._postingEndMinute == value)
                {
                    return;
                }
                this._postingEndMinute = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(PostingEndMinute));
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "値を入力してください。")]
        public string Contents
        {
            get
            {
                return base.ModelClass.Contents;
            }
            set
            {
                if (base.ModelClass.Contents == value)
                {
                    return;
                }
                base.ModelClass.Contents = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(Contents));
            }
        }

        /// <summary>
        /// 回数
        /// </summary>
        public int? NumberOfNotifications
        {
            get
            {
                return base.ModelClass.NumberOfNotifications;
            }
            set
            {
                if (base.ModelClass.NumberOfNotifications == value)
                {
                    return;
                }
                base.ModelClass.NumberOfNotifications = value;
                this.SaveCommand.RaiseCanExecuteChanged();
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(NumberOfNotifications));
            }
        }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return base.ModelClass.IsDeleted;
            }
            set
            {
                if (base.ModelClass.IsDeleted == value)
                {
                    return;
                }
                base.ModelClass.IsDeleted = value;
                base.updateErrors(value);
                base.notifyPropertyChanged(nameof(IsDeleted));
            }
        }

        #endregion property

        #region command property
        /// <summary>
        /// 保存
        /// </summary>
        private RelayCommand<Window> _saveCommand;
        /// <summary>
        /// 保存
        /// </summary>
        public RelayCommand<Window> SaveCommand
        {
            get
            {
                return this._saveCommand = this._saveCommand ?? new RelayCommand<Window>(this.SaveEnteredContent, this.isEnabledSaveCmd);
            }
        }

        #endregion command property

        #region constructor
        /// <summary>
        /// 初期化時処理
        /// </summary>
        /// <param name="val_clsModel"></param>
        public VMNotice(Notice val_clsModel) : base(val_clsModel)
        {
            this.initialization();
            this.initialized();
        }
        /// <summary>
        /// 主に初期化
        /// </summary>
        private void initialization()
        {
            // none
        }
        /// <summary>
        /// 初期値
        /// </summary>
        private void initialized()
        {
            this.Id = DEFAULT_PARAM01;
            this.Title = string.Empty;
            this.SubTitle = string.Empty;
            this.Contents = string.Empty;
            this.IsDeleted = false;

        }
        #endregion constructor

        #region command method
        /// <summary>
        /// 可否判定
        /// </summary>
        /// <param name="parameter">View引数</param>
        /// <returns>
        /// <para>true = 可</para>
        /// <para>false = 否</para>
        /// </returns>
        private bool isEnabledSaveCmd(Window parameter)
        {
            bool result = true;

            // 回数が０以下かつ掲載時日時が未入力の場合
            if (this.ModelClass.NumberOfNotifications <= 0 && (this.ModelClass.PostingStartDate == null || this.ModelClass.PostingEndDate == null))
            {
                result = !result;
            }

            return result;
        }
        /// <summary>
        /// コマンド.保存
        /// </summary>
        /// <param name="parameter">View引数</param>
        private void SaveEnteredContent(Window parameter)
        {
            try
            {
                // UNDONE:IOチェック
                if (base.HasErrors)
                {
                    string str = string.Empty;
                    foreach (KeyValuePair<string, List<string>> item in base._dicErr.Where(x => x.Value != null))
                    {
                        str += "key:" + item.Key;
                        foreach (string value in item.Value)
                        {
                            str += "\n";
                            str += "value:" + value;
                        }
                        str += "\n";
                    }
                    MessageBox.Show(str);

                    return;
                }

                // 掲載時開始及び終了日時を作成
                if (base.ModelClass.PostingStartDate != null)
                {
                    DateTime dt = base.ModelClass.PostingStartDate.Value;
                    dt = new DateTime(dt.Year, dt.Month, dt.Day, Convert.ToInt32(this._postingStartHour), Convert.ToInt32(this._postingStartMinute), DEFAULT_PARAM01);
                    base.ModelClass.PostingStartDate = dt;
                }

                if (base.ModelClass.PostingEndDate != null)
                {
                    DateTime dt = base.ModelClass.PostingEndDate.Value;
                    dt = new DateTime(dt.Year, dt.Month, dt.Day, Convert.ToInt32(this._postingEndHour), Convert.ToInt32(this._postingEndMinute), DEFAULT_PARAM01);
                    base.ModelClass.PostingEndDate = dt;
                }

                MessageBox.Show(string.Format(NOTIFICATION_CONTENT.ERROR_MESSAGE01, NOTIFICATION_CONTENT.INFO, "保存", NOTIFICATION_CONTENT.REGISTRATION_ERROR));
                MessageBox.Show(string.Format(NOTIFICATION_CONTENT.ERROR_MESSAGE02, NOTIFICATION_CONTENT.INFO, "保存"));


                ((Window)parameter).Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
        #endregion command method

    }
    #endregion


    #region ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="cls">対象モデル as class</typeparam>
    /// <remarks>
    /// <para>https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters</para>
    /// </remarks>
    public class ViewModelBase<cls> : DynamicObject, INotifyDataErrorInfo, INotifyPropertyChanged
        where cls : class
    {
        #region property
        /// <summary>
        /// 対象クラス内部用
        /// </summary>
        private cls _modelClass;
        /// <summary>
        /// 対象クラス継承用
        /// </summary>
        protected cls ModelClass
        {
            get
            {
                return this._modelClass;
            }
        }
        #endregion property

        #region constructor
        /// <summary>
        /// 初期化時
        /// </summary>
        /// <param name="val_clsModel">対象モデル</param>
        public ViewModelBase(cls val_clsModel)
        {
            this._modelClass = val_clsModel;
        }
        #endregion constructor

        #region DynamicObject member
        /// <summary>
        /// プロパティ取得
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="targetObject"></param>
        /// <returns>
        /// <para>true = 正</para>
        /// <para>false = 否</para>
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object targetObject)
        {
            bool result = false;
            string propertyName = binder.Name;
            System.Reflection.PropertyInfo property = this._modelClass.GetType().GetProperty(propertyName);

            if (property == null || !property.CanRead)
            {
                // プロパティが存在しないか読み取りが出来ないので値の取得に失敗
                targetObject = null;
                return result;
            }

            // プロパティから値を取得する
            targetObject = property.GetValue(this._modelClass);

            return !result;

        }
        /// <summary>
        /// プロパティ設定
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="val_obj"></param>
        /// <returns>
        /// <para>true = 正</para>
        /// <para>false = 否</para>
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object val_obj)
        {
            bool result = false;
            string propertyName = binder.Name;
            System.Reflection.PropertyInfo property = this._modelClass.GetType().GetProperty(propertyName);
            if (property == null || !property.CanWrite)
            {
                return result;
            }

            // プロパティの値をセットしてイベントを発行する
            property.SetValue(this._modelClass, val_obj);
            this.notifyPropertyChanged(propertyName);

            return !result;

        }
        #endregion  DynamicObject member

        #region INotifyPropertyChanged member
        /// <summary>
        /// プロパティ値が変更するときに発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// プロパティ値が変更するときに発生します。
        /// </summary>
        /// <param name="info"></param>
        protected void notifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion INotifyPropertyChanged member

        #region INotifyDataErrorInfo member
        /// <summary>
        /// エラーリスト内部用
        /// </summary>
        private Dictionary<string, List<string>> dicErr = new Dictionary<string, List<string>>();
        /// <summary>
        /// エラーリスト外部用
        /// </summary>
        public Dictionary<string, List<string>> _dicErr
        {
            get
            {
                return this.dicErr;
            }
            set
            {
                if (value == this.dicErr)
                {
                    return;
                }
                this.dicErr = value;
                //うまくいかねぇ
                this.notifyPropertyChanged(nameof(this._dicErr));
            }
        }

        /// <summary>
        /// エラー情報が変更された際に発生するイベント
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// ErrorsChangedイベントを発生させる
        /// </summary>
        /// <param name="propertyName"></param>
        protected void notifyErrorsChanged([CallerMemberName]string propertyName = "")
        {
            EventHandler<DataErrorsChangedEventArgs> err = this.ErrorsChanged;
            if (err != null)
            {
                err(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// エンティティに検証エラーがあるかどうかを示す値を取得します。
        /// </summary>
        /// <remarks>
        /// <para>true = 有</para>
        /// <para>false = 無</para>
        /// </remarks>
        public bool HasErrors
        {
            get
            {
                return this.dicErr.Values.Any(x => x != null);
            }
        }
        /// <summary>
        /// 指定したプロパティまたはエンティティ全体の検証エラーを取得します。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable result = null;

            if (string.IsNullOrWhiteSpace(propertyName) || !this.dicErr.ContainsKey(propertyName))
            {
                return result;
            }

            result = this.dicErr[propertyName];

            return result;

        }
        /// <summary>
        /// エラー情報を更新する。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void updateErrors(object value, [CallerMemberName]string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            List<System.ComponentModel.DataAnnotations.ValidationResult> results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, results))
            {
                // エラー無し
                this.dicErr[propertyName] = null;
            }
            else
            {
                this.dicErr[propertyName] = results.Select(x => x.ErrorMessage).ToList();
            }

            this.notifyErrorsChanged(propertyName);
        }

        #endregion INotifyDataErrorInfo member

        #region ICommand

        #region none parameter
        /// <summary>
        /// その機能を中継することのみを目的とするコマンド
        /// デリゲートを呼び出すことにより、他のオブジェクトに対して呼び出します。
        ///CanExecute メソッドの既定の戻り値は 'true' です。
        /// <see cref="RaiseCanExecuteChanged"/> は、次の場合は必ず呼び出す必要があります。
        /// <see cref="CanExecute"/> は、別の値を返すことが予期されます。
        /// </summary>
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            /// <summary>
            /// RaiseCanExecuteChanged が呼び出されたときに生成されます。
            /// </summary>
            public event EventHandler CanExecuteChanged;

            #region constructor
            /// <summary>
            /// 新しいコマンドを作成します。
            /// </summary>
            /// <param name="execute">実行ロジック。</param>
            /// <param name="canExecute">実行ステータス ロジック。</param>
            public RelayCommand(Action execute, Func<bool> canExecute)
            {
                // nullの場合、例外
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }
            /// <summary>
            /// 常に実行可能な新しいコマンドを作成します。
            /// </summary>
            /// <param name="execute">実行ロジック。</param>
            public RelayCommand(Action execute)
                : this(execute, null)
            {
                // none
            }
            #endregion constructor

            /// <summary>
            /// 現在の状態でこの <see cref="RelayCommand"/> が実行できるかどうかを判定します。
            /// </summary>
            /// <param name="parameter">
            /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
            /// </param>
            /// <returns>このコマンドが実行可能な場合は true、それ以外の場合は false。</returns>
            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute();
            }

            /// <summary>
            /// 現在のコマンド ターゲットに対して <see cref="RelayCommand"/> を実行します。
            /// </summary>
            /// <param name="parameter">
            /// コマンドによって使用されるデータ。コマンドが、データの引き渡しを必要としない場合、このオブジェクトを null に設定できます。
            /// </param>
            public void Execute(object parameter)
            {
                _execute();
            }

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
        }
        #endregion none parameter

        #region parameter
        /// <summary>
        /// 任意の型の引数を1つ受け付けるRelayCommand
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class RelayCommand<T> : ICommand
        {
            private readonly Action<T> _execute;
            private readonly Func<T, bool> _canExecute;

            /// <summary>
            /// RaiseCanExecuteChanged が呼び出されたときに生成されます。
            /// </summary>
            public event EventHandler CanExecuteChanged;

            #region constructor
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
            #endregion constructor

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

        }
        #endregion parameter

        #endregion ICommand

    }
    #endregion ■■■■■■■■■■■■■■■■■■■■【 ViewModelBase 】■■■■■■■■■■■■■■■■■■■■

}
