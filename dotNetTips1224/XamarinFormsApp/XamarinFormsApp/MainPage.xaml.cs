using Plugin.FilePicker; // https://github.com/jfversluis/FilePicker-Plugin-for-Xamarin-and-Windows
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing; // BarcodeReaderExtensionsの拡張メソッドを利用

namespace XamarinFormsApp
{
  public partial class MainPage : ContentPage
  {
    private ZXing.BarcodeReader _reader
      = new BarcodeReader() { AutoRotate = true, Options = { TryHarder = true } };

    public MainPage()
    {
      InitializeComponent();
    }

    private async void Button_Click(object sender, EventArgs e)
    {
      var file = await CrossFilePicker.Current.PickFile();
      if (file == null)
        return;

      //var fn = file.FileName;
      byte[] fileData = file.DataArray;

      ClearResult();


      // 選択された画像ファイルを表示
      this.Image1.Source
        = ImageSource.FromStream(() => new MemoryStream(fileData, 0, fileData.Length));

      // バーコード読み取り
      // Xamarin.Formsでは色々な方法が考えられるが、ここではSkiaSharpを使う
      using (var stream = new MemoryStream(fileData, 0, fileData.Length))
      using (var managedStream = new SKManagedStream(stream))
      {
        SKBitmap bitmap = SKBitmap.Decode(managedStream);
        ZXing.Result result = _reader.Decode(bitmap);

        // ☝このDecodeメソッドは拡張メソッドとして実装されている
        // https://github.com/micjahn/ZXing.Net/blob/master/Source/Bindings/ZXing.SkiaSharp/BarcodeReader.Extensions.cs
        // ので、using ZXing; が必須。

        if (result != null)
          ShowResult(result);
      }
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
