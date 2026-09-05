using System;
using System.Runtime.InteropServices;

namespace SHARP
{
    internal class OMethod
    {
      [ComVisible(true)]
      [Serializable]
      public enum OMethods
      {
        TelegramBot,
        MyPrivateServer,
      }
    }
}
