namespace SLANG
{
    using System.IO;
    using System.Text;

    internal static class StringExtensions
    {
        internal static Stream ToMemoryStream(this string instance)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(instance));
        }
    }
}
