using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinStdApp
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private async void Button_Click(object sender, EventArgs e)
    {
      var file = await CrossFilePicker.Current.PickFile();
      if (file == null)
        return;

      ClearResult();

      // 選択された画像ファイルを表示
      byte[] fileData = file.DataArray;
      this.Image1.Source
        = ImageSource.FromStream(() => new MemoryStream(fileData, 0, fileData.Length));

      // バーコード読み取り
      var result = await StdLib.ZXingBarcodeReader.DecodeAsync(fileData);
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
