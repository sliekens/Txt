// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG.Core
{
    using Microsoft.Practices.ServiceLocation;

    public class CharacterLexer : AlternativeLexer<Character>
    {
        /// <summary>Initializes a new instance of the <see cref="CharacterLexer"/> class.</summary>
        public CharacterLexer(IServiceLocator serviceLocator)
            : base(serviceLocator, "CHAR", '\x01', '\x7F')
        {
        }
    }
}
