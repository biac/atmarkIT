Class MainWindow
  Private Sub Control_GotFocus(sender As Object, e As RoutedEventArgs)

    'フォーカスを受け取ったコントロールに結び付けられているデータを得る
    Dim ctl = TryCast(sender, Control)
    Dim data = TryCast(ctl?.DataContext, SampleData)
    If (data IsNot Nothing) Then
      ' data（結び付けられているデータ）を使って何かする
      ' 例：ListViewでこのデータを持っている項目を選択する
      '     =フォーカスを受け取ったコントロールを含む項目が選択される
      Me.ListView1.SelectedItem = data

      'ListView内でのインデックスを得る
      Dim list = TryCast(ListView1.ItemsSource, IList(Of SampleData))
      If (list IsNot Nothing) Then
        Dim index As Integer = list.IndexOf(data)

        ' index（ListView内でのインデックス）を使って何かする
        Run1.Text = index.ToString()
      End If
    End If
  End Sub
End Class
