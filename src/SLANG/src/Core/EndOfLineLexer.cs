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
    }
}