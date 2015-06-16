namespace TextFx.ABNF
{
    public class GreedyAlternativeLexerFactory : IAlternativeLexerFactory
    {
        public ILexer<Alternative> Create(params ILexer[] lexers)
        {
            return new GreedyAlternativeLexer(lexers);
        }
    }
}
