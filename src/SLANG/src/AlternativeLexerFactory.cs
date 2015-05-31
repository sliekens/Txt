namespace SLANG
{
    public class AlternativeLexerFactory : IAlternativeLexerFactory
    {
        public ILexer<Alternative> Create(params ILexer[] lexers)
        {
            return new AlternativeLexer(lexers);
        }
    }
}