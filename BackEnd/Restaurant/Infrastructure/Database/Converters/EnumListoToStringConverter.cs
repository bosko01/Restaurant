using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class EnumListToStringConverter<TEnum> : ValueConverter<List<TEnum>, string> where TEnum : struct, Enum
    {
        public EnumListToStringConverter() : base(
            e => string.Join(',', e.Select(e => e.ToString())), // Enum to string
            s => ConvertToEnumList(s))
        {
        }

        private static List<TEnum> ConvertToEnumList(string s)
        {
            var values = s.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var enumList = new List<TEnum>();
            foreach (var value in values)
            {
                if (Enum.TryParse(typeof(TEnum), value, out var result) && result is TEnum enumValue)
                {
                    enumList.Add(enumValue);
                }
                else
                {
                    throw new ArgumentException($"Invalid value '{value}' for enum type '{typeof(TEnum).Name}'.");
                }
            }

            return enumList;
        }
    }
}