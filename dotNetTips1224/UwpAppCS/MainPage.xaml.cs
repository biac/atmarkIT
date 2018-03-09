/*
This software includes the work that is distributed in the Apache License 2.0 

ZXing.Net
Copyright © 2012-2017 Michael Jahn https://github.com/micjahn/ZXing.Net

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace UwpAppCS
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

      // 選択された画像ファイルからSoftwareBitmapを作る
      // https://docs.microsoft.com/ja-jp/windows/uwp/audio-video-camera/imaging
      SoftwareBitmap softwareBitmap;
      using (IRandomAccessStream stream = await file.OpenReadAsync())
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
      ZXing.BarcodeReader _reader
        = new ZXing.BarcodeReader
        {
          AutoRotate = true,
          Options = { TryHarder = true }
        };
      // UWPではSoftwareBitmapかWriteableBitmapを渡す
      //ZXing.Result result = _reader.Decode(softwareBitmap);
      // ☟別スレッドでやるときも、作成済みのSoftwareBitmapインスタンスを渡してよい
      ZXing.Result result
            = await Task.Run(() => _reader.Decode(softwareBitmap));
      if (result != null)
        ShowResult(result);
    }

    private void ClearResult()
    {
      this.Image1.Source = null;
      this.BarcodeFormatText.Text = "(N/A)";
      this.TextText.Text = "(N/A)";
    }

    private void ShowResult(ZXing.Result result)
    {
      this.BarcodeFormatText.Text = result.BarcodeFormat.ToString();
      this.TextText.Text = result.Text;
    }
  }
}
