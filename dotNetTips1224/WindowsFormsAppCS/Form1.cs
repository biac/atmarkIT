using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppCS
{
  public partial class Form1 : Form
  {
    private ZXing.BarcodeReader _reader = new ZXing.BarcodeReader() { AutoRotate=true,};

    public Form1()
    {
      InitializeComponent();
    }

    private async void Button_Click(object sender, EventArgs e)
    {
      var dialog = new OpenFileDialog();
      dialog.Title = "バーコードの写った画像ファイルを開く";
      dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        ClearResult();
        Application.DoEvents();

        // 選択された画像ファイルを表示
        var source = new Bitmap(dialog.FileName);
        this.Image1.Image = source;

        // バーコード読み取り
        // Windows FormsではBitmapを渡す
        //ZXing.Result result = _reader.Decode(Image1.Image as Bitmap);
        // ☟別スレッドでやるなら、作成済みのBitmapインスタンスは渡せない
        ZXing.Result result
          = await Task.Run(() => _reader.Decode(new Bitmap(dialog.FileName)));
        if (result != null)
          ShowResult(result);
      }
    }

    private void ClearResult()
    {
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
