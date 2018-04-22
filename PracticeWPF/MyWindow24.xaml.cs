using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
    /// MyWindow24.xaml の相互作用ロジック
    /// </summary>
    public partial class MyWindow24 : Window
    {

        #region 初期化
        public MyWindow24()
        {
            InitializeComponent();
            AddMyEvent();
        }

        private void AddMyEvent()
        {
            myButton01.Click += (sender, e) => MyButton01_Click();
            myButton02.Click += (sender, e) => MyButton02_Click();
            myButton03.Click += (sender, e) => MyButton03_Click();
            myButton04.Click += (sender, e) => MyButton04_Click();
            myButton05.Click += (sender, e) => MyButton05_Click();
        }
        #endregion

        #region 【 ver1 】データ定義
        [DataContract]
        //[DataContract(Name = "Taka")]  //クラス名と異なる場合
        public class Person
        {
            [DataMember]
            //[DataMember(Name = "key")]  //メンバ名と異なる場合
            public int ID { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public IDictionary<string, string> Attributes { get; private set; }

            public Person()
            {
                this.Attributes = new Dictionary<string, string>();
            }
        }
        #endregion

        #region 【 ver1 】実装
        private void MyButton01_Click()
        {
            // シリアライズ
            var p_1 = new Person()
            {
                ID = 0,
                Name = "Taka",
            };
            p_1.Attributes.Add("key1", "value1");
            p_1.Attributes.Add("key2", "value2");
            p_1.Attributes.Add("key3", "value3");

            var p_2 = new Person()
            {
                ID = 1,
                Name = "PG",
            };
            p_2.Attributes.Add("keyAA", "valueAA");
            p_2.Attributes.Add("keyBB", "valueBB");
            p_2.Attributes.Add("keyCC", "valueCC");

            List<Person> pList = new List<Person>() { p_1, p_2 };

            string json = Serialize(pList);

            Console.WriteLine(json);

            // デシリアライズ
            var pDeserializeList = Deserialize<IList<Person>>(json);

            foreach (var p in pDeserializeList)
            {
                Console.WriteLine("ID = " + p.ID);
                Console.WriteLine("Name = " + p.Name);
                foreach (var att in p.Attributes)
                {
                    Console.WriteLine(att.Key + " = " + att.Value);
                }
            }

            Console.ReadLine();
        }

        /*
                [
                    {
                        "ID": 0, 
                        "Name": "Taka", 
                        "Attribute": {
                            "key1": "value1"
                            "key2": "value2"
                            "key3": "value3"
                        }
                    }, 
                    {
                        "ID": 1, 
                        "Name": "PG", 
                        "Attribute": {
                            "keyAA": "valueAA"
                            "keyBB": "valueBB"
                            "keyCC": "valueCC"
                        }
                    }
                ]        
        */


        /// <summary>
        /// 任意のオブジェクトをJsonメッセージへシリアライズします。
        /// </summary>
        public static string Serialize(object graph)
        {
            using (var stream = new MemoryStream())
            {
                var setting = new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true,
                };
                var serializer = new DataContractJsonSerializer(graph.GetType(), setting);
                serializer.WriteObject(stream, graph);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Jsonメッセージをオブジェクトへデシリアライズします。
        /// </summary>
        public static T Deserialize<T>(string message)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var setting = new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true,
                };
                var serializer = new DataContractJsonSerializer(typeof(T), setting);
                return (T)serializer.ReadObject(stream);
            }
        }
        #endregion

        #region 【 ver2 】データ定義
        //（参考） http://yasuand.hatenablog.com/entry/2013/09/12/051655
        [DataContract]
        class Data
        {
            [DataMember]
            public int a;

            [DataMember]
            public double d;

            [DataMember]
            public string s;



            [DataMember(Name = "created_at")]
            private string created_at_str_prop
            {
                get
                {
                    return created_at_str_field;
                }

                set
                {
                    created_at_field = DateTime.ParseExact(value, "ddd MMM dd HH:mm:ss zzz yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
                    created_at_str_field = value;
                }
            }
            private string created_at_str_field;
            public DateTime created_at
            {
                get
                {
                    return created_at_field;
                }

                set
                {
                    created_at_str_field = value.ToString("ddd MMM dd HH:mm:ss zzz yyyy", DateTimeFormatInfo.InvariantInfo);
                    created_at_field = value;
                }
            }
            private DateTime created_at_field;
        }
        #endregion

        #region 【 ver2 】実装
        private void MyButton02_Click()
        {
            try
            {
                //未定義の情報は無視される
                var json = @"{""a"":123, ""s"":""test!"", ""pi"":3.14}";

                var serializer = new DataContractJsonSerializer(typeof(Data));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var data = (Data)serializer.ReadObject(ms);
                    Console.WriteLine(data.a);
                    Console.WriteLine(data.d);
                    Console.WriteLine(data.s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 【 ver3 】データ定義
        //（参考）http://blog.clock-up.jp/entry/2016/08/26/csharp-json
        [DataContract]
        class Book
        {
            [DataMember]
            public int Id { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Asin { get; set; }
        }
        #endregion

        #region 【 ver3 】実装１（シリアライズ）
        private void MyButton03_Click()
        {
            // 日本語ちゃんと出力されるようにしておく
            //Console.OutputEncoding = new UTF8Encoding();

            // せっかくだからリスト構造を作ってみる
            List<Book> books = new List<Book>
            {
              new Book{ Id = 1, Name = "野望の王国 1", Asin = "B00M84FTOS"},
              new Book{ Id = 2, Name = "アカギ 1", Asin = "B00DVYTZ12"},
            };

            // 普通の JSON テキスト構築
            Console.WriteLine("-------- normal json --------");
            string jsonString = JsonConvert.SerializeObject(books);
            Console.WriteLine(jsonString);

            // インデント付きの JSON テキスト構築
            Console.WriteLine("\n-------- formatted json --------");
            string formattedJson = JsonConvert.SerializeObject(books, Formatting.Indented);
            Console.WriteLine(formattedJson);
        }
        #endregion

        #region 【 ver3 】実装２（デシリアライズ）
        private void MyButton04_Click()
        {
            // 日本語ちゃんと出力されるようにしておく
            //Console.OutputEncoding = new UTF8Encoding();

            // JSON テキストのサンプル (書式はあえて適当に崩した)
            string jsonSample = @"[
              { ""Id"": 10, ""Name"": ""フェルマーの最終定理"", ""Asin"": ""4102159711"" },
              {
                ""Asin"": ""410215972X"",
                ""Name"" : ""暗号解読"", ""Id"": 20
              }
            ]";

            // JSON テキストから C# オブジェクトを構築
            List<Book> booksByJson = JsonConvert.DeserializeObject<List<Book>>(jsonSample);

            // 結果確認用出力
            Console.WriteLine("-------- parse json result --------");
            booksByJson.ForEach((book) =>
            {
                Console.WriteLine(book.Id + ", " + book.Name + ", " + book.Asin);
            });
        }
        #endregion

        #region 【 ver3 】実装３（外部ファイルから読み込んで、デシリアライズ）
        private void MyButton05_Click()
        {
            try
            {
                //パス指定
                string targetDirectory = System.Environment.CurrentDirectory + "\\..\\" + "\\..\\" + "Resources\\";
                string targetFileName = "myJsonFile01.json";
                string targetFileFullPath = targetDirectory + targetFileName;

                //ファイル読み込み
                System.IO.FileStream fs = new System.IO.FileStream(targetFileFullPath, System.IO.FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string myJsonContent = sr.ReadToEnd();

                //デシリアライズ
                List<Book> booksByJson = JsonConvert.DeserializeObject<List<Book>>(myJsonContent);

                //コンソール出力
                foreach (var item in booksByJson)
                {
                    Console.WriteLine("item:" + item.Id + ", Name:" + item.Name + ", Asin:" + item.Asin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}