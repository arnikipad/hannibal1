using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace SHARP
{
    internal class BSSID
    {
      [DllImport("iphlpapi.dll")]
      private static extern int SendARP(
        int destIp,
        int srcIP,
        byte[] macAddr,
        ref uint physicalAddrLen);

      public static string GetBSSID()
      {
        byte[] macAddr = new byte[6];
        uint length = (uint) macAddr.Length;
        try
        {
          if (BSSID.SendARP(BitConverter.ToInt32(IPAddress.Parse(BSSID.GetDefaultGateway()).GetAddressBytes(), 0), 0, macAddr, ref length) != 0)
            return "unknown";
          string[] strArray = new string[(int) length];
          for (int index = 0; (long) index < (long) length; ++index)
            strArray[index] = macAddr[index].ToString("x2");
          return string.Join(":", strArray);
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        return "Failed";
      }

      public static string GetDefaultGateway()
      {
        try
        {
          return ((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces()).Where<NetworkInterface>((Func<NetworkInterface, bool>) (n => n.OperationalStatus == OperationalStatus.Up)).Where<NetworkInterface>((Func<NetworkInterface, bool>) (n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)).SelectMany<NetworkInterface, GatewayIPAddressInformation>((Func<NetworkInterface, IEnumerable<GatewayIPAddressInformation>>) (n =>
          {
            IPInterfaceProperties ipProperties = n.GetIPProperties();
            return ipProperties == null ? (IEnumerable<GatewayIPAddressInformation>) null : (IEnumerable<GatewayIPAddressInformation>) ipProperties.GatewayAddresses;
          })).Select<GatewayIPAddressInformation, IPAddress>((Func<GatewayIPAddressInformation, IPAddress>) (g => g?.Address)).Where<IPAddress>((Func<IPAddress, bool>) (a => a != null)).FirstOrDefault<IPAddress>().ToString();
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        return "Unknown";
      }
    }
}
