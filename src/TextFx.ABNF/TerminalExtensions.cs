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

            return Convert.ToString(instance.ToChar(), toBase).ToUpperInvariant();
        }
    }
}