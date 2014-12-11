namespace Text
{
    public abstract class Token
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public int Offset { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return this.Data;
        }
    }
}