namespace TextFx
{
    /// <summary>Creates instances of the <see cref="SequenceLexer" /> class.</summary>
    public class SequenceLexerFactory : ISequenceLexerFactory
    {
        /// <inheritdoc />
        public ILexer<Sequence> Create(params ILexer[] lexers)
        {
            return new SequenceLexer(lexers);
        }
    }
}