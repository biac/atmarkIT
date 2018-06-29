using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OldControlWpf
{
  /// <summary>
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      // JavaScript エラーのポップアップが出るのを抑止する
      // https://stackoverflow.com/a/18289217
      this.WebView1.Loaded += (s, e) =>
      {
        dynamic activeX = this.WebView1.GetType().InvokeMember("ActiveXInstance",
                      BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                      null, this.WebView1, new object[] { });
        activeX.Silent = true;
      };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      this.Title = string.Empty;
      this.MessageTextBox.Text = string.Empty;
      this.WebView1.Source = null;
      if (Uri.TryCreate(this.UrlTextBox.Text, UriKind.Absolute, out var uri))
        this.WebView1.Navigate(uri);
    }

    private async void WebView1_Navigating(object sender, NavigatingCancelEventArgs e)
    {
      await this.Dispatcher.InvokeAsync(() =>
      {
        if (e.Uri != null)
          this.MessageTextBox.Text += $"Navigating: {e.Uri}\n";
      });
    }

    private async void WebView1_Navigated(object sender, NavigationEventArgs e)
    {
      // ここは別スレッドから呼ばれることがあるので、InvokeAsync
      await this.Dispatcher.InvokeAsync(() =>
      {
        // https://stackoverflow.com/a/46132464/1327929
        // Web サイトからレスポンスが得られたかを確かめるには…、
        var doc = this.WebView1.Document as mshtml.HTMLDocument; // 表示されたドキュメントを取得し、
        string url = doc.url;
        if (url != null)
        {
          if (url.StartsWith("http")) // その URL が "http" や "https" で始まっていればOK。
                                      // ※不正なURLなどのときは "res://ieframe.dll/…" となる
          {
            // 正しく表示できた時の処理
            this.UrlTextBox.Text = url;
            this.Title = doc.title;
            this.MessageTextBox.Text += $"Navigated: {url}\n";
          }
          else if (url.StartsWith("about:"))
          {
            // abount:blank などのとき
            this.UrlTextBox.Text = url;
          }
          else // res://ieframe.dll/… など、エラーのとき
          {
            this.MessageTextBox.Text += $"{doc.URLUnencoded}\n";
            this.MessageTextBox.Text += $"{doc.nameProp}\n";
          }
        }
      });
    }
  }
}
