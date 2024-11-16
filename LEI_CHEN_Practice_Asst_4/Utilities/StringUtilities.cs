using System.Text.RegularExpressions;

namespace Asst4.Utilities
{
  public static class StringUtilities
  {
    public static string PostalCode(string inputPostalCode)
    {
      if (string.IsNullOrWhiteSpace(inputPostalCode))
        return string.Empty;

      // Remove all spaces and convert to uppercase
      string cleaned = inputPostalCode.Trim().ToUpper().Replace(" ", "");

      // Validate the format A0A0A0
      var regex = new Regex(@"^[A-Z]\d[A-Z]\d[A-Z]\d$");
      if (regex.IsMatch(cleaned))
        return cleaned;

      return string.Empty; // Return empty if invalid format
    }
  }
}