using System.Linq;

namespace TextFx.ABNF
{
    using System;

    public static class TerminalExtensions
    {
        /// <summary>
        ///     Converts the current instance to its equivalent string representation in a specified base.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
        /// <returns>The converted value in the specified base.</returns>
        /// <exception cref="ArgumentException"><paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
        public static string ToBase(this Terminal instance, int toBase)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            var values = instance.Text.ToCharArray().Select(c => Convert.ToString(c, toBase).ToUpperInvariant());
            var valuesAsString = string.Join(".", values);
            switch (toBase)
            {
                case 2:
                    return "%b" + valuesAsString;
                case 10:
                    return "%d" + valuesAsString;
                case 16:
                    return "%x" + valuesAsString;
                default:
                    throw new ArgumentException("The given base is not currently supported.", "toBase");
            }
        }
    }
}