using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace TimerSample002
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // 要usingに「using Windows.System.Threading;」を追加
        private ThreadPoolTimer _timer;

        private int _count;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._timer = ThreadPoolTimer.CreatePeriodicTimer(_timerEvent, TimeSpan.FromSeconds(1));
        }

        private async void _timerEvent(ThreadPoolTimer timer)
        {
            // ここで重い処理を実行
            // Buttonコントロールを配置してマウスオーバーハイライトを確認してみると良い
            //while (true)
            //{
            //    System.Diagnostics.Debug.WriteLine("終わらない");
            //}

            this._count++;

            // この記述はエラーになる
            //this.textBlock.Text = this._count.ToString();

            // UIスレッド以外のスレッドから画面を更新する場合はDispatcher.RunAsyncを利用する
            // 非同期処理なのでawait/asyncキーワードが必要になる
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.textBlock.Text = this._count.ToString();
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this._timer != null)
            {
                this._timer.Cancel();
                this._timer = null;
                this.button.Content = "タイマー再開";
            }
            else
            {
                this._timer = ThreadPoolTimer.CreatePeriodicTimer(_timerEvent, TimeSpan.FromSeconds(1));
                this.button.Content = "タイマー停止";
            }
        }
    }
}
