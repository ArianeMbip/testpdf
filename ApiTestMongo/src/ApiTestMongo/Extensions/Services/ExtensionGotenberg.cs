using ApiTestMongo.Gotenberg.Resources;
using System.ComponentModel;
using System.Globalization;

namespace ApiTestMongo.Extensions.Services
{
    public static class ExtensionGotenberg
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }
        /// <summary>
        /// Get width and Height of a Paper
        /// </summary>
        /// <param name="paperSizeEnum"></param>
        /// <returns></returns>
        public static (string, string) GetSize(this PaperSizeEnum paperSizeEnum)
        {
            var description = paperSizeEnum.GetDescription();
            var chunks = description.Split("x");

            return (chunks[0], chunks[1]);
        }

        public static string ToLowerFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input[1..];
        }


    }
}
