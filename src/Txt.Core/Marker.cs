namespace Txt.Core
{
    public class Marker : ITextContext
    {
        public Marker(int offset, int line, int column)
        {
            Offset = offset;
            Line = line;
            Column = column;
        }

        public int Column { get; }

        public int Line { get; }

        public int Offset { get; }
    }
}
