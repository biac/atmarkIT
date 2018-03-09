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


using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfAppCS
{
  /// <summary>
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      ClearResult();
      this.SizeChanged += (s, e) => AdjustOverlay();
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

      // バーコード読み取り
      // WPFではZXing.Presentation名前空間のBarcodeReaderを使う
      ZXing.Presentation.BarcodeReader reader
        = new ZXing.Presentation.BarcodeReader()
              {
                AutoRotate = true,
                Options = { TryHarder = true },
              };

      // WPFではBitmapImageかBitmapSourceを渡す
      //ZXing.Result result = reader.Decode(Image1.Source as BitmapImage);
      // ☟別スレッドでやるなら、作成済みのBitmapImageインスタンスは渡せない
      ZXing.Result result
        = await Task.Run(() => reader.Decode(new BitmapImage(new Uri(dialog.FileName))));
      if (result != null)
        ShowResult(result);
    }

    private void ClearResult()
    {
      this.Image1.Source = null;
      this.BarcodeFormatText.Text = "(N/A)";
      this.TextText.Text = "(N/A)";
      this.OverlayCanvas.Visibility = Visibility.Collapsed;
    }

    private void ShowResult(ZXing.Result result)
    {
      // テキスト表示
      this.BarcodeFormatText.Text = result.BarcodeFormat.ToString();
      this.TextText.Text = result.Text;

      // 認識エリア表示
      ZXing.ResultPoint[] points = result.ResultPoints;
      this.Polygon1.BeginInit();
      {
        this.Polygon1.Points.Clear();
        foreach (var p in points)
          this.Polygon1.Points.Add(new Point(p.X, p.Y));
      }
      this.Polygon1.EndInit();

      // 回転
      int orientation
        = (int)result.ResultMetadata[ZXing.ResultMetadataType.ORIENTATION];
      switch (orientation)
      {
        case 180:
          orientation = 0;
          break;
        case 270:
          orientation = 90;
          break;
      }
      BitmapSource bs = this.Image1.Source as BitmapSource;
      this.Polygon1.RenderTransform = new RotateTransform(orientation, bs.PixelWidth / 2.0, bs.PixelHeight / 2.0);

      this.OverlayCanvas.Visibility = Visibility.Visible;
      AdjustOverlay();
    }

    // 認識エリアを表示しているCanvasの位置とサイズを調整
    private void AdjustOverlay()
    {
      if (!this.OverlayCanvas.IsVisible)
        return;

      if (this.Image1.Source is BitmapSource bs)
      {
        Point imagePosition
          = this.Image1.TransformToAncestor(this.ImageGrid)
                       .Transform(new Point(0, 0));
        this.OverlayCanvas.Margin
          = new Thickness(imagePosition.X, imagePosition.Y, 0.0, 0.0);

        double scale = this.Image1.RenderSize.Width / bs.PixelWidth;
        this.OverlayCanvas.RenderTransform = new ScaleTransform(scale, scale);
      }
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
