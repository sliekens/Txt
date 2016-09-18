namespace Txt.Core
{
    public partial class TextSource
    {
        private class Snapshot : ITextContext
        {
            public Snapshot(int dataIndex, long offset, int line, int column)
            {
                DataIndex = dataIndex;
                Offset = offset;
                Line = line;
                Column = column;
            }

            public int Column { get; }

            public int DataIndex { get; }

            public int Line { get; }

            public long Offset { get; }
        }
    }
}
