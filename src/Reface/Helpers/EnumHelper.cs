using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reface
{
    public class EnumHelper
    {
        public static FieldInfo[] GetItems<T>()
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                throw CanNotGetEnumItemException.CreateNotEnumType();

            string[] names = Enum.GetNames(type);
            FieldInfo[] result = new FieldInfo[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                FieldInfo field = type.GetField(name);
                result[i] = field;
            }
            return result;
        }

        public static List<FieldInfo> GetItemsByAttribute<TEnum, TAttribute>()
            where TAttribute : Attribute
        {
            FieldInfo[] fieldInfos = EnumHelper.GetItems<TEnum>();
            List<FieldInfo> result = new List<FieldInfo>();
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.GetCustomAttribute<TAttribute>() == null) continue;
                result.Add(fieldInfo);
            }
            return result;
        }

        public static bool HasFlag<T>(T value, T flag)
        {
            if (!typeof(T).IsEnum)
                return false;
            int i_value = ConvertToInt32(value);
            int i_flag = ConvertToInt32(flag);
            return (i_value & i_flag) == i_flag;
        }

        public static T RemoveFlag<T>(T value, T flag)
        {
            if (!typeof(T).IsEnum)
                throw new InvalidCastException("T 必须是枚举类型");
            int i_value = ConvertToInt32(value);
            int i_flag = ConvertToInt32(flag);
            int i_result = i_value - (i_value & i_flag);
            return (T)Enum.ToObject(typeof(T), i_result);
        }

        private static int ConvertToInt32<T>(T value)
        {
            return Convert.ToInt32(value);
        }
    }
}
