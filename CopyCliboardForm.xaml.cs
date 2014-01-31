using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clipboard
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class CopyCliboardForm : Window
  {
    Config _cfg;//CopyCliboardForm.xaml
    public CopyCliboardForm()
    {
      InitializeComponent();
      _cfg = Config.Load();
      DataContext = _cfg.Items;
    }

    private void ButtonAdd_Click(object sender, RoutedEventArgs e)
    {
      var item = new Config.Item()
      {
        Name = _tbName.Text,
        Content = _tbContent.Text
      };
      _cfg.Items.Add(item);
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var item = _lbItems.SelectedItem as Config.Item;
      //System.Windows.Forms.Clipboard.SetText("");
      if (item == null) return;
      System.Windows.Forms.Clipboard.SetText(item.Content);
    }

    private void _this_Closed(object sender, EventArgs e)
    {
      _cfg.Save();
    }
  }
}
