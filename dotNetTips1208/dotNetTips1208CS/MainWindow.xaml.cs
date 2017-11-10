using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNetTips1208CS
{
  /// <summary>
  /// MainWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Control_GotFocus(object sender, RoutedEventArgs e)
    {
      // フォーカスを受け取ったコントロールに結び付けられているデータを得る
      if (sender is Control ctl
          && ctl.DataContext is SampleData data)
      {
        // data（結び付けられているデータ）を使って何かする
        // 例：ListViewでこのデータを持っている項目を選択する
        //     ＝フォーカスを受け取ったコントロールを含む項目が選択される
        this.ListView1.SelectedItem = data;

        // ListView内でのインデックスを得る
        if (this.ListView1.ItemsSource is IList<SampleData> list)
        {
          int index = list.IndexOf(data);

          // index（ListView内でのインデックス）を使って何かする
          Run1.Text = index.ToString();
        }
      }
    }
  }
}
