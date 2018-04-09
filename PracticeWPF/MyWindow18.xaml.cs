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
    /// LINQ
    /// </summary>
    public partial class MyWindow18 : Window
    {
        List<Employee> _employee = new List<Employee>();

        public class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int position { get; set; }
            public bool IsMarried { get; set; }
            public string Address { get; set; }
            public string Tel { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public string Note { get; set; }
        }

        enum Position
        {
            hira = 0,
            syunin = 1,
            kacyou = 2,
            bucyou = 2
        }

        public MyWindow18()
        {
            InitializeComponent();

            SetParameters();
            SetEvent();
        }

        private void SetParameters()
        {
            _employee.Add(new Employee { Name = "Fukuzawa", Age = 35, IsMarried = true, });
            _employee.Add(new Employee { Name = "Higuchi", Age = 28, IsMarried = true, });
            _employee.Add(new Employee { Name = "Noda", Age = 42, IsMarried = false, });
            _employee.Add(new Employee { Name = "Igawa", Age = 21, IsMarried = false, });
            _employee.Add(new Employee { Name = "Sawada", Age = 31, IsMarried = true, });
        }


        private void SetEvent()
        {
            myButton01.Click += (sender, e) => button01_Click_addedEvent();
            //Button02.Click += (sender, e) => button02_Click_addedEvent();
        }

        private void button01_Click_addedEvent()
        {
            List<Employee> _tmp_employee01;
            List<Employee> _tmp_employee02;

            //LINQで生成したリストを他のリストに放り込む時、.ToList() が無いと、キャストエラー。
            _tmp_employee01 = _employee
                                 .Where(x => x.position == (int)Position.kacyou).ToList();


            //order
            _tmp_employee02 = _employee
                                 .Where(x => x.Weight <= 60)
                                 .OrderByDescending(x => x.Age).ToList();

        }
    }
}
