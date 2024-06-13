using Common.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Email
    {
        public string mailAddress;

        private Email(string mailAddress)
        {
            this.mailAddress = mailAddress;
        }

        public static Email Create(string mailAddress)
        {
            if (Email.IsValid(mailAddress))
            {
                return new Email(mailAddress);
            }

            throw new BussinessRuleValidationExeption("Email is required");
        }

        private static bool IsValid(string mailAddress)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(mailAddress, regex, RegexOptions.IgnoreCase);
        }
    }
}