using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Xml;

namespace PracticeWPF
{
    /// <summary>
    /// XMLのシリアライズ・デシリアライズ
    /// </summary>

    //==============================================
    // ソリューションエクスプローラの「参照」にて右クリック→「参照の追加」
    // 『System.Runtime.Serialization』を追加。
    //==============================================
    public partial class MyWindow22 : Window
    {

        #region データ定義
        public class Person01
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        [System.Runtime.Serialization.DataContract]
        public class Person02
        {
            [System.Runtime.Serialization.DataMember]
            public int ID { get; set; }
            [System.Runtime.Serialization.DataMember]
            public string Name { get; set; }
            [DataMember(Name="ad")]  //xml出力する時、要素名が「ad」となる。
            public string Address { get; set; }
        }
        #endregion

        //==============================================
        // DataContract
        //   ・DataContractSerializerで使われる
        //   ・System.Runtime.Serializationを参照に追加することで使用可能。
        //   ・DataContractをクラスへ、DataMemberをプロパティへつける。
        //   ・DataContractSerializerのWriteObjectでシリアライズ、ReadObjectでデシリアライズできる
        //==============================================

        public MyWindow22()
        {
            InitializeComponent();

            AddMyEvent();
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            myButton02.Click += (sender, e) => MyButton02_Click();
            myButton03.Click += (sender, e) => MyButton03_Click();
        }

        #region 属性を付けない場合
        private void MyButton01_Click()
        {
            //============================
            //   属性を付けない場合
            //============================

            // コンストラクタにターゲットの型を渡す  
            var ds1 = new DataContractSerializer(typeof(Person01));

            // 出力先を作成  
            var sw1 = new StringWriter();
            var xw1 = new XmlTextWriter(sw1);
            // 読みやすいように整形
            xw1.Formatting = Formatting.Indented;

            // シリアライズ
            ds1.WriteObject(xw1, new Person01 { ID = 1, Name = "田中　太郎" });

            // 結果確認  
            Console.WriteLine(sw1);  //シリアライズ可

            // デシリアライズ  
            var sr1 = new StringReader(sw1.ToString());
            var xr1 = new XmlTextReader(sr1);
            var person1 = (Person01)ds1.ReadObject(xr1);
            Console.WriteLine("ID:{0}, Name:{1}", person1.ID, person1.Name);   //デシリアライズ可
        }
        #endregion

        #region 属性を付ける場合
        private void MyButton02_Click()
        {
            //============================
            //      属性を付ける場合
            //============================

            // コンストラクタにターゲットの型を渡す  
            var ds2 = new DataContractSerializer(typeof(Person02));

            // 出力先を作成  
            var sw2 = new StringWriter();
            var xw2 = new XmlTextWriter(sw2);
            // 読みやすいように整形
            xw2.Formatting = Formatting.Indented;

            // シリアライズ
            ds2.WriteObject(xw2, new Person02 { ID = 1, Name = "田中　太郎" });

            // 結果確認  
            Console.WriteLine(sw2);  //シリアライズ可

            // デシリアライズ  
            var sr2 = new StringReader(sw2.ToString());
            var xr2 = new XmlTextReader(sr2);
            var person2 = (Person02)ds2.ReadObject(xr2);
            Console.WriteLine("ID:{0}, Name:{1}", person2.ID, person2.Name);   //デシリアライズ可
        }
        #endregion

        private void MyButton03_Click()
        {
            //============================
            //         複数要素
            //============================

            // コンストラクタにターゲットの型を渡す  
            var persons = new DataContractSerializer(typeof(List<Person02>));

            // 出力先を作成  
            var sw = new StringWriter();
            var xw = new XmlTextWriter(sw);
            // 読みやすいように整形
            xw.Formatting = Formatting.Indented;

            //要素を作成
            var contents = new List<Person02>();
            contents.Add(new Person02 { ID = 1, Name = "田中　太郎" });
            contents.Add(new Person02 { ID = 2, Name = "石川　五右衛門" });
            contents.Add(new Person02 { ID = 3, Name = "柳生　十兵衛"  , Address = "江戸"});
            // シリアライズ
            persons.WriteObject(xw, contents);

            // 結果確認  
            Console.WriteLine(sw);  //シリアライズ可

            // デシリアライズ  
            var sr = new StringReader(sw.ToString());
            var xr = new XmlTextReader(sr);

            List<Person02> person2 = (List<Person02>)persons.ReadObject(xr);

            //コンソール出力
            Console.WriteLine(person2);
            foreach (var item in person2)
            {
                Console.WriteLine("ID:" + item.ID + ",  Name:" + item.Name + ",  Address:" + item.Address);

            }


            //Console.WriteLine("ID:{0}, Name:{1}", person2.ID, person2.Name);   //デシリアライズ可

        }
    }
}
