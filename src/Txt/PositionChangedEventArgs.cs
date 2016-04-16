using System;

namespace Txt
{
    public class PositionChangedEventArgs : EventArgs
    {
        public int Offset { get; set; }
    }
}