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
        #region 定義情報
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
        #endregion

        #region 定義情報２
        List<ParentClass> parentList = new List<ParentClass>();

        public class ParentClass
        {
            public int ParentId { get; set; }
            public string ParentName { get; set; }

            public List<ChildClass> ChildList { get; set; }
        }

        public class ChildClass
        {
            public int ChildId { get; set; }
            public string ChildName { get; set; }
        }
        #endregion

        #region 初期化
        public MyWindow18()
        {
            InitializeComponent();

            SetParameters();
            SetEvent();
        }

        private void SetParameters()
        {
            _employee.Add(new Employee { Name = "Fukuzawa", Age = 35, IsMarried = true, Position = (int)PositionCode.hira });
            _employee.Add(new Employee { Name = "Higuchi", Age = 28, IsMarried = true, Position = (int)PositionCode.hira });
            _employee.Add(new Employee { Name = "Noda", Age = 42, IsMarried = false, Position = (int)PositionCode.hira });
            _employee.Add(new Employee { Name = "Igawa", Age = 21, IsMarried = false, Position = (int)PositionCode.syunin });
            _employee.Add(new Employee { Name = "Sawada", Age = 31, IsMarried = true, Position = (int)PositionCode.syunin });
            _employee.Add(new Employee { Name = "yamaguchi", Age = 28, IsMarried = true, Position = (int)PositionCode.kacyou });
            _employee.Add(new Employee { Name = "fujisawa", Age = 36, IsMarried = true, Position = (int)PositionCode.bucyou });
            _employee.Add(new Employee { Name = "oda", Age = 20, IsMarried = false, Position = (int)PositionCode.hira });
            _employee.Add(new Employee { Name = "oda", Age = 20, IsMarried = false, Position = (int)PositionCode.hira });
            _employee.Add(new Employee { Name = "oda", Age = 20, IsMarried = false, Position = (int)PositionCode.hira });
        }

        private void SetEvent()
        {
            myButton01.Click += (sender, e) => button01_Click_addedEvent();
            myButton02.Click += (sender, e) => button02_Click_addedEvent();
            myButton03.Click += (sender, e) => button03_Click_addedEvent();
            myButton04.Click += (sender, e) => button04_Click_addedEvent();
            myButton05.Click += (sender, e) => button05_Click_addedEvent();
            myButton06.Click += (sender, e) => button06_Click_addedEvent();
            myButton07.Click += (sender, e) => button07_Click_addedEvent();
            myButton08.Click += (sender, e) => button08_Click_addedEvent();
        }
        #endregion

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
            //-------------------------------
            //
            //-------------------------------
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

            //-------------------------------
            //
            //-------------------------------
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


            Console.WriteLine("================================");

        }
        #endregion

        #region group by 複数キー
        private void button03_Click_addedEvent()
        {
            var _grouped_employee03 = _employee.GroupBy(x => new { x.Position, x.IsMarried }).ToList();

            foreach (var item1 in _grouped_employee03)
            {
                Console.WriteLine(item1.Key);
                Console.WriteLine(item1.Key.Position);
                Console.WriteLine(item1.Key.IsMarried);

                foreach (var item2 in item1)
                {
                    Console.WriteLine(item2.Name);
                }
                Console.WriteLine("---------------------");
            }
            }
        #endregion

        #region group by した内容を、カンマ区切りで取得
        private void button04_Click_addedEvent()
        {
            var _grouped_employee07 = _employee
                                        .Where(x => x.IsMarried == false)
                                        .Select(x => x.Name)
                                        .Distinct();


            string filterdNames = string.Join(",", _grouped_employee07);
            Console.WriteLine("============================");
            Console.WriteLine(filterdNames);
            Console.WriteLine("============================");

        }
        #endregion

        #region Distinct・Sum・Count
        private void button05_Click_addedEvent()
        {
            //---------------
            //   Distinct
            //---------------
            var _grouped_employee04 = _employee.Select(x => x.Age).Distinct();
            Console.WriteLine("============================");
            foreach (var item in _grouped_employee04)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("============================");


            //---------------
            //     Sum
            //---------------
            var _grouped_employee05 = _employee.Select(x => x.Age).Sum();
            Console.WriteLine("============================");
            Console.WriteLine(_grouped_employee05);
            Console.WriteLine("============================");


            //---------------
            //     Count
            //---------------
            var _grouped_employee06 = _employee.Select(x => x.Age).Count();
            Console.WriteLine("============================");
            Console.WriteLine(_grouped_employee06);
            Console.WriteLine("============================");

        }
        #endregion

        #region 特定条件の Count
        private void button06_Click_addedEvent()
        {
            //--------------------
            //  特定条件の Count
            //--------------------
            if (_employee
                    .Where(x => x.IsMarried).ToList()
                    .Count == 0)
            {
                return;
            }
        }
        #endregion

        #region 子リストを含むリスト
        private void button07_Click_addedEvent()
        {
            parentList = new List<ParentClass>();

            var pel1 = new ParentClass();
            pel1.ParentId = 1;
            pel1.ChildList = new List<ChildClass>();
            pel1.ChildList.Add(new ChildClass { ChildId = 1, ChildName = "c1"});
            pel1.ChildList.Add(new ChildClass { ChildId = 2, ChildName = "c2" });
            parentList.Add(pel1);


            var pel2 = new ParentClass();
            pel2.ParentId = 2;
            pel2.ChildList = new List<ChildClass>();
            pel2.ChildList.Add(new ChildClass { ChildId = 2, ChildName = "c3" });
            pel2.ChildList.Add(new ChildClass { ChildId = 3, ChildName = "c4" });
            pel2.ChildList.Add(new ChildClass { ChildId = 4, ChildName = "c5" });
            parentList.Add(pel2);



            int childElementCount = parentList.Sum(x => x.ChildList.ToList().Count);

            Console.WriteLine(childElementCount);
        }
        #endregion

        #region 条件を指定してListから削除
        private void button08_Click_addedEvent()
        {
            _employee.RemoveAll(x => x.Age == 20);

            _employee.RemoveAll(x => x.IsMarried == false);
        }
        #endregion

    }
}
