using Common.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber
    {
        private string CountryCode { get; set; }

        private string Number { get; set; }

        private PhoneNumber(string countryCode, string number)
        {
            CountryCode = countryCode;
            Number = number;
        }

        public static PhoneNumber Create(string countryCode, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new BussinessRuleValidationExeption("Country Code is required");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new BussinessRuleValidationExeption("Phone number is required");
            }

            if (PhoneNumber.IsValid(countryCode, phoneNumber))
            {
                return new PhoneNumber(countryCode, phoneNumber);
            }

            throw new BussinessRuleValidationExeption("Country Code or Phone number is not valid");
        }

        private static bool IsValid(string countryCode, string phoneNumber)
        {
            string temp = countryCode + phoneNumber;

            return Regex.Match(temp.Trim(), @"^(\+[0-9]{12})$").Success;
        }
    }
}