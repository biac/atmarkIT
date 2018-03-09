using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using System.Runtime.InteropServices;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace UwpApp
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();

      ClearResult();
      ApplicationView.GetForCurrentView().TryResizeView(new Size(600.0, 480.0));
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      var picker = new Windows.Storage.Pickers.FileOpenPicker();
      picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
      picker.FileTypeFilter.Add(".jpg");
      picker.FileTypeFilter.Add(".jpeg");
      picker.FileTypeFilter.Add(".png");
      picker.FileTypeFilter.Add(".gif");
      picker.FileTypeFilter.Add(".bmp");

      Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
      if (file == null)
        return;

      ClearResult();

      // 選択された画像ファイルを読み込んでバイト配列に変換する
      byte[] data;
      using (var stream = await file.OpenStreamForReadAsync())
      {
        data = new byte[(int)stream.Length];
        stream.Read(data, 0, (int)stream.Length);
      }

      // バイト配列からSoftwareBitmapを作る
      // https://docs.microsoft.com/ja-jp/windows/uwp/audio-video-camera/imaging
      SoftwareBitmap softwareBitmap;
      using (IRandomAccessStream stream = new MemoryStream(data).AsRandomAccessStream())
      {
        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
        softwareBitmap = await decoder.GetSoftwareBitmapAsync();
      }
      // Image コントロールは、BGRA8 エンコードを使用し、プリマルチプライ処理済みまたはアルファ チャネルなしの画像しか受け取れない
      // 異なるフォーマットの場合は変換する☟
      if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8
          || softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
      {
        softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
      }

      // ImageコントロールにはSoftwareBitmapSourceを渡す
      var source = new SoftwareBitmapSource();
      await source.SetBitmapAsync(softwareBitmap);
      this.Image1.Source = source;

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
      this.BarcodeFormatText.Text = result.Format;
      this.TextText.Text = result.Text;
    }
  }
}
