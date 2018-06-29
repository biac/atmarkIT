using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System;
using System.Windows.Forms;

namespace dotNetTips1238WinFormCS
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      this.Text = null;
      this.MessageTextBox.Text = string.Empty;
      if (Uri.TryCreate(this.UrlTextBox.Text, UriKind.Absolute, out var uri))
        this.WebView1.Navigate(uri);
    }

    // ナビゲーション開始時に発生するイベント
    private void WebView1_NavigationStarting(object sender, WebViewControlNavigationStartingEventArgs e)
    {
      // 例えば e.Uri が、これから表示する新しいURI
      if (e.Uri != null)
        this.MessageTextBox.Text += $"Navigate: {e.Uri}\r\n";
      // また、e.Cancelプロパティにfalseを設定して、ナビゲーションをキャンセル可能
    }

    // ナビゲーション完了時に発生するイベント
    private void WebView1_NavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
    {
      if (e.IsSuccess)
      {
        // 表示に成功した
        // 例えば、e.Uriが、表示したWebページのURI
        // WebView1.DocumentTitleが、Webページのタイトル
        this.Text = this.WebView1.DocumentTitle;
        this.UrlTextBox.Text = e.Uri.ToString();
        this.MessageTextBox.Text += $"Completed: {e.Uri}\r\n";
      }
      else
      {
        // 表示に失敗した
        // e.WebErrorStatusでエラーが分かる
        this.MessageTextBox.Text += $"Failed: {e.WebErrorStatus} ({(int)e.WebErrorStatus})\r\n";
      }
    }
  }
}
