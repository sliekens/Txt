// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewLineLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.CRLF
{
    public class NewLineLexer : CompositeLexer<Concatenation, NewLine>
    {
        public NewLineLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
