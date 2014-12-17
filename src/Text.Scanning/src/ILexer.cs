using System.Diagnostics.Contracts;

namespace Text.Scanning
{
    [ContractClass((typeof(ContractClassForILexer<>)))]
    public interface ILexer<TToken>
        where TToken : Token
    {
        TToken Read();

        bool TryRead(out TToken token);

        void PutBack(TToken token);
    }
}
