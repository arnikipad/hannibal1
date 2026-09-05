using System.Threading;

namespace SHARP
{
    internal class Clipboard
    {
      public static string GetText()
      {
        string ReturnValue = string.Empty;
        try
        {
          Thread thread = new Thread((ThreadStart) (() => ReturnValue = System.Windows.Forms.Clipboard.GetText()));
          thread.SetApartmentState(ApartmentState.STA);
          thread.Start();
          thread.Join();
        }
        catch
        {
        }
        return ReturnValue;
      }

      public static void SetText(string text)
      {
        Thread thread = new Thread((ThreadStart) (() =>
        {
          try
          {
            System.Windows.Forms.Clipboard.SetText(text);
          }
          catch
          {
          }
        }));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
      }
    }
}
