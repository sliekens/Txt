using System;
using System.Text;

namespace TextFx.Tests
{
    public class FakeTextSource : ITextSource
    {
        public Action OnDispose { get; set; }

        public Func<int> OnRead { get; set; }

        public Action<char> OnUnreadChar { get; set; }

        public Func<char[], int, int, int> OnReadCharArrayInt32Int32 { get; set; }

        public Action<char[], int, int> OnUnreadCharArrayInt32Int32 { get; set; }

        public Func<Encoding> OnEncodingGet { get; set; }

        public void Dispose()
        {
            if (OnDispose == null)
            {
                throw new NotImplementedException();
            }

            OnDispose();
        }

        public int Read()
        {
            if (OnRead == null)
            {
                throw new NotImplementedException();
            }

            return OnRead();
        }

        public void Unread(char c)
        {
            if (OnUnreadChar == null)
            {
                throw new NotImplementedException();
            }

            OnUnreadChar(c);
        }

        public int Read(char[] buffer, int offset, int count)
        {
            if (OnReadCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            return OnReadCharArrayInt32Int32(buffer, offset, count);
        }

        public void Unread(char[] buffer, int offset, int count)
        {
            if (OnUnreadCharArrayInt32Int32 == null)
            {
                throw new NotImplementedException();
            }

            OnUnreadCharArrayInt32Int32(buffer, offset, count);
        }

        public Encoding Encoding
        {
            get
            {
                if (OnEncodingGet == null)
                {
                    throw new NotImplementedException();
                }

                return OnEncodingGet();
            }
        }
    }
}