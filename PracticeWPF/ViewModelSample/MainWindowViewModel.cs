using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF.ViewModelSample
{
    class MainWindowViewModelSample : BindableBaseSample
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { this.SetProperty(ref this.message, value); }
        }

        #region コマンドの実装
        private RelayCommandSanple changeMessageCommand;
        public RelayCommandSanple ChangeMessageCommand
        {
            get { return changeMessageCommand = changeMessageCommand ?? new RelayCommandSanple(ChangeMessage); }
        }

        private void ChangeMessage()
        {
            this.Message = "コマンドを実行しました。";
        }
        #endregion


        public MainWindowViewModelSample()
        {
            this.Message = "初期値です。";
        }
    }
}
