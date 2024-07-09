using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class PhoneConverter : ValueConverter<PhoneNumber, string>
    {
        public PhoneConverter() : base(
             phoneNumber => phoneNumber.ToString(),
             fullNumber => PhoneNumber.Parse(fullNumber))
        {
        }
    }
}