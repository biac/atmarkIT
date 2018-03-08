Public Class Form1

  Private _reader As ZXing.BarcodeReader = New ZXing.BarcodeReader() With {.AutoRotate = True}



  Private Sub Button_Click(sender As Object, e As EventArgs)
    Dim dialog = New OpenFileDialog()
    dialog.Title = "バーコードの写った画像ファイルを開く"
    dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp"
    If (dialog.ShowDialog() = DialogResult.OK) Then
      ClearResult()
      Application.DoEvents()

      ' 選択された画像ファイルを表示
      Dim source = New Bitmap(dialog.FileName)
      Me.Image1.Image = source

      ' バーコード読み取り
      ' Windows FormsではBitmapを渡す
      Dim result As ZXing.Result = _reader.Decode(TryCast(Image1.Image, Bitmap))
      If (result IsNot Nothing) Then
        ShowResult(result)
      End If
    End If
  End Sub

  Private Sub ClearResult()
    Me.BarcodeFormatText.Text = "(N/A)"
    Me.TextText.Text = "(N/A)"
  End Sub

  Private Sub ShowResult(result As ZXing.Result)
    Me.BarcodeFormatText.Text = result.BarcodeFormat.ToString()
    Me.TextText.Text = result.Text
  End Sub

End Class
