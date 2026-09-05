namespace SHARP
{
    internal struct CookieFormat
    {
      internal string Host;
      internal string Name;
      internal string Path;
      internal string Cookie;
      internal string Expiry;

      internal CookieFormat(string host, string name, string path, string cookie, string expiry)
      {
        this.Host = host;
        this.Name = name;
        this.Path = path;
        this.Cookie = cookie;
        this.Expiry = expiry;
      }
    }
}
