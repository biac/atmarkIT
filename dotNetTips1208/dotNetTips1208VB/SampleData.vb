Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class BindableBase
  Implements INotifyPropertyChanged

  Public Event PropertyChanged As PropertyChangedEventHandler _
    Implements INotifyPropertyChanged.PropertyChanged

  Overridable Sub OnPropertyChanged(propertyName As String)
    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
  End Sub

  Overridable Sub SetProperty(Of T)(ByRef storage As T, value As T,
                                <CallerMemberName> Optional propertyName As String = Nothing)
    If (Object.Equals(storage, value)) Then
      Return
    End If
    storage = value
    OnPropertyChanged(propertyName)
  End Sub
End Class

Public Class SampleData
  Inherits BindableBase

  Public Sub New(id As String, Optional title As String = Nothing)
    Me.Id = id
    Me.Title = title
  End Sub

  ' 不変のプロパティ
  Public ReadOnly Property Id As String

  ' 変更可能なプロパティ
  Private _title As String
  Public Property Title As String
    Get
      Return _title
    End Get
    Set(value As String)
      SetProperty(_title, value)
    End Set
  End Property
End Class

Public Class SampleDataList

  '画面にバインドするコレクション
  Public ReadOnly Property List As ObservableCollection(Of SampleData) _
      = New ObservableCollection(Of SampleData)

#If DEBUG Then
  Public Sub New()
    List.Add(New SampleData("001", "時間のかかる処理をバックグラウンドで実行するには？"))
    List.Add(New SampleData("002", "DataGridやListViewなどに表示しているデータを別スレッドから変更するには？"))
    List.Add(New SampleData("003", "ラジオボタンの選択をバインディングソースに反映させるには？"))
    List.Add(New SampleData("004", "ラジオボタンを双方向バインディングするには？"))
    List.Add(New SampleData("005", "テキストブロックの一部分だけをデータバインディングするには？"))
  End Sub
#End If
End Class