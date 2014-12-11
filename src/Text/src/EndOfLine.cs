namespace Text
{
    /// <summary>Enumerates types of line endings.</summary>
    public enum EndOfLine
    {
        Ignore = 0x0,
        Cr = 0x0D,
        Lf = 0x0A,
        CrLf = 0x0D0A,
        LfCr = 0x0A0D
    }
}
