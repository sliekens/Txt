// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineFeedLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class LineFeedLexer : Lexer<LineFeed>
    {
        /// <summary>Initializes a new instance of the <see cref="LineFeedLexer"/> class.</summary>
        public LineFeedLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "LF")
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out LineFeed element)
        {
            Element terminal;
            if (!TryReadTerminal(scanner, '\x0A', out terminal))
            {
                element = default(LineFeed);
                return false;
            }

            element = new LineFeed(terminal);
            return true;
        }
    }
}