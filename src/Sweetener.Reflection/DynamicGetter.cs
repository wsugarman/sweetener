using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Sweetener.Reflection
{
    // TODO: This API has not been finalized

    /// <summary>
    /// A class for creating delegates that may access members regardless of their visibility.
    /// </summary>
    public static class DynamicGetter
    {
        /// <summary>
        /// Gets a delegate that returns the value of the field with the given <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TSource">The type that contains the field.</typeparam>
        /// <typeparam name="TField">The type of the desired field.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>A delegate for accessing the field.</returns>
        /// <exception cref="ArgumentException">
        /// <para>
        /// A field with the given <paramref name="name"/> cannot be found in the type
        /// <typeparamref name="TSource"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>The type of the field does not match the given type <typeparamref name="TField"/>.</para>
        /// </exception>
        public static Func<TSource, TField> ForField<TSource, TField>(string name)
        {
            FieldInfo fieldInfo = typeof(TSource).GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null)
                throw new ArgumentException($"Cannot find an instance field called '{name}' for type '{typeof(TSource).Name}'.");

            if (fieldInfo.FieldType != typeof(TField))
                throw new ArgumentException($"Field type '{nameof(fieldInfo.FieldType)}' does not match input type '{typeof(TField).Name}'.");

            ParameterExpression inputParam = Expression.Parameter(typeof(TSource), "input");
            return Expression.Lambda<Func<TSource, TField>>(Expression.Field(inputParam, fieldInfo), inputParam).Compile();
        }

        /// <summary>
        /// Gets a delegate that returns the value of the static field with the given <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TSource">The type that contains the field.</typeparam>
        /// <typeparam name="TField">The type of the desired field.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>A delegate for accessing the field.</returns>
        /// <exception cref="ArgumentException">
        /// <para>
        /// A field with the given <paramref name="name"/> cannot be found in the type
        /// <typeparamref name="TSource"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>The type of the field does not match the given type <typeparamref name="TField"/>.</para>
        /// </exception>
        public static Func<TField> ForStaticField<TSource, TField>(string name)
        {
            FieldInfo fieldInfo = typeof(TSource).GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (fieldInfo == null)
                throw new ArgumentException($"Cannot find an instance field called '{name}' for type '{typeof(TSource).Name}'.");

            if (fieldInfo.FieldType != typeof(TField))
                throw new ArgumentException($"Field type '{nameof(fieldInfo.FieldType)}' does not match input type '{typeof(TField).Name}'.");

            return Expression.Lambda<Func<TField>>(Expression.Field(null, fieldInfo)).Compile();
        }

        /// <summary>
        /// Gets a delegate that returns the value of the property with the given <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TSource">The type that contains the property.</typeparam>
        /// <typeparam name="TProperty">The type of the desired property.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <returns>A delegate for accessing the property.</returns>
        /// <exception cref="ArgumentException">
        /// <para>
        /// A property with the given <paramref name="name"/> cannot be found in the type
        /// <typeparamref name="TSource"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>The type of the property does not match the given type <typeparamref name="TProperty"/>.</para>
        /// </exception>
        public static Func<TSource, TProperty> ForProperty<TSource, TProperty>(string name)
        {
            PropertyInfo propertyInfo = typeof(TSource).GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
                throw new ArgumentException($"Cannot find an instance property called '{name}' for type '{typeof(TSource).Name}'.");

            if (propertyInfo.PropertyType != typeof(TProperty))
                throw new ArgumentException($"Property type '{nameof(propertyInfo.PropertyType)}' does not match input type '{typeof(TProperty).Name}'.");

            MethodInfo getter = propertyInfo.GetGetMethod(nonPublic: true);
            if (getter == null)
                throw new ArgumentException($"Cannot find 'getter' for property '{name}' on type '{typeof(TSource).Name}'.");

            ParameterExpression inputParam = Expression.Parameter(typeof(TSource), "input");
            return Expression.Lambda<Func<TSource, TProperty>>(Expression.Property(inputParam, propertyInfo), inputParam).Compile();
        }

        /// <summary>
        /// Gets a delegate that returns the value of the static property with the given <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TSource">The type that contains the property.</typeparam>
        /// <typeparam name="TProperty">The type of the desired property.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <returns>A delegate for accessing the property.</returns>
        /// <exception cref="ArgumentException">
        /// <para>
        /// A property with the given <paramref name="name"/> cannot be found in the type
        /// <typeparamref name="TSource"/>.
        /// </para>
        /// <para>-or-</para>
        /// <para>The type of the property does not match the given type <typeparamref name="TProperty"/>.</para>
        /// </exception>
        public static Func<TProperty> ForStaticProperty<TSource, TProperty>(string name)
        {
            PropertyInfo propertyInfo = typeof(TSource).GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (propertyInfo == null)
                throw new ArgumentException($"Cannot find a static property called '{name}' for type '{typeof(TSource).Name}'.");

            if (propertyInfo.PropertyType != typeof(TProperty))
                throw new ArgumentException($"Property type '{nameof(propertyInfo.PropertyType)}' does not match input type '{typeof(TProperty).Name}'.");

            MethodInfo getter = propertyInfo.GetGetMethod(nonPublic: true);
            if (getter == null)
                throw new ArgumentException($"Cannot find 'getter' for property '{name}' on type '{typeof(TSource).Name}'.");

            return Expression.Lambda<Func<TProperty>>(Expression.Property(null, propertyInfo)).Compile();
        }
    }
}
