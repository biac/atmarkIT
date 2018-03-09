using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private async void Button_Click(object sender, EventArgs e)
    {
      var dialog = new OpenFileDialog();
      dialog.Title = "バーコードの写った画像ファイルを開く";
      dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
      if (dialog.ShowDialog() != DialogResult.OK)
        return;

      ClearResult();
      Application.DoEvents();

      // 選択された画像ファイルを表示
      var source = new Bitmap(dialog.FileName);
      this.Image1.Image = source;

      // 表示している画像をバイト配列に変換
      ImageConverter converter = new ImageConverter();
      var bytes = (byte[])converter.ConvertTo(this.Image1.Image, typeof(byte[]));

      // バーコード読み取り
      var result = await StdLib.ZXingBarcodeReader.DecodeAsync(bytes);
      if (result != null)
        ShowResult(result);
    }

    private void ClearResult()
    {
      this.Image1.Image = null;
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
