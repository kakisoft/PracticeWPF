using PracticeWPF.ViewModelSample;
using SimpleMVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


/// <summary>
/// MVVM
/// 
/// 【参考サイト】
/// https://docs.microsoft.com/ja-jp/dotnet/standard/cross-platform/using-portable-class-library-with-model-view-view-model
/// 
/// 動かないんで、公式と全く同じフォルダ構成にしたけど、それでも動かNEEEEE!!!!!!
/// ってか、このサンプルの V（ビハインド）はどこじゃい。
/// </summary>


#region *************************【 Model 】*************************
namespace SimpleMVVM.Model
{
    public class Customer
    {
        public int CustomerID
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }
    }
}
#endregion






#region *************************【 Model2 】*************************
namespace SimpleMVVM.Model
{
    public class CustomerRepository
    {
        private List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer(){ CustomerID = 1, FullName="Dana Birkby", Phone="394-555-0181"},
                new Customer(){ CustomerID = 2, FullName="Adriana Giorgi", Phone="117-555-0119"},
                new Customer(){ CustomerID = 3, FullName="Wei Yu", Phone="798-555-0118"}
            };
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public void UpdateCustomer(Customer SelectedCustomer)
        {
            Customer customerToChange = _customers.Single(c => c.CustomerID == SelectedCustomer.CustomerID);
            customerToChange = SelectedCustomer;
        }
    }
}
#endregion







#region *************************【 ViewModel 】*************************
namespace SimpleMVVM.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
#endregion







#region *************************【 ViewModel2 】*************************
namespace SimpleMVVM.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _handler;
        private bool _isEnabled;

        public RelayCommand(Action handler)
        {
            _handler = handler;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
#endregion







namespace SimpleMVVM.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private List<PracticeWPF.ViewModelSample.Customer> _customers;
        private PracticeWPF.ViewModelSample.Customer _currentCustomer;
        private CustomerRepository _repository;

        public CustomerViewModel()
        {
            _repository = new CustomerRepository();
            //_customers = _repository.GetCustomers();

            WireCommands();
        }

        private void WireCommands()
        {
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
        }

        public RelayCommand UpdateCustomerCommand
        {
            get;
            private set;
        }

        public List<PracticeWPF.ViewModelSample.Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        public PracticeWPF.ViewModelSample.Customer CurrentCustomer
        {
            get
            {
                return _currentCustomer;
            }

            set
            {
                if (_currentCustomer != value)
                {
                    _currentCustomer = value;
                    OnPropertyChanged("CurrentCustomer");
                    UpdateCustomerCommand.IsEnabled = true;
                }
            }
        }

        public void UpdateCustomer()
        {
            //_repository.UpdateCustomer(CurrentCustomer);
        }
    }
}










namespace PracticeWPF
{
    public partial class MyWindow37 : Window
    {
        public MyWindow37()
        {
            InitializeComponent();
        }
    }
}
