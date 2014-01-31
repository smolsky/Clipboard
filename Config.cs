using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Clipboard
{
  
  public class Config
  {
    public class Item
    {
      public string Name { get; set; }
      public string Content { get; set; }
      public override string ToString()
      {
        var format = "{0}: {1}";
        if( String.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Content) )
          format = "{0}{1}";
        var res = String.Format(format, Name, Content);
        if (String.IsNullOrEmpty(res))
          return "Empty";
        return res;
      }
    };

    public ObservableCollection<Item> Items { get; set; }

    private Config() 
    {
      Items = new ObservableCollection<Item>();
    }

    private static String FilePath
    {
      get
      {
        return "Config.xml";
      }
    }
    public void Save()
    {
      using (var fs = new FileStream(FilePath, FileMode.OpenOrCreate))
      {
        var xmlSerializer = new XmlSerializer(typeof(Config));

        xmlSerializer.Serialize(fs, this);

        fs.Flush();
        fs.Close();
      }
    }

    public static Config Load()
    {
      if (!File.Exists(FilePath))
        return new Config();
      try
      {
        using (var fs = new FileStream(FilePath, FileMode.Open))
        {
          var xmlSerializer = new XmlSerializer(typeof(Config));

          var cfg = xmlSerializer.Deserialize(fs) as Config;

          fs.Close();
          return cfg;
        }
      }
      catch (Exception)
      {
        return new Config();
      }
    }
  }
}
