﻿using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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