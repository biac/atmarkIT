' This software includes the work that is distributed in the Apache License 2.0 
' 
' ZXing.Net
' Copyright © 2012-2017 Michael Jahn https://github.com/micjahn/ZXing.Net
' 
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
' 
'     http://www.apache.org/licenses/LICENSE-2.0
' 
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Public Class Form1

  Private Sub Button_Click(sender As Object, e As EventArgs)
    Dim dialog = New OpenFileDialog()
    dialog.Title = "バーコードの写った画像ファイルを開く"
    dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp"
    If (dialog.ShowDialog() <> DialogResult.OK) Then
      Return
    End If

    ClearResult()
    Application.DoEvents()

    ' 選択された画像ファイルを表示
    Dim source = New Bitmap(dialog.FileName)
    Me.Image1.Image = source

    ' バーコード読み取り
    Dim reader As ZXing.BarcodeReader = New ZXing.BarcodeReader() With {.AutoRotate = True}
    ' Windows FormsではBitmapを渡す
    Dim result As ZXing.Result = reader.Decode(TryCast(Image1.Image, Bitmap))
    If (result IsNot Nothing) Then
      ShowResult(result)
    End If
  End Sub

  Private Sub ClearResult()
    Me.Image1.Image = Nothing
    Me.BarcodeFormatText.Text = "(N/A)"
    Me.TextText.Text = "(N/A)"
  End Sub

  Private Sub ShowResult(result As ZXing.Result)
    Me.BarcodeFormatText.Text = result.BarcodeFormat.ToString()
    Me.TextText.Text = result.Text
  End Sub

End Class
