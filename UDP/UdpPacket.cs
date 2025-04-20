using System.Net;

namespace SocketsSharp.Udp
{
    public class UdpPacket
    {
        private IPEndPoint Sender { get; set; }
        private byte[] Data { get; set; }

        public UdpPacket(IPEndPoint sender, byte[] data)
        {
            this.Sender = sender;
            this.Data = data;
        }
    }
}