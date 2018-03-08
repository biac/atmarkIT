Imports System.Windows.Threading
Imports Microsoft.Win32

Class MainWindow

  ' WPFではZXing.Presentation名前空間のBarcodeReaderを使う
  Private _reader As ZXing.Presentation.BarcodeReader _
      = New ZXing.Presentation.BarcodeReader() With {.AutoRotate = True}

  Public Sub New()

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()

    ' InitializeComponent() 呼び出しの後で初期化を追加します。
    ClearResult()
  End Sub

  Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
    Dim dialog = New OpenFileDialog()
    dialog.Title = "バーコードの写った画像ファイルを開く"
    dialog.Filter = "画像ファイル(*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp"
    If (dialog.ShowDialog() = True) Then
      ClearResult()
      DoEvents()

      ' 選択された画像ファイルを表示
      Dim source = New BitmapImage(New Uri(dialog.FileName))
      Me.Image1.Source = source

      ' バーコード読み取り
      ' WPFではBitmapImageかBitmapSourceを渡す
      Dim result As ZXing.Result = _reader.Decode(TryCast(Image1.Source, BitmapSource))
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

  Private Sub DoEvents()
    Dim frame As DispatcherFrame = New DispatcherFrame()
    Dim callback = New DispatcherOperationCallback(
      Function(obj)
        CType(obj, DispatcherFrame).Continue = False
        Return Nothing
      End Function)
    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame)
    Dispatcher.PushFrame(frame)
  End Sub

End Class
