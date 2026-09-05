namespace SHARP
{
    internal struct PasswordFormat
    {
      internal readonly string Username;
      internal readonly string Password;
      internal readonly string Url;

      internal PasswordFormat(string username, string password, string url)
      {
        this.Username = username;
        this.Password = password;
        this.Url = url;
      }
    }
}
