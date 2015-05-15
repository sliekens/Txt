// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibleCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class VisibleCharacterLexer : AlternativeLexer<VisibleCharacter>
    {
        /// <summary>Initializes a new instance of the <see cref="VisibleCharacterLexer"/> class.</summary>
        /// <param name="serviceLocator">The object that retrieves instances of <see cref="ILexer{TElement}"/> by type and optional rule name.</param>
        public VisibleCharacterLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "VCHAR", '\x21', '\x7E')
        {
        }
    }
}