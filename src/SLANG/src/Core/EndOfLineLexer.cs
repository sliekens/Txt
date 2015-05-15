// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndOfLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class EndOfLineLexer : SequenceLexer<EndOfLine, CarriageReturn, LineFeed>
    {
        public EndOfLineLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "CRLF")
        {
        }

        protected override bool TryRead1(ITextScanner scanner, out CarriageReturn element)
        {
            return this.Services.GetInstance<ILexer<CarriageReturn>>().TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out LineFeed element)
        {
            return this.Services.GetInstance<ILexer<LineFeed>>().TryRead(scanner, out element);
        }
    }
}