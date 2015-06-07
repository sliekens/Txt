namespace TextFx
{
    using System;

    /// <summary>Creates instances of the <see cref="ValueRangeLexer" /> class.</summary>
    public class ValueRangeLexerFactory : IValueRangeLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Terminal> Create(char lowerBound, char upperBound)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentOutOfRangeException("upperBound", "Precondition: upperBound >= lowerBound");
            }

            return new ValueRangeLexer(lowerBound, upperBound);
        }
    }
}