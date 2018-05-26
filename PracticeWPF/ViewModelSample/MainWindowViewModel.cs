using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF.ViewModelSample
{
    class MainWindowViewModel : BindableBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { this.SetProperty(ref this.message, value); }
        }

        #region コマンドの実装
        private RelayCommand changeMessageCommand;
        public RelayCommand ChangeMessageCommand
        {
            get { return changeMessageCommand = changeMessageCommand ?? new RelayCommand(ChangeMessage); }
        }

        private void ChangeMessage()
        {
            this.Message = "コマンドを実行しました。";
        }
        #endregion


        public MainWindowViewModel()
        {
            this.Message = "初期値です。";
        }
    }
}
