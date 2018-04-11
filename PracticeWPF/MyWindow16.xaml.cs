using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PracticeWPF
{
    /// <summary>
    /// HTTP通信：非同期
    /// ツール→NuGetパッケージマネージャ→ソリューションのNuGetパッケージの管理：『Json.NET』
    /// </summary>
    public partial class MyWindow16 : Window
    {
        //-------------------------------------------------------
        // .NETクライアントからHTTPリクエストを送る場合、従来は、
        //WebClientクラスや
        //WebRequest, WebResponse クラスを
        //使用していたが、こちらは設計が古いもよう。
        //
        //新しくは、.NET Framework 4.5 では BCL 入りした
        //System.Net.Http.dllの HttpClient クラスを使用する。
        //https://techinfoofmicrosofttech.osscons.jp/index.php?HttpClient%E3%81%AE%E9%A1%9E%E3%81%AE%E4%BD%BF%E3%81%84%E6%96%B9
        //-------------------------------------------------------
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

        public class MyResult
        {
            public HttpStatusCode StatusCode { get; set; }
            public string Response { get; set; }
        }

        #region －１－
        //===========================================
        //             JSON形式で送信
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
            //http://challenge-your-limits.herokuapp.com/call/me
            string targetURL;
            if (textBox04.Text.Trim() != "")
            {
                targetURL = textBox04.Text;
            }
            else
            {
                targetURL = "http://www.google.co.jp/";
            }


            //HTTPリクエスト
            var req = HttpWebRequest.Create(targetURL);
            req.Timeout = 15000;//タイムアウト(15秒)
            //HTPレスポンス
            var res = (HttpWebResponse)req.GetResponse();


            Console.WriteLine(res);


            //ヘッダ取得
            Console.WriteLine("");
            Console.WriteLine("[SEND]");
            var h = req.Headers;
            for (int i = 0; i < h.Count; i++ ) {
                Console.WriteLine("{0}: {1}", h.GetKey(i), h[i]);
            }

            //ヘッダ取得
            Console.WriteLine("");
            Console.WriteLine("[RECV]");
            h = res.Headers;
            for (int i = 0; i < h.Count; i++) {
                Console.WriteLine("{0}: {1}", h.GetKey(i), h[i]);
            }

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

        #region  －６－
        //==========< 同期 >=========
        private void Button06_1_Click(object sender, RoutedEventArgs e)
        {
            Download();
        }
        public static string ReadFromUrl(Uri url)
        {
            using (WebClient webClient = new WebClient())
            {
                using (Stream stream = webClient.OpenRead(url))
                {
                    TextReader tr = new StreamReader(stream, Encoding.UTF8, true);
                    string body = tr.ReadToEnd();
                    return body;
                }
            }
        }

        public static void Download()
        {
            Uri url = new Uri("https://github.com/Microsoft/dotnet/blob/master/README.md");
            string body = ReadFromUrl(url);
            Console.WriteLine(body);
        }

        //==========< 非同期 >=========
        private async void Button06_2_ClickAsync(object sender, RoutedEventArgs e)
        {
            await DownloadAsync();
        }
        public static async Task DownloadAsync()
        {
            Uri url = new Uri("https://github.com/Microsoft/dotnet/blob/master/README.md");
            // Step8: ReadFromUrlから非同期対応のReadFromUrlAsyncに変更する。
            // Step9: ReadFromUrlAsyncがTask<string>を返すので、awaitする。
            //        awaitすると、stringが得られる。
            string body = await ReadFromUrlAsync(url);
            Console.WriteLine(body);
        }
        public static async Task<string> ReadFromUrlAsync(Uri url)
        {
            using (WebClient webClient = new WebClient())
            {
                // Step1: OpenReadから非同期対応のOpenReadTaskAsyncに変更する。
                // Step2: OpenReadTaskAsyncがTask<Stream>を返すので、awaitする。
                //        awaitすると、Streamが得られる。
                using (Stream stream = await webClient.OpenReadTaskAsync(url))
                {
                    TextReader tr = new StreamReader(stream, Encoding.UTF8, true);
                    // Step3: ReadToEndから非同期対応のReadToEndAsyncに変更する。
                    // Step4: ReadToEndAsyncがTask<string>を返すので、awaitする。
                    //        awaitすると、stringが得られる。
                    string body = await tr.ReadToEndAsync();
                    return body;
                }
            }
        }
        #endregion

        #region  －７－
        static int count = 1;
        private void Button07_Click(object sender, RoutedEventArgs e)
        {
            Task task = Task.Factory.StartNew(() => {
                Console.WriteLine(count.ToString() + "！");
                count++;
                Thread.Sleep(1000);
                Console.WriteLine(count.ToString() + "！");
                count++;
                Thread.Sleep(1000);
                Console.WriteLine(count.ToString() + "！");
                count++;
                Thread.Sleep(1000);
                Console.WriteLine("だ～～～～～～～～～～～～～～～～～～～～～～～～～～～～");
                Thread.Sleep(1000);
            });

            // メインスレッドが先に終わらないように・・・
            while (true) ;
        }
        #endregion

        #region  －８－
        //===========================================
        //         シンプルなTaskの使い方
        //
        //===========================================
        private void Button08_1_Click(object sender, RoutedEventArgs e)
        {
            TaskSample01();
        }
        //Task <TResult>クラスは、値を返し、通常は非同期に実行される単一の操作を表します。
        private void TaskSample01()
        {
            var t = Task<int>.Run(() => {
                // Just loop.
                int max = 1000000;
                int ctr = 0;
                for (ctr = 0; ctr <= max; ctr++)
                {
                    if (ctr == max / 2 && DateTime.Now.Hour <= 12)
                    {
                        ctr++;
                        break;
                    }
                }
                return ctr;
            });
            Console.WriteLine("Finished {0:N0} iterations.", t.Result);
        }

        //===========================================
        //          明示的なタスクの実行
        //https://docs.microsoft.com/ja-jp/dotnet/standard/parallel-programming/task-based-asynchronous-programming?view=netframework-4.7.2
        //===========================================
        private void Button08_2_Click(object sender, RoutedEventArgs e)
        {
            TaskSample02();
        }
        private void TaskSample02()
        {
            Thread.CurrentThread.Name = "Main";

            // Create a task and supply a user delegate by using a lambda expression. 
            Task taskA = new Task(() => Console.WriteLine("Hello from taskA."));
            // Start the task.
            taskA.Start();

            // Output a message from the calling thread.
            Console.WriteLine("Hello from thread '{0}'.",
                              Thread.CurrentThread.Name);
            taskA.Wait();
        }
        // The example displays output like the following:
        //       Hello from thread 'Main'.
        //       Hello from taskA.


        //===========================================
        //          Task<TResult>
        //
        //===========================================
        private void Button08_3_Click(object sender, RoutedEventArgs e)
        {
            TaskSample03();
        }
        private void TaskSample03()
        {
            var t = Task<int>.Run(() => {
                // Just loop.
                int max = 1000000;
                int ctr = 0;
                for (ctr = 0; ctr <= max; ctr++)
                {
                    if (ctr == max / 2 && DateTime.Now.Hour <= 12)
                    {
                        ctr++;
                        break;
                    }
                }
                return ctr;
            });
            Console.WriteLine("Finished {0:N0} iterations.", t.Result);
        }

        //===========================================
        //     Task.Status Property　　状態確認
        //https://docs.microsoft.com/ja-jp/dotnet/api/system.threading.tasks.task.status?view=netframework-4.7.2
        //===========================================
        private void Button08_4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskSample04();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TaskSample04()
        {
            var tasks = new List<Task<int>>();
            var source = new CancellationTokenSource();
            var token = source.Token;
            int completedIterations = 0;

            for (int n = 0; n <= 19; n++)
                tasks.Add(Task.Run(() => {
                    int iterations = 0;
                    for (int ctr = 1; ctr <= 2000000; ctr++)
                    {
                        token.ThrowIfCancellationRequested();
                        iterations++;
                    }
                    Interlocked.Increment(ref completedIterations);
                    if (completedIterations >= 10)
                        source.Cancel();
                    return iterations;
                }, token));

            Console.WriteLine("Waiting for the first 10 tasks to complete...\n");
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException)
            {
                Console.WriteLine("Status of tasks:\n");
                Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id",
                                  "Status", "Iterations");
                foreach (var t in tasks)
                    Console.WriteLine("{0,10} {1,20} {2,14}",
                                      t.Id, t.Status,
                                      t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
            }
        }
        // The example displays output like the following:
        //    Waiting for the first 10 tasks to complete...
        //    Status of tasks:
        //
        //       Task Id               Status     Iterations
        //             1      RanToCompletion      2,000,000
        //             2      RanToCompletion      2,000,000
        //（中略）
        //            16      RanToCompletion      2,000,000
        //            17             Canceled            n/a
        //            20             Canceled            n/a        #endregion
        #endregion

        #region  －９－
        //===========================================
        //
        //https://araramistudio.jimdo.com/2015/08/26/async-await%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%9F%E9%9D%9E%E5%90%8C%E6%9C%9F%E5%87%A6%E7%90%86/
        //===========================================
        private CancellationTokenSource m_CancelToken;
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart_Click_Content();
        }
        private async void btnStart_Click_Content()
        {
            this.btnStart.IsEnabled = false;
            this.btnCancel.IsEnabled = true;

            //キャンセル処理にはCancellationTokenSourceを使う
            m_CancelToken = new CancellationTokenSource();


            //awaitで並列処理化
            //並列処理部分を匿名メソッドで定義する
            await Task.Run(() => {
                for (int i = 1; i <= 10; ++i)
                {
                    if (m_CancelToken.IsCancellationRequested) break;

                    //非同期で処理されるので直接UIにアクセスできない
                    //Invokeをつかって処理する
                    Dispatcher.Invoke((Action)(() => {
                        this.textboxProc1.Text = "処理：(" + i + "/10)";
                    }));
                    Thread.Sleep(1000);
                }
            });
            //awaitで並列処理部分の処理を待つが、その間他の処理がちゃんと動く

            m_CancelToken.Dispose();
            m_CancelToken = null;

            this.btnCancel.IsEnabled = false;
            this.btnStart.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //awaitで並列処理部分の処理を待つ間でもここに来る
            if (null != m_CancelToken)
            {
                m_CancelToken.Cancel();
            }
        }
        #endregion

        #region  －１０－
        //===========================================
        //
        //
        //===========================================
        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region  －１１－
        //===========================================
        //            シンプルなGet
        //
        //===========================================
        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            //http://challenge-your-limits.herokuapp.com/call/me
            string targetURL;
            if (textBox11.Text.Trim() != "")
            {
                targetURL = textBox11.Text;
            }
            else
            {
                targetURL = "http://www.google.co.jp/";
            }

            Button11_Click_Content(targetURL);
        }
        private async void Button11_Click_Content(string targetURL)
        {
            try
            {
                string content = await AccessTheWebAsync(targetURL);

                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async Task<string> AccessTheWebAsync(string targetURL)
        {
            HttpClient client = new HttpClient();

            Task<string> getStringTask = client.GetStringAsync(targetURL);
            string urlContents = await getStringTask;

            return urlContents;
        }
        #endregion

        #region  －１２－
        //===========================================
        //            シンプルなPOST
        //
        //===========================================
        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            //http://challenge-your-limits.herokuapp.com/challenge_users
            string targetURL;
            if (textBox11.Text.Trim() != "")
            {
                targetURL = textBox11.Text;
            }
            else
            {
                targetURL = "http://www.google.co.jp/";
            }

            Button12_Click_Content(targetURL);
        }
        private async void Button12_Click_Content(string targetURL)
        {
            try
            {
                string content = await PostAccessTheWebAsync(targetURL);

                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async Task<string> PostAccessTheWebAsync(string targetURL)
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "name", "YourName" },
                { "email", "hoooobaaaaaa@tran.com" },
            });
            var response = await client.PostAsync(targetURL, content);
            string urlContents = await response.Content.ReadAsStringAsync();

            return urlContents;
        }
        #endregion

        #region  －１３－
        //===========================================
        //          Taskを使用したPOST
        //
        //===========================================
        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            //http://challenge-your-limits.herokuapp.com/challenge_users
            string targetURL;
            if (textBox11.Text.Trim() != "")
            {
                targetURL = textBox11.Text;
            }
            else
            {
                targetURL = "http://www.google.co.jp/";
            }

            Button13_Click_Content(targetURL);
        }
        private void Button13_Click_Content(string targetURL)
        {
            try
            {
                var task = Task.Run(() =>
                {
                    return PostAccess02TheWebAsync(targetURL);
                });
                System.Console.WriteLine(task.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async Task<string> PostAccess02TheWebAsync(string targetURL)
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "name", "YourName" },
                { "email", "hoooobaaaaaa@tran.com" },
            });
            var response = await client.PostAsync(targetURL, content);
            string urlContents = await response.Content.ReadAsStringAsync();

            return urlContents;
        }
        #endregion

        #region  －１４－
        //===========================================
        //          
        //https://medium.com/@joni2nja/leveraging-c-7-tuple-how-to-stream-api-result-to-the-client-using-tuple-3ce68a75ae7c
        //===========================================
        //private readonly HttpClient _httpClient;
        //public MyService1(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            string targetURL;
            if (textBox11.Text.Trim() != "")
            {
                targetURL = textBox11.Text;
            }
            else
            {
                targetURL = "http://www.google.co.jp/";
            }

            //-------(     )-------
            if( ((Button)sender).Content.ToString() == "－１４＿１－")
            {
                Button14_1_Click_Content(targetURL);
            }
            else
            {
                Button14_2_Click_Content(targetURL);
            }

        }

        //==========< －１４＿１－  >==========
        private async void Button14_1_Click_Content(string targetURL)
        {
            var result = await PostAccessFromButton14_1Async(targetURL);

            Console.WriteLine(result.StatusCode);
            Console.WriteLine(result.Response);
        }

        public async Task<MyResult> PostAccessFromButton14_1Async(string targetURL)
        {
            var _httpClient = new HttpClient();

            var result = await _httpClient.PostAsync("http://example.com/", new StringContent(""));
            return new MyResult
            {
                StatusCode = result.StatusCode,
                Response = await result.Content.ReadAsStringAsync()
            };
        }

        //==========< －１４＿２－  >==========
        private async void Button14_2_Click_Content(string targetURL)
        {
            var result = await PostAccessFromButton14_2Async(targetURL);

            Console.WriteLine(result);
        }

        //--------------------------------------------------------------
        // Tuple
        //    NuGet：Install-Package System.ValueTuple -Version 4.4.0
        //--------------------------------------------------------------
        public async Task<(HttpStatusCode StatusCode, string Response)> PostAccessFromButton14_2Async(string targetURL)
        {
            var _httpClient = new HttpClient();

            var result = await _httpClient.PostAsync("http://example.com/", new StringContent(""));
            return (result.StatusCode, await result.Content.ReadAsStringAsync());
        }
        #endregion

    }
}
