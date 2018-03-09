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
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppCS
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

      // バーコード読み取り
      ZXing.BarcodeReader reader = new ZXing.BarcodeReader() { AutoRotate = true, };
      // Windows FormsではBitmapを渡す
      //ZXing.Result result = reader.Decode(Image1.Image as Bitmap);
      // ☟別スレッドでやるなら、作成済みのBitmapインスタンスは渡せない
      ZXing.Result result
            = await Task.Run(() => reader.Decode(new Bitmap(dialog.FileName)));
      if (result != null)
        ShowResult(result);
    }

    private void ClearResult()
    {
      this.Image1.Image = null;
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
