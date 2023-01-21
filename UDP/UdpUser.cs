using System.Net;

namespace SocketsSharp.Udp
{
    public class UdpUser : UdpActor
    {
        private UdpUser()
        {

        }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            UdpUser connection = new UdpUser();
            connection.client.Connect(hostname, port);
            return connection;
        }

        public static UdpUser ConnectTo(IPEndPoint ipEndPoint)
        {
            UdpUser connection = new UdpUser();
            connection.client.Connect(ipEndPoint);
            return connection;
        }

        public static UdpUser ConnectTo(IPAddress address, ushort port)
        {
            UdpUser connection = new UdpUser();
            connection.client.Connect(new IPEndPoint(address, port));
            return connection;
        }

        public void Send(byte[] data)
        {
            client.Send(data, data.Length);
        }
    }
}