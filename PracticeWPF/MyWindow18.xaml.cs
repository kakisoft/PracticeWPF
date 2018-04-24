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
            public int Position { get; set; }
            public bool IsMarried { get; set; }
            public string Address { get; set; }
            public string Tel { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public string Note { get; set; }
        }

        enum PositionCode
        {
            hira = 0,
            syunin = 1,
            kacyou = 2,
            bucyou = 3
        }

        public MyWindow18()
        {
            InitializeComponent();

            SetParameters();
            SetEvent();
        }

        private void SetParameters()
        {
            _employee.Add(new Employee { Name = "Fukuzawa" , Age = 35, IsMarried = true , Position = (int)PositionCode.hira   });
            _employee.Add(new Employee { Name = "Higuchi"  , Age = 28, IsMarried = true , Position = (int)PositionCode.hira   });
            _employee.Add(new Employee { Name = "Noda"     , Age = 42, IsMarried = false, Position = (int)PositionCode.hira   });
            _employee.Add(new Employee { Name = "Igawa"    , Age = 21, IsMarried = false, Position = (int)PositionCode.syunin });
            _employee.Add(new Employee { Name = "Sawada"   , Age = 31, IsMarried = true , Position = (int)PositionCode.syunin });
            _employee.Add(new Employee { Name = "yamaguchi", Age = 28, IsMarried = true , Position = (int)PositionCode.kacyou });
            _employee.Add(new Employee { Name = "fujisawa" , Age = 36, IsMarried = true , Position = (int)PositionCode.bucyou });
        }


        private void SetEvent()
        {
            myButton01.Click += (sender, e) => button01_Click_addedEvent();
            myButton02.Click += (sender, e) => button02_Click_addedEvent();
        }

        #region 他のリストを生成
        private void button01_Click_addedEvent()
        {
            List<Employee> _tmp_employee01;
            List<Employee> _tmp_employee02;

            //LINQで生成したリストを他のリストに放り込む時、.ToList() が無いと、キャストエラー。
            _tmp_employee01 = _employee
                                 .Where(x => x.Position == (int)PositionCode.kacyou).ToList();


            //order
            _tmp_employee02 = _employee
                                 .Where(x => x.Weight <= 60)
                                 .OrderByDescending(x => x.Age).ToList();

        }
        #endregion

        #region group by
        private void button02_Click_addedEvent()
        {
            var _grouped_employee01 = _employee.GroupBy(x => x.Position).ToList();

            foreach (var item1 in _grouped_employee01)
            {
                Console.WriteLine(item1.Key);
                foreach (var item2 in item1)
                {
                    Console.WriteLine(item2.Name);
                }
                Console.WriteLine("---------------------");
            }


            Console.WriteLine("================================");

            var _grouped_employee02 = from x in _employee
                                      group x by x.IsMarried;
            foreach (var item1 in _grouped_employee02)
            {
                Console.WriteLine(item1.Key);
                foreach (var item2 in item1)
                {
                    Console.WriteLine(item2.Name);
                }
                Console.WriteLine("---------------------");
            }

        }
        #endregion
    }
}
