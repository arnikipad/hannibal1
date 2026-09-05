using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SHARP
{
    internal class IP
    {
      public static async Task ip() => Counting.country = IP.GetLocationInfo(Help.IP).Country;

      private static LocationInfo GetLocationInfo(string ipAddress)
      {
        try
        {
          WebRequest webRequest = WebRequest.Create("http://ip-api.com/json/" + ipAddress);
          webRequest.Method = "GET";
          using (WebResponse response = webRequest.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              using (StreamReader streamReader = new StreamReader(responseStream))
              {
                string[] strArray1 = streamReader.ReadToEnd().Split(',');
                LocationInfo locationInfo = new LocationInfo();
                foreach (string str1 in strArray1)
                {
                  char[] chArray = new char[1]{ ':' };
                  string[] strArray2 = str1.Split(chArray);
                  string str2 = strArray2[0].Trim().TrimStart('"').TrimEnd('"');
                  string str3 = strArray2[1].Trim().TrimStart('"').TrimEnd('"');
                  if (str2 == "country")
                    locationInfo.Country = str3;
                }
                return locationInfo;
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("Ошибка при получении данных: " + ex.Message);
        }
        return (LocationInfo) null;
      }
    }
}
