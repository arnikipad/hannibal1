namespace SHARP
{
    internal struct CreditCardFormat
    {
      internal string Number;
      internal string ExpYear;
      internal string ExpMonth;
      internal string Name;

      internal CreditCardFormat(string number, string expyear, string expmonth, string name)
      {
        this.Name = name;
        this.Number = number;
        this.ExpYear = expyear;
        this.ExpMonth = expmonth;
      }
    }
}
