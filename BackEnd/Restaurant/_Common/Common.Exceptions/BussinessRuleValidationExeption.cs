namespace Common.Exceptions
{
    public class BussinessRuleValidationExeption : CustomException
    {
        public BussinessRuleValidationExeption(string message) : base(message)
        {
        }
    }
}