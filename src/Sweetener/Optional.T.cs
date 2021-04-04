// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Sweetener
{
    /// <summary>
    /// Represents a type that encapsulates an optional value.
    /// </summary>
    /// <remarks>
    /// <see cref="Optional{T}"/> and <see cref="Nullable{T}"/> are not equivalent in their usage.
    /// While <see cref="Nullable{T}"/> types provide a union of value types with <see langword="null"/>,
    /// <see cref="Optional{T}"/> types represent either a value of type <typeparamref name="T"/>,
    /// which may itself be <see langword="null"/>, or no value whatsoever.
    /// </remarks>
    /// <typeparam name="T">The underlying value type of the <see cref="Optional{T}"/> generic type.</typeparam>
    [Serializable]
    [DebuggerDisplay("{HasValue ? \"Some(\" + Value + \")\" : \"None\",nq}")]
    [SuppressMessage("Design"     , "CA1066:Type {0} should implement IEquatable<T> because it overrides Equals", Justification = "Equals method forwards calls to the underlying type where applicable")]
    [SuppressMessage("Naming"     , "CA1716:Identifiers should not match keywords"                              , Justification = "Generic class should avoid ambiguity with Visual Basic optional parameter keyword")]
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types"                 , Justification = "Underlying value type may not implement IEquatable<T>")]
    [SuppressMessage("Usage"      , "CA2231:Overload operator equals on overriding value type Equals"           , Justification = "Underlying value type may not implement IEquatable<T>")]
    public readonly struct Optional<T>
    {
        /// <summary>
        /// Gets a value indicating whether the current <see cref="Optional{T}"/> object
        /// has a value of its underlying type assigned.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the current <see cref="Optional{T}"/> object has a value;
        /// <see langword="false"/> if the current <see cref="Optional{T}"/> object has no value.
        /// </value>
        public bool HasValue { get; }

        /// <summary>
        /// Gets the value of the current <see cref="Optional{T}"/> object if it has been assigned an underlying value.
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
        public T Value => HasValue ? _value : throw new InvalidOperationException(SR.MissingOptionalValueMessage);

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
        /// <param name="obj">An object.</param>
        /// <returns>
        /// <see langword="true"/> if the <see cref="HasValue"/> property for the current
        /// <see cref="Optional{T}"/> object is <see langword="true"/> and either of the
        /// following is true; otherwise, <see langword="false"/>.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The <paramref name="obj"/> parameter is an <see cref="Optional{T}"/> object
        /// whose <see cref="HasValue"/> property is also <see langword="true"/> and whose
        /// value of the <see cref="Value"/> property is equal to the value of the
        /// <see cref="Value"/> property for the current <see cref="Optional{T}"/> object.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The <paramref name="obj"/> parameter is equal to the value of the
        /// <see cref="Value"/> property for the current <see cref="Optional{T}"/> object.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is Optional<T> optionalOther)
            {
                return HasValue && optionalOther.HasValue
                    ? EqualityComparer<T>.Default.Equals(_value, optionalOther._value)
                    : HasValue == optionalOther.HasValue;
            }

            return HasValue && (_value != null ? _value.Equals(obj) : obj == null);
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
            // Note that null/default will typically result in the same hash value as None,
            // but this behavior is consistent with Nullable<T>
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
        /// <remarks>The equivalent property for this operator is <see cref="Value"/>.</remarks>
        /// <param name="value">An <see cref="Optional{T}"/> value.</param>
        /// <returns>The value of the <see cref="Value"/> property for the <paramref name="value"/> parameter.</returns>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="HasValue"/> property is <see langword="false"/>.
        /// </exception>
        [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "The underlying value can be obtained using the Optional<T>.Value property")]
        public static explicit operator T(Optional<T> value)
            => value.Value;

        /// <summary>
        /// Creates a new <see cref="Optional{T}"/> object initialized to a specified value.
        /// </summary>
        /// <remarks>The equivalent method for this operator is <see cref="Optional{T}(T)"/>.</remarks>
        /// <param name="value">A value of type <typeparamref name="T"/>.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> object whose <see cref="Value"/> property is
        /// initialized with the <paramref name="value"/> parameter.
        /// </returns>
        [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Optional values can alternatively be created via the constructor or the method Optional.Some<T>(T)")]
        public static implicit operator Optional<T>(T value)
            => new Optional<T>(value);
    }

    /// <summary>
    /// Supports a type that encapsulates an optional value. This class cannot be inherited.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Static class should avoid ambiguity with Visual Basic optional parameter keyword")]
    public static class Optional
    {
        /// <summary>
        /// Compares the relative values of two <see cref="Optional{T}"/> objects.
        /// </summary>
        /// <typeparam name="T">
        /// The underlying value type of the <paramref name="m1"/> and <paramref name="m2"/> parameters.
        /// </typeparam>
        /// <param name="m1">A <see cref="Optional"/> object.</param>
        /// <param name="m2">A <see cref="Optional"/> object.</param>
        /// <returns>
        /// An integer that indicates the relative values of the <paramref name="m1"/> and <paramref name="m2"/>
        /// parameters.
        /// <list type="table">
        /// <listheader>
        /// <term>Return Value</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// The <see cref="Optional{T}.HasValue"/> property for <paramref name="m1"/> is <see langword="false"/>,
        /// and the <see cref="Optional{T}.HasValue"/> property for <paramref name="m2"/> is <see langword="true"/>,
        /// or the <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/> and <paramref name="m2"/>
        /// are <see langword="true"/>, and the value of the <see cref="Optional{T}.Value"/> property for
        /// <paramref name="m1"/> is less than the value of the <see cref="Optional{T}.Value"/> property for
        /// <paramref name="m2"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// The <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/> and <paramref name="m2"/>
        /// are <see langword="false"/>, or the <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/>
        /// and <paramref name="m2"/> are <see langword="true"/>, and the value of the <see cref="Optional{T}.Value"/>
        /// property for <paramref name="m1"/> is equal to the value of the <see cref="Optional{T}.Value"/>
        /// property for <paramref name="m2"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// The <see cref="Optional{T}.HasValue"/> property for <paramref name="m1"/> is <see langword="true"/>,
        /// and the <see cref="Optional{T}.HasValue"/> property for <paramref name="m2"/> is <see langword="false"/>,
        /// or the <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/> and <paramref name="m2"/>
        /// are <see langword="true"/>, and the value of the <see cref="Optional{T}.Value"/> property for
        /// <paramref name="m1"/> is greater than the value of the <see cref="Optional{T}.Value"/> property for
        /// <paramref name="m2"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare<T>(Optional<T> m1, Optional<T> m2)
        {
            if (m1.HasValue)
            {
                return m2.HasValue
                    ? Comparer<T>.Default.Compare(m1.GetValueOrDefault()!, m2.GetValueOrDefault()!)
                    : 1;
            }

            return m2.HasValue ? -1 : 0;
        }

        /// <summary>
        /// Indicates whether two specified <see cref="Optional{T}"/> objects are equal.
        /// </summary>
        /// <typeparam name="T">
        /// The underlying value type of the <paramref name="m1"/> and <paramref name="m2"/> parameters.
        /// </typeparam>
        /// <param name="m1">A <see cref="Optional"/> object.</param>
        /// <param name="m2">A <see cref="Optional"/> object.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="m1"/> parameter is equal to the <paramref name="m2"/>
        /// parameter; otherwise, <see langword="false"/>.
        /// <list type="table">
        /// <listheader>
        /// <term>Return Value</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term><see langword="true"/></term>
        /// <description>
        /// The <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/> and <paramref name="m2"/>
        /// are <see langword="false"/>, or the <see cref="Optional{T}.HasValue"/> properties for <paramref name="m1"/>
        /// and <paramref name="m2"/> are <see langword="true"/>, and the <see cref="Optional{T}.Value"/> properties
        /// of the parameters are equal.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see langword="false"/></term>
        /// <description>
        /// The <see cref="Optional{T}.HasValue"/> property is <see langword="true"/> for one parameter and
        /// <see langword="false"/> for the other parameter, or the <see cref="Optional{T}.HasValue"/> properties for
        /// <paramref name="m1"/> and <paramref name="m2"/> are <see langword="true"/>, and the
        /// <see cref="Optional{T}.Value"/> properties of the parameters are unequal.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public static bool Equals<T>(Optional<T> m1, Optional<T> m2)
            => m1.HasValue
                ? m2.HasValue && EqualityComparer<T>.Default.Equals(m1.GetValueOrDefault()!, m2.GetValueOrDefault()!)
                : !m2.HasValue;

        /// <summary>
        /// Creates a new <see cref="Optional{T}"/> objet that does not encapsulate a value.
        /// </summary>
        /// <remarks><see cref="None"/> is equivalent to the default value.</remarks>
        /// <value>An <see cref="Optional{T}"/> object initialized with no value.</value>
        public static Optional<T> None<T>()
            => default;

        /// <summary>
        /// Creates a new <see cref="Optional{T}"/> objet that encapsulates some value.
        /// </summary>
        /// <typeparam name="T">The underlying value type of the resulting <see cref="Optional{T}"/> object.</typeparam>
        /// <param name="value">A value of type <typeparamref name="T"/>.</param>
        /// <returns>
        /// An <see cref="Optional{T}"/> object whose <see cref="Optional{T}.Value"/> property is
        /// initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static Optional<T> Some<T>(T value)
            => new Optional<T>(value);

        
    }
}
