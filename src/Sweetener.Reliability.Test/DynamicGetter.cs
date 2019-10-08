using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Sweetener.Reliability.Test
{
    internal static class DynamicGetter
    {
        public static Func<TSource, TOut> ForField<TSource, TOut>(string name)
        {
            FieldInfo fieldInfo = typeof(TSource).GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null)
                throw new ArgumentException($"Cannot find an instance field called '{name}' for type '{typeof(TSource).Name}'.");

            if (fieldInfo.FieldType != typeof(TOut))
                throw new ArgumentException($"Field type '{nameof(fieldInfo.FieldType)}' does not match input type '{typeof(TOut).Name}'.");

            DynamicMethod method = new DynamicMethod(typeof(TSource).Name + "_Get" + name, typeof(TOut), new Type[] { typeof(TSource) });

            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, fieldInfo);
            gen.Emit(OpCodes.Ret);

            return (Func<TSource, TOut>)method.CreateDelegate(typeof(Func<TSource, TOut>));
        }

        public static Func<TOut> ForStaticField<TSource, TOut>(string name)
        {
            FieldInfo fieldInfo = typeof(TSource).GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null)
                throw new ArgumentException($"Cannot find an instance field called '{name}' for type '{typeof(TSource).Name}'.");

            if (fieldInfo.FieldType != typeof(TOut))
                throw new ArgumentException($"Field type '{nameof(fieldInfo.FieldType)}' does not match input type '{typeof(TOut).Name}'.");

            DynamicMethod method = new DynamicMethod(typeof(TSource).Name + "_Get" + name, typeof(TOut), Type.EmptyTypes);

            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldsfld, fieldInfo);
            gen.Emit(OpCodes.Ret);

            return (Func<TOut>)method.CreateDelegate(typeof(Func<TOut>));
        }

        public static Func<TSource, TOut> ForProperty<TSource, TOut>(string name)
        {
            PropertyInfo propertyInfo = typeof(TSource).GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
                throw new ArgumentException($"Cannot find an instance property called '{name}' for type '{typeof(TSource).Name}'.");

            if (propertyInfo.PropertyType != typeof(TOut))
                throw new ArgumentException($"Property type '{nameof(propertyInfo.PropertyType)}' does not match input type '{typeof(TOut).Name}'.");

            MethodInfo getter = propertyInfo.GetGetMethod(nonPublic: true);
            if (getter == null)
                throw new ArgumentException($"Cannot find 'getter' for property '{name}' on type '{typeof(TSource).Name}'.");

            DynamicMethod method = new DynamicMethod(typeof(TSource).Name + "_Get" + name, typeof(TOut), new Type[] { typeof(TSource) });

            ILGenerator gen = method.GetILGenerator();
            gen.Emit    (OpCodes.Ldarg_0);
            gen.EmitCall(OpCodes.Callvirt, getter, null);
            gen.Emit    (OpCodes.Ret);

            return (Func<TSource, TOut>)method.CreateDelegate(typeof(Func<TSource, TOut>));
        }

        public static Func<TOut> ForStaticProperty<TSource, TOut>(string name)
        {
            PropertyInfo propertyInfo = typeof(TSource).GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (propertyInfo == null)
                throw new ArgumentException($"Cannot find a static property called '{name}' for type '{typeof(TSource).Name}'.");

            if (propertyInfo.PropertyType != typeof(TOut))
                throw new ArgumentException($"Property type '{nameof(propertyInfo.PropertyType)}' does not match input type '{typeof(TOut).Name}'.");

            MethodInfo getter = propertyInfo.GetGetMethod(nonPublic: true);
            if (getter == null)
                throw new ArgumentException($"Cannot find 'getter' for property '{name}' on type '{typeof(TSource).Name}'.");

            DynamicMethod method = new DynamicMethod(typeof(TSource).Name + "_Get" + name, typeof(TOut), Type.EmptyTypes);

            ILGenerator gen = method.GetILGenerator();
            gen.EmitCall(OpCodes.Call, getter, null);
            gen.Emit    (OpCodes.Ret);

            return (Func<TOut>)method.CreateDelegate(typeof(Func<TOut>));
        }
    }
}
