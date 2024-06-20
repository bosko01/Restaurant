using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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