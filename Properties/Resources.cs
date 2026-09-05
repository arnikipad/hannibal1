using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SHARP.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal Resources()
      {
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (SHARP.Properties.Resources.resourceMan == null)
            SHARP.Properties.Resources.resourceMan = new ResourceManager("SHARP.Properties.Resources", typeof (SHARP.Properties.Resources).Assembly);
          return SHARP.Properties.Resources.resourceMan;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => SHARP.Properties.Resources.resourceCulture;
        set => SHARP.Properties.Resources.resourceCulture = value;
      }
    }
}
