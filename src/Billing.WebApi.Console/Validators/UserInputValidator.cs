using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Billing.WebApi.Console.Validators
{
    public class UserInputValidator
    {
        public static bool IsValidNumber(string value, int minBound, int maxBound, out int parsedInt) =>
            int.TryParse(value, out parsedInt) && minBound <= parsedInt && parsedInt <= maxBound;

        public static bool IsValidDateInFormat(string input, string format, out DateTime parsedDate) 
            => DateTime.TryParseExact(input, format, 
                CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, 
                out parsedDate);

        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            try
            {
                return Regex.IsMatch(phone,
                    @"^((\+7|7|8)+\(([0-9]){3,4})\)[0-9]{3}-[0-9]{2}-[0-9]{2}$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
