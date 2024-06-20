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

        public static bool IsValid(string mailAddress)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(mailAddress))
                return false;

            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(mailAddress);
        }
    }
}