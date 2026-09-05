using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARP
{
    internal class Screenchik
    {
      public static async Task GetScreen(string SDir)
      {
        Rectangle bounds = Screen.PrimaryScreen.Bounds;
        int width = bounds.Width;
        bounds = Screen.PrimaryScreen.Bounds;
        int height = bounds.Height;
        Bitmap bitmap = new Bitmap(width, height);
        Graphics.FromImage((Image) bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
        bitmap.Save(SDir + "\\$creen.jpeg", ImageFormat.Jpeg);
      }
    }
}
