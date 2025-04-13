using System.Reflection;
using System.Runtime.Serialization;

namespace HRAAB_Management.Business.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the string value associated with the <see cref="EnumMemberAttribute"/>
        /// of an enum value, if present. If no attribute is found, returns the enum's name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string ToEnumMemberValue<T>(this T enumValue) where T : Enum
        {
            Type type = typeof(T);
            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                EnumMemberAttribute? attribute = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>(false);
                if (attribute != null)
                {
                    return attribute.Value ?? string.Empty;
                }
            }

            return enumValue.ToString();
        }
    }
}
