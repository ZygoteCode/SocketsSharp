using System.Net.Sockets;

namespace SocketsSharp.Udp
{
    public abstract class UdpActor
    {
        protected UdpClient client;

        protected UdpActor()
        {
            client = new UdpClient();
        }

        public UdpPacket Receive()
        {
            UdpReceiveResult result = client.ReceiveAsync().GetAwaiter().GetResult();
            return new UdpPacket(result.RemoteEndPoint, result.Buffer);
        }
    }
}