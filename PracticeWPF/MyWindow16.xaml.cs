using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// MyWindow16.xaml の相互作用ロジック
    /// ツール→NuGetパッケージマネージャ→ソリューションのNuGetパッケージの管理：『Json.NET』
    /// </summary>
    public partial class MyWindow16 : Window
    {
        public MyWindow16()
        {
            InitializeComponent();
        }

        [JsonObject]
        public class Person
        {
            [JsonProperty("age")]
            public int Age { get; private set; }

            [JsonProperty("name")]
            public string Name { get; private set; }

            public Person(int age, string name)
            {
                this.Age = age;
                this.Name = name;
            }
        }

        #region －１－
        //===========================================
        //
        //
        //===========================================
        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            Button01_ClickAsync(sender, e);
        }

        private async void Button01_ClickAsync(object sender, RoutedEventArgs e)
        {
            await GetHttpResponse();
        }

        private async Task GetHttpResponse()
        {
            var json = "{ \"age\" : 20, \"name\" : \"太郎\"  }";

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //var accept = new MediaTypeWithQualityHeaderValue("application/json");
                //client.DefaultRequestHeaders.Accept.Add(accept);

                var response = await client.PostAsync("http://somehost/someapi", content);

                Console.WriteLine(response);
            }
        }
        #endregion

        #region  －２－
        //===========================================
        //
        //
        //===========================================
        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("HttpClientクラスでWebページを取得する");

            // 時間計測用のタイマー
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            // 取得したいWebページのURI
            Uri webUri = new Uri("http://dev.windows.com/ja-jp");
            //Uri webUri = new Uri("https://dev.windows.com/ja-jp"); // HTTPSでもOK!
            //Uri webUri = new Uri("https://dev.windows.com/"); // デフォルトではリダイレクト先を取得してくれる

            //Uri webUri = new Uri("https://appdev.microsoft.com/"); // 403エラー
            //Uri webUri = new Uri("https://appdev.microsoft.com/ja-JP/"); // 404エラー
            //Uri webUri = new Uri("http://notexist.example.com/"); // リモート名を解決できないエラー

            // GetWebPageAsyncメソッドを呼び出す
            Task<string> webTask = GetWebPageAsync(webUri);
            webTask.Wait(); // Mainメソッドではawaitできないので、処理が完了するまで待機する
            string result = webTask.Result;  // 結果を取得

            timer.Stop();
            Console.WriteLine("{0:0.000}秒", timer.Elapsed.TotalSeconds);
            Console.WriteLine();

            // 取得結果を使った処理
            if (result != null)
            {
                // サンプルとして、取得したHTMLデータの<h1>タグ以降を一定長だけ表示
                Console.WriteLine("========");
                int h1pos = result.IndexOf("<h1", StringComparison.OrdinalIgnoreCase);
                if (h1pos < 0)
                    h1pos = 0;
                const int MaxLength = 720;
                int len = result.Length - h1pos;
                if (len > MaxLength)
                    len = MaxLength;
                Console.WriteLine(result.Substring(h1pos, len));
                Console.WriteLine("========");
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
        static async Task<string> GetWebPageAsync(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                // ユーザーエージェント文字列をセット（オプション）
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");

                // 受け入れ言語をセット（オプション）
                client.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");

                // タイムアウトをセット（オプション）
                client.Timeout = TimeSpan.FromSeconds(10.0);

                try
                {
                    // Webページを取得するのは、事実上この1行だけ
                    return await client.GetStringAsync(uri);
                }
                catch (HttpRequestException e)
                {
                    // 404エラーや、名前解決失敗など
                    Console.WriteLine("\n例外発生!");
                    // InnerExceptionも含めて、再帰的に例外メッセージを表示する
                    Exception ex = e;
                    while (ex != null)
                    {
                        Console.WriteLine("例外メッセージ: {0} ", ex.Message);
                        ex = ex.InnerException;
                    }
                }
                catch (TaskCanceledException e)
                {
                    // タスクがキャンセルされたとき（一般的にタイムアウト）
                    Console.WriteLine("\nタイムアウト!");
                    Console.WriteLine("例外メッセージ: {0} ", e.Message);
                }
                return null;
            }
        }
        #endregion

        #region  －３－
        //===========================================
        //
        //
        //===========================================
        private async void Button03_Click(object sender, RoutedEventArgs e)
        {
            await Button03_Click_Async(sender, e);
        }
        private async Task Button03_Click_Async(object sender, RoutedEventArgs e)
        {
            await GetHttpResponse03Async();
        }

        private async Task GetHttpResponse03Async()
        {
            var httpClient = new HttpClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                                                        {
                                                            { "foo", "111" },
                                                            { "bar", "222" },
                                                            { "baz", "333" },
                                                        });

            var response = await httpClient.PostAsync("http://localhost/", content);
        }

        #endregion

        #region  －４－
        //===========================================
        //
        //
        //===========================================
        private void Button04_Click(object sender, RoutedEventArgs e)
        {
            //HTTPリクエスト
            var req = HttpWebRequest.Create("http://www.google.co.jp/");
            req.Timeout = 15000;//タイムアウト(15秒)
            //HTPレスポンス
            var res = (HttpWebResponse)req.GetResponse();

            Console.WriteLine(res);
        }
        #endregion

        #region  －５－
        //===========================================
        //
        //
        //===========================================
        private void Button05_Click(object sender, RoutedEventArgs e)
        {
            const string TARGET_URL = "http://challenge-your-limits.herokuapp.com/call/me";

            //HTTPリクエスト
            var req = HttpWebRequest.Create(TARGET_URL);
            req.Timeout = 15000;//タイムアウト(15秒)
            //HTPレスポンス
            var res = (HttpWebResponse)req.GetResponse();

            Console.WriteLine(res);
        }
        #endregion

    }
}
