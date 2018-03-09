using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp
{
  /// <summary>
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new OpenFileDialog();
      dialog.Title = "バーコードの写った画像ファイルを開く";
      dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
      if (dialog.ShowDialog() != true)
        return;

      ClearResult();
      DoEvents();

      // 選択された画像ファイルを表示
      var source = new BitmapImage(new Uri(dialog.FileName));
      this.Image1.Source = source;

      // BitmapImageをバイト配列に変換
      byte[] data;
      var encoder = new BmpBitmapEncoder();
      encoder.Frames.Add(BitmapFrame.Create(source));
      using (var ms = new MemoryStream())
      {
        encoder.Save(ms);
        data = ms.ToArray();
      }

      // バーコード読み取り
      var result = await StdLib.ZXingBarcodeReader.DecodeAsync(data);
      if (result != null)
        ShowResult(result);
    }

    private void ClearResult()
    {
      this.Image1.Source = null;
      this.BarcodeFormatText.Text = "(N/A)";
      this.TextText.Text = "(N/A)";
    }

    private void ShowResult(StdLib.ZXingResultSummary result)
    {
      // テキスト表示
      this.BarcodeFormatText.Text = result.Format;
      this.TextText.Text = result.Text;
    }

    private void DoEvents()
    {
      DispatcherFrame frame = new DispatcherFrame();
      var callback = new DispatcherOperationCallback(obj =>
      {
        ((DispatcherFrame)obj).Continue = false;
        return null;
      });
      Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
      Dispatcher.PushFrame(frame);
    }
  }
}
