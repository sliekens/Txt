namespace TextFx.ABNF
{
    using System;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>Creates instances of the <see cref="ValueRangeLexer" /> class.</summary>
    public class ValueRangeLexerFactory : IValueRangeLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(char lowerBound, char upperBound)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }

            var range = new char[1 + upperBound - lowerBound];
            int i;
            char c;
            for (i = 0, c = lowerBound; c <= upperBound; i++, c++)
            {
                range[i] = c;
            }

            return new ValueRangeLexer(range, lowerBound, upperBound);
        }

        public ILexer<Terminal> Create(int lowerBound, int upperBound, [NotNull] Encoding encoding)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException(nameof(upperBound), "Precondition: upperBound >= lowerBound");
            }

            var range = new char[1 + upperBound - lowerBound];
            int i;
            int c;
            for (i = 0, c = lowerBound; c <= upperBound; i++, c++)
            {
                var bytes = BitConverter.GetBytes(c);
                if (!(encoding is UnicodeEncoding)) 
                {
                    bytes = Encoding.Convert(encoding, Encoding.Unicode, bytes);
                }

                var values = Encoding.Unicode.GetChars(bytes);
                range[i] = values[0];
            }

            return new ValueRangeLexer(range, lowerBound, upperBound);
        }
    }
}