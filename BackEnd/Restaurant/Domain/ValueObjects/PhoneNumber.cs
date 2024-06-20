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
            string temp = countryCode.Replace(" ", "") + " " + phoneNumber.Replace(" ", "");

            return Regex.Match(temp.Trim(), @"^(?:\+381)?\s?\(?\d{2}\)?[-.\s]?\d{3}[-.\s]?\d{4}$").Success;
        }

        public static PhoneNumber Parse(string fullNumber)
        {
            // Split the full number on the first space
            var splitIndex = fullNumber.IndexOf(' ');
            if (splitIndex == -1)
            {
                throw new ArgumentException("Invalid full number format. Expected format 'CountryCode + Empty_space + Number'.");
            }

            var countryCode = fullNumber.Substring(0, splitIndex);
            var number = fullNumber.Substring(splitIndex + 1);

            return new PhoneNumber(countryCode, number);
        }

        public override string ToString()
        {
            return $"{CountryCode} {Number}";
        }
    }
}