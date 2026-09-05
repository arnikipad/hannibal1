using System.IO;

namespace SHARP
{
    internal sealed class Filemanager
    {
      public static void CopyDirectory(string sourceDir, string targetDir)
      {
        if (!Directory.Exists(sourceDir))
          throw new DirectoryNotFoundException(sourceDir);
        if (!Directory.Exists(targetDir))
          Directory.CreateDirectory(targetDir);
        foreach (string enumerateDirectory in Directory.EnumerateDirectories(sourceDir))
        {
          string targetDir1 = Path.Combine(targetDir, Path.GetFileName(enumerateDirectory));
          Filemanager.CopyDirectory(enumerateDirectory, targetDir1);
        }
        foreach (string enumerateFile in Directory.EnumerateFiles(sourceDir))
        {
          string destFileName = Path.Combine(targetDir, Path.GetFileName(enumerateFile));
          File.Copy(enumerateFile, destFileName, true);
        }
      }
    }
}
