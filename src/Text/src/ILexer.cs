namespace Text
{
    public interface ILexer<TToken>
        where TToken : Token
    {
        TToken Read();

        bool TryRead(out TToken token);
    }
}
