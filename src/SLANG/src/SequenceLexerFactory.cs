namespace SLANG
{
    public class SequenceLexerFactory : ISequenceLexerFactory
    {
        public ILexer<Sequence> Create(params ILexer[] lexers)
        {
            return new SequenceLexer(lexers);
        }
    }
}