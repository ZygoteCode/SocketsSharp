using System.Net;
using System.Net.Sockets;

namespace SocketsSharp.Udp
{
    public class UdpServer : UdpActor
    {
        private IPEndPoint _listenOn;

        public UdpServer(ushort port) : this(new IPEndPoint(IPAddress.Any, port))
        {

        }

        public UdpServer(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            client = new UdpClient(_listenOn);
        }

        public void SendTo(byte[] data, IPEndPoint endpoint)
        {
            client.Send(data, data.Length, endpoint);
        }
    }
}