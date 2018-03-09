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
/*
This software includes the work that is distributed in the MIT License

SkiaSharp
Copyright © 2016-2018 Xamarin Inc. https://github.com/mono/SkiaSharp

FilePicker Plugin for Xamarin and Windows
Copyright ©2016-2018 Gerald Versluis, rafaelrmou
https://github.com/jfversluis/FilePicker-Plugin-for-Xamarin-and-Windows
Copyright ©2016 Rafael Moura
https://github.com/Studyxnet/FilePicker-Plugin-for-Xamarin-and-Windows/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
https://opensource.org/licenses/mit-license.php
*/


using Plugin.FilePicker;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing; // BarcodeReaderExtensionsの拡張メソッドを利用

namespace XamarinFormsApp
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
      // Xamarin.Formsでは色々な方法が考えられるが、ここではSkiaSharpを使う
      using (var stream = new MemoryStream(fileData, 0, fileData.Length))
      using (var managedStream = new SKManagedStream(stream))
      {
        SKBitmap bitmap = SKBitmap.Decode(managedStream);

        ZXing.BarcodeReader reader = new BarcodeReader()
        {
          AutoRotate = true,
          Options = { TryHarder = true },
        };
        //ZXing.Result result = reader.Decode(bitmap);
        // ☟別スレッドでやるときも、UIスレッドで作成したSKBitmapインスタンスを渡してよい
        ZXing.Result result = await Task.Run(() => reader.Decode(bitmap));

        // 注意：☝このDecodeメソッドは拡張メソッドとして実装されている
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
