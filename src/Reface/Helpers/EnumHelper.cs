﻿using System;
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
    }
}
