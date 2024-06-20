using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Converters
{
    public class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter() : base(
            email => email.mailAddress,
            address => Email.Create(address))
        {
        }
    }
}