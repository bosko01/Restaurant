using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class BussinessRuleValidationExeption : CustomException
    {
        public BussinessRuleValidationExeption(string message) : base(message)
        {
        }
    }
}