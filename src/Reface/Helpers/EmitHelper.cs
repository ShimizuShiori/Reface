using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Reface.Helpers
{
    public static class EmitHelper
    {
        public static Func<T, object> CreatePropertyGetter<T>(PropertyInfo property)
        {
            var type = typeof(T);

            var dynamicMethod = new DynamicMethod("get_" + property.Name, typeof(object), new[] { type }, type);
            var iLGenerator = dynamicMethod.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);

            iLGenerator.Emit(OpCodes.Callvirt, property.GetMethod);

            if (property.PropertyType.IsValueType)
            {
                // 如果是值类型，装箱
                iLGenerator.Emit(OpCodes.Box, property.PropertyType);
            }
            else
            {
                // 如果是引用类型，转换
                iLGenerator.Emit(OpCodes.Castclass, property.PropertyType);
            }

            iLGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(typeof(Func<T, object>)) as Func<T, object>;
        }

        public static Action<T, object> CreatePropertySetter<T>(string propertyName)
        {
            var type = typeof(T);
            var callMethod = type.GetMethod("set_" + propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
            return CreatePropertySetter<T>(callMethod);
        }

        public static Action<T, object> CreatePropertySetter<T>(PropertyInfo property)
        {
            return CreatePropertySetter<T>(property.SetMethod);
        }

        public static Action<T, object> CreatePropertySetter<T>(MethodInfo setMethod)
        {
            var type = typeof(T);

            var dynamicMethod = new DynamicMethod("EmitCallable", null, new[] { type, typeof(object) }, type.Module);
            var iLGenerator = dynamicMethod.GetILGenerator();

            var callMethod = setMethod;
            var parameterInfo = callMethod.GetParameters()[0];
            var local = iLGenerator.DeclareLocal(parameterInfo.ParameterType, true);

            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (parameterInfo.ParameterType.IsValueType)
            {
                // 如果是值类型，拆箱
                iLGenerator.Emit(OpCodes.Unbox_Any, parameterInfo.ParameterType);
            }
            else
            {
                // 如果是引用类型，转换
                iLGenerator.Emit(OpCodes.Castclass, parameterInfo.ParameterType);
            }

            iLGenerator.Emit(OpCodes.Stloc, local);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldloc, local);

            iLGenerator.EmitCall(OpCodes.Callvirt, callMethod, null);
            iLGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(typeof(Action<T, object>)) as Action<T, object>;
        }
    }
}
