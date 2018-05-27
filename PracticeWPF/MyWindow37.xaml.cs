using PracticeWPF.ViewModelSample;
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
    /// MVVM
    /// 
    /// 【参考サイト】
    /// https://docs.microsoft.com/ja-jp/dotnet/standard/cross-platform/using-portable-class-library-with-model-view-view-model
    /// 
    /// 動かないんで、公式と全く同じフォルダ構成にしたけど、それでも動かNEEEEE!!!!!!
    /// </summary>
    public partial class MyWindow37 : Window
    {
        private List<Customer> _customers;

        public MyWindow37()
        {
            InitializeComponent();



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
