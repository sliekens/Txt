namespace SLANG
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class StringExtensions
    {
        public static Stream AsStream(this string instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return new MemoryStream(instance.Select(Convert.ToByte).ToArray());
        }
    }
}