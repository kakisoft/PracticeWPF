using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF.ViewModelSample
{
    class MainWindowViewModelSample01 : BindableBaseSample01
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { this.SetProperty(ref this.message, value); }
        }

        #region コマンドの実装
        private RelayCommandSanple01 changeMessageCommand;
        public RelayCommandSanple01 ChangeMessageCommand
        {
            get { return changeMessageCommand = changeMessageCommand ?? new RelayCommandSanple01(ChangeMessage); }
        }

        private void ChangeMessage()
        {
            this.Message = "コマンドを実行しました。";
        }
        #endregion


        public MainWindowViewModelSample01()
        {
            this.Message = "初期値です。";
        }
    }
}
