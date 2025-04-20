namespace SocketsSharp.Raw
{
    public class RawPacket
    {
        private byte[] data;
        private int length;

        public RawPacket(byte[] data, int length)
        {
            this.data = data;
            this.length = length;
        }

        public byte[] GetData()
        {
            return this.data;
        }

        public int GetLength()
        {
            return this.length;
        }
    }
}
