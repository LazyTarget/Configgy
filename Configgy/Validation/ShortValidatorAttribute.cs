﻿using System;
using System.Linq;
using System.Reflection;

namespace Configgy.Validation
{
    /// <summary>
    /// An <see cref="IValueValidator"/> for <see cref="short"/>s.
    /// </summary>
    public class ShortValidatorAttribute : ValueValidatorAttributeBase, INumericishValidator<short>
    {
        /// <summary>
        /// The minimum value allowed by this validator.
        /// </summary>
        public short Min { get; protected set; }

        /// <summary>
        /// The maximum value allowed by this validator.
        /// </summary>
        public short Max { get; protected set; }

        /// <summary>
        /// The set of values allowed by this validator.
        /// </summary>
        public short[] ValidValues { get; protected set; }

        /// <summary>
        /// Creates a validator with default max and min values.
        /// </summary>
        public ShortValidatorAttribute()
        {
            Min = short.MinValue;
            Max = short.MaxValue;
        }

        /// <summary>
        /// Creates a validator with the given max and min values.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        public ShortValidatorAttribute(short min, short max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Creates a validator with the given set of valid values.
        /// </summary>
        /// <param name="validValues">The set of values to be considered valid; any other values will cause an exception.</param>
        public ShortValidatorAttribute(params short[] validValues)
            : this()
        {
            ValidValues = validValues;
        }

        /// <summary>
        /// Validate a potential value.
        /// This method must throw an exception if the value is invalid.
        /// </summary>
        /// <typeparam name="T">The type the value is expected to be coerced into.</typeparam>
        /// <param name="value">The raw string value.</param>
        /// <param name="valueName">The name of the value.</param>
        /// <param name="property">If this value is directly associated with a property on a <see cref="Config"/> instance this is the reference to that property.</param>
        /// <param name="result">If the validator did the coercion it should set this to the result.</param>
        /// <returns>
        ///     True if the validator performed coercion as a side effect, false otherwise.
        ///     Any return value (true or false) indicates successful validation.
        ///     If the validator did not successfully validate the value it should throw an exception, preferably <see cref="Exceptions.ValidationException"/>.
        /// </returns>
        /// <exception cref="Exceptions.ValidationException">Thrown when the value is not valid.</exception>
        public override bool Validate<T>(string value, string valueName, ICustomAttributeProvider property, out T result)
        {
            var val = short.Parse(value);

            if (val < Min || val > Max)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (ValidValues != null && !ValidValues.Contains(val))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            result = (T)(object)val;
            return true;
        }
    }
}
