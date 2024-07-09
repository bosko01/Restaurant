using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions.RegexValidation
{
    public static class RegexValidation
    {
        public const string Email = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string NumericsOnly = "[^0-9]";
        public const string UserType = "^(Admin|User|Guest)$";
        public const string Password = "\"^((?=\\S*?[A-Z])(?=\\S*?[a-z])(?=\\S*?[0-9]).{6,})\\S$\"";
    }
}