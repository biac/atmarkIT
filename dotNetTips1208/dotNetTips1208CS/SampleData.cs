using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dotNetTips1208CS
{
  // 特集：C# 7の新機能詳説：第2回 簡潔なコーディングのために(1/2)
  // http://www.atmarkit.co.jp/ait/articles/1707/26/news017.html
  public abstract class BindableBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void SetProperty<T>(ref T storage, T value,
                                [CallerMemberName] string propertyName = null)
    {
      if (object.Equals(storage, value))
        return;
      storage = value;
      OnPropertyChanged(propertyName);
    }
  }

  public class SampleData : BindableBase
  {
    public SampleData(string id, string title = null)
    {
      this.Id = id;
      this.Title = title;
    }

    // 不変のプロパティ
    public string Id { get; }

    // 変更可能なプロパティ
    private string _title;
    public string Title
    {
      get => _title;
      set => SetProperty(ref _title, value);
    }
  }

  public class SampleDataList
  {
    // 画面にバインドするコレクション
    public ObservableCollection<SampleData> List { get; }
      = new ObservableCollection<SampleData>();

#if DEBUG
    public SampleDataList()
    {
      List.Add(new SampleData("001", "時間のかかる処理をバックグラウンドで実行するには？"));
      List.Add(new SampleData("002", "DataGridやListViewなどに表示しているデータを別スレッドから変更するには？"));
      List.Add(new SampleData("003", "ラジオボタンの選択をバインディングソースに反映させるには？"));
      List.Add(new SampleData("004", "ラジオボタンを双方向バインディングするには？"));
      List.Add(new SampleData("005", "テキストブロックの一部分だけをデータバインディングするには？"));
    }
#endif
  }

}
