using System.Diagnostics.Contracts;

namespace Text
{
    [ContractClass((typeof(ContractClassForILexer<>)))]
    public interface ILexer<TToken>
        where TToken : Token
    {
        TToken Read();

        bool TryRead(out TToken token);
    }
}
