using System.Reflection;
using System.Runtime.Serialization;

namespace HRAAB_Management.Business.Helpers
{
    public static class EnumHelper
    {
        public static T ParseFromEnumMember<T>(string value) where T : Enum
        {
            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                EnumMemberAttribute enumMemberAttribute = field.GetCustomAttribute<EnumMemberAttribute>()
                    ?? throw new ArgumentException("Enum type not found!");
                if (enumMemberAttribute != null && enumMemberAttribute.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException($"No matching EnumMember found for value '{value}' in enum {typeof(T)}");
        }
    }
}
