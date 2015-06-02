namespace SLANG
{
    public class OptionLexerFactory : IOptionLexerFactory
    {
        public ILexer<Repetition> Create(ILexer lexer)
        {
            return new OptionLexer(lexer);
        }
    }
}