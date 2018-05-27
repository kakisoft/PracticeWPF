using PracticeWPF.Common;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticeWPF.ViewModelSample
{
    class CalcViewModel : BindableBase
    {
        private string _leftValue;
        public string LeftValue
        {
            get { return _leftValue; }
            set { this.SetProperty(ref this._leftValue, value); }
        }

        private string _rightValue;
        public string RightValue
        {
            get { return _rightValue; }
            set { this.SetProperty(ref this._rightValue, value); }
        }

        private string _answerValue;
        public string AnswerValue
        {
            get { return _answerValue; }
            set { this.SetProperty(ref this._answerValue, value); }
        }

        private ICommand calcCommand;

        public ICommand CalcCommand
        {
            get { return this.calcCommand ?? (this.calcCommand = new DelegateCommand(CalcExecute, CanCalcExecute)); }
        }

        private bool CanCalcExecute()
        {
            return true;
        }

        private void CalcExecute()
        {
            //AnswerValue = IntToString(Calculation.Sum(StringToInt(LeftValue), StringToInt(RightValue)));
        }

        private int StringToInt(string src)
        {
            int ret = 0;
            if (int.TryParse(src, out ret))
            {
                return ret;
            }
            throw new ArgumentException("src" + src);
        }

        private string IntToString(int src)
        {
            return src.ToString();
        }
    }
}
