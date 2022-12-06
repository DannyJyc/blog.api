using System.Reflection;

namespace Blog.API.Dynamic.Api.Core.Helper
{
    public class AttributeHelper
    {
        /// <summary>
        /// https://github.com/pda-team/Panda.DynamicWebApi/blob/master/src/Panda.DynamicWebApi/Helpers/ReflectionHelper.cs#L35
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="defaultValue"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
       where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(attributeType, inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }
    }
}
