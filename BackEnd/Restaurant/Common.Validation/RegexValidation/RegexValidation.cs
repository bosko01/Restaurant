namespace Common.Validation.RegexValidation
{
    public static class RegexValidation
    {
        public const string Email = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string NumericsOnly = "[^0-9]";
        public const string UserType = "^(Admin|User|Guest)$";
        public const string Password = "\"^((?=\\S*?[A-Z])(?=\\S*?[a-z])(?=\\S*?[0-9]).{6,})\\S$\"";
        public const string PhoneNumber = @"^(?:\+381)?\s?\(?\d{2}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
    }
}