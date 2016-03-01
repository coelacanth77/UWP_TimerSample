using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace TimerSample001
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _timer;

        // イベントの実行回数をカウントする変数
        private int _count;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._timer = new DispatcherTimer();

            // タイマーイベントの間隔を指定。
            // ここでは1秒おきに実行する
            this._timer.Interval = TimeSpan.FromSeconds(1);

            // 1秒おきに実行するイベントを指定
            this._timer.Tick += _timer_Tick;

            // タイマーイベントを開始する
            this._timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {

            // コメントアウト(以下のコメントアウトコードを有効にするとUIスレッドで重い処理を実行した結果が確認できます。)
            // ここで重たい処理発生
            // ボタンの上にマウスオーバーした際にボタンがハイライトされるか確認してみましょう
            //while(true)
            //{
            //    System.Diagnostics.Debug.WriteLine("終わらない");
            //}

            // カウントを1加算
            this._count++;

            // TextBlockにカウントを表示
            this.textBlock.Text = this._count.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this._timer.IsEnabled == true)
            {
                this._timer.Stop();
                this.button.Content = "タイマー再開";
            }
            else
            {
                this._timer.Start();
                this.button.Content = "タイマー停止";
            }
        }
    }
}
