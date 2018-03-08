using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.IO;
using System.Windows.Threading;

namespace WpfAppCS
{
  /// <summary>
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {

    // WPFではZXing.Presentation名前空間のBarcodeReaderを使う
    private ZXing.Presentation.BarcodeReader _reader
      = new ZXing.Presentation.BarcodeReader() { AutoRotate=true,};

    public MainWindow()
    {
      InitializeComponent();

      ClearResult();
      this.Image1.SizeChanged += (s, e) => AdjustOverlay();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new OpenFileDialog();
      dialog.Title = "バーコードの写った画像ファイルを開く";
      dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
      if (dialog.ShowDialog() == true)
      {
        ClearResult();
        DoEvents();

        // 選択された画像ファイルを表示
        var source = new BitmapImage(new Uri(dialog.FileName));
        this.Image1.Source = source;

        // バーコード読み取り
        // WPFではBitmapImageかBitmapSourceを渡す
        ZXing.Result result = _reader.Decode(Image1.Source as BitmapSource);
        // ☟別スレッドでやるなら、作成済みのBitmapImageインスタンスは渡せない
        //ZXing.Result result 
        //  = await Task.Run(()=> _reader.Decode(new BitmapImage(new Uri(dialog.FileName))));
        if (result != null)
          ShowResult(result);
      }
    }

    private void ClearResult()
    {
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
      this.Polygon1.RenderTransform = new RotateTransform( orientation,bs.PixelWidth/2.0, bs.PixelHeight/2.0);

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




    //private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
    //{
    //  // Stride = (width) x (bytes per pixel)
    //  int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel+7) / 8;
    //  byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];

    //  bitmapSource.CopyPixels(pixels, stride, 0);

    //  return pixels;
    //}


    //private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
    //{
    //  // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

    //  using (MemoryStream outStream = new MemoryStream())
    //  {
    //    BitmapEncoder enc = new BmpBitmapEncoder();
    //    enc.Frames.Add(BitmapFrame.Create(bitmapImage));
    //    enc.Save(outStream);
    //    Bitmap bitmap = new Bitmap(outStream);

    //    return new Bitmap(bitmap);
    //  }
    //}


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
