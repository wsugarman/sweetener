using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Sweetener
{
    /// <summary>
    /// Represents a type that may or may not be available.
    /// </summary>
    /// <remarks>
    /// <see cref="Optional{T}"/> and <see cref="Nullable{T}"/> are not equivalent in their usage.
    /// While <see cref="Nullable{T}"/> provides a union of value types with <see langword="null"/>,
    /// <see cref="Optional{T}"/> provides additional semantics for a given type <typeparamref name="T"/>
    /// that allows callers to denote a value is undefined.
    /// </remarks>
    /// <typeparam name="T">The underlying value type of the <see cref="Optional{T}"/> generic type.</typeparam>
    [Serializable]
    [SuppressMessage("Design"     , "CA1066:Type {0} should implement IEquatable<T> because it overrides Equals", Justification = "Equals method forwards calls to the underlying type where applicable")]
    [SuppressMessage("Naming"     , "CA1716:Identifiers should not match keywords"                              , Justification = "Optional type is generic and will not conflict with keyword")]
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types"                 , Justification = "It would be misleading to provide an Equals override for a wrapper around a type that may not implement Equals")]
    [SuppressMessage("Usage"      , "CA2231:Overload operator equals on overriding value type Equals"           , Justification = "It would be misleading to provide an Equals override for a wrapper around a type that may not implement Equals")]
    public readonly struct Optional<T>
    {
        /// <summary>
        /// Gets a value indicating whether the current <see cref="Optional{T}"/> object
        /// has a defined value of its underlying type.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the current <see cref="Optional{T}"/> object has a value;
        /// <see langword="false"/> if the current <see cref="Optional{T}"/> object has no value.
        /// </value>
        public bool HasValue { get; }

        /// <summary>
        /// Gets the value of the current <see cref="Optional{T}"/> object if it has been defined.
        /// </summary>
        /// <remarks>
        /// If the <see cref="HasValue"/> property is <see langword="true"/>, it is still
        /// possible for the value of the <see cref="Value"/> property to be the default
        /// value of the underlying type.
        /// </remarks>
        /// <value>
        /// The value of the current <see cref="Optional{T}"/> object if the <see cref="HasValue"/>
        /// property is <see langword="true"/>. An exception is thrown if the <see cref="HasValue"/>
        /// property is <see langword="false"/>.
        /// </value>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="HasValue"/> property is <see langword="false"/>.
        /// </exception>
        public T Value => HasValue ? _value : throw new InvalidOperationException(SR.UndefinedOptionalValueMessage);

        /// <summary>
        /// Returns an empty <see cref="Optional{T}"/> object where the <see cref="HasValue"/>
        /// property is <see langword="false"/>.
        /// </summary>
        /// <remarks><see cref="Undefined"/> is equivalent to the default value.</remarks>
        /// <value>An empty <see cref="Optional{T}"/> object.</value>
        [SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Avoid creating non-generic Optional class that would conflict with VB keyword")]
        public static Optional<T> Undefined { get; } = default;

        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{T}"/> structure to the specified value.
        /// </summary>
        /// <remarks>
        /// The <see cref="Optional{T}"/> constructor initializes the <see cref="HasValue"/>
        /// property of the new <see cref="Optional{T}"/> object to <see langword="true"/>,
        /// and the <see cref="Value"/> property to the value of the <paramref name="value"/> parameter.
        /// </remarks>
        /// <param name="value">A value of type <typeparamref name="T"/>.</param>
        public Optional(T value)
        {
            HasValue = true;
            _value   = value;
        }

        /// <summary>
        /// Retrieves the value of the current <see cref="Optional{T}"/> object,
        /// or the default value of the underlying type.
        /// </summary>
        /// <returns>
        /// The value of the <see cref="Value"/> property if the <see cref="HasValue"/> property
        /// is <see langword="true"/>; otherwise, the default value of the underlying type.
        /// </returns>
        [return: MaybeNull]
        public T GetValueOrDefault()
            => _value;

        /// <summary>
        /// Retrieves the value of the current <see cref="Optional{T}"/> object,
        /// or the specified default value.
        /// </summary>
        /// <param name="defaultValue">
        /// A value to return if the <see cref="HasValue"/> property is <see langword="false"/>.
        /// </param>
        /// <returns>
        /// The value of the <see cref="Value"/> property if the <see cref="HasValue"/> property
        /// is <see langword="true"/>; otherwise, the <paramref name="defaultValue"/> parameter.
        /// </returns>
        public T GetValueOrDefault(T defaultValue)
            => HasValue ? _value : defaultValue;

        /// <summary>
        /// Gets the value of the <see cref="Value"/> property if the <see cref="HasValue"/>
        /// property is <see langword="true"/>. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the value of the <see cref="Value"/> property,
        /// if the <see cref="HasValue"/> property is <see langword="true"/>; otherwise,
        /// the default value of the underlying type. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the current <see cref="Optional{T}"/> object has a value;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryGetValue([MaybeNullWhen(false)] out T value)
        {
            value = _value;
            return HasValue;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Optional{T}"/> object is equal to a specified object.
        /// </summary>
        /// <param name="other">An object.</param>
        /// <returns>
        /// <see langword="true"/> if the <see cref="HasValue"/> property for the current
        /// <see cref="Optional{T}"/> object is <see langword="true"/> and either of the
        /// following is true; otherwise, <see langword="false"/>.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="other"/> parameter is an <see cref="Optional{T}"/> object
        /// whose <see cref="HasValue"/> property is also <see langword="true"/> and whose
        /// value of the <see cref="Value"/> property is equal to the value of the
        /// <see cref="Value"/> property for the current <see cref="Optional{T}"/> object.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="other"/> parameter is equal to the value of the
        /// <see cref="Value"/> property for the current <see cref="Optional{T}"/> object.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public override bool Equals(object? other)
        {
            if (other is Optional<T> optionalOther)
            {
                return HasValue && optionalOther.HasValue
                    ? _value?.Equals(optionalOther._value) ?? optionalOther._value == null
                    : HasValue == optionalOther.HasValue;
            }

            return HasValue ? _value?.Equals(other) ?? other == null : false;
        }

        /// <summary>
        /// Retrieves the hash code of the object returned by the <see cref="Value"/> property.
        /// </summary>
        /// <returns>
        /// The hash code of the object returned by the <see cref="Value"/> property if the
        /// <see cref="HasValue"/> property is <see langword="true"/> and the value of
        /// the <see cref="Value"/> property is not <see langword="null"/>; otherwise zero.
        /// </returns>
        public override int GetHashCode()
            // Note that null/default will typically result in the same hash value
            // as undefined Optional<T>, but this behavior is consistent with Nullable<T>
            // and doesn't modify the result of the underlying GetHashCode()
            => HasValue && _value != null ? _value.GetHashCode() : 0;

        /// <summary>
        /// Returns the text representation of the value of the current <see cref="Optional{T}"/> object.
        /// </summary>
        /// <remarks>
        /// The <see cref="ToString"/> method returns the string yielded by calling the
        /// <c>ToString</c> method of the object returned by the <see cref="Value"/> property.
        /// </remarks>
        /// <returns>
        /// The text representation of the object returned by the <see cref="Value"/> property
        /// if the <see cref="HasValue"/> property is <see langword="true"/> and the value of
        /// the <see cref="Value"/> property is not <see langword="null"/>; otherwise
        /// an empty string (<c>""</c>).
        /// </returns>
        public override string? ToString()
            => HasValue && _value != null ? _value.ToString() : "";

        /// <summary>
        /// Defines an explicit conversion of an <see cref="Optional{T}"/> instance to its underlying value.
        /// </summary>
        /// <remarks>The equivalent method for this operator is <see cref="Value"/>.</remarks>
        /// <param name="value">An <see cref="Optional{T}"/> value.</param>
        /// <returns>The value of the <see cref="Value"/> property for the <paramref name="value"/> parameter.</returns>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="HasValue"/> property is <see langword="false"/>.
        /// </exception>
        [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Value property alternative is already defined")]
        public static explicit operator T(Optional<T> value)
            => value.Value;

        /// <summary>
        /// Creates a new <see cref="Optional{T}"/> object initialized to a specified value.
        /// </summary>
        /// <remarks>The equivalent method for this operator is <see cref="Optional(T)"/>.</remarks>
        /// <param name="value">A value of type <typeparamref name="T"/>.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> object whose <see cref="Value"/> property is
        /// initialized with the <paramref name="value"/> parameter.
        /// </returns>
        [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Constructor alternative is already defined")]
        public static implicit operator Optional<T>(T value)
            => new Optional<T>(value);
    }
}
