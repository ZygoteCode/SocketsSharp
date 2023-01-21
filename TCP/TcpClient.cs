using System;

namespace SocketsSharp.Tcp
{
    public class TcpClient
    {
        private System.Net.Sockets.TcpClient client;
        private System.Net.Sockets.NetworkStream stream;

        public void ConnectTo(System.Net.IPAddress address, ushort port)
        {
            client = new System.Net.Sockets.TcpClient();
            client.Connect(new System.Net.IPEndPoint(address, port));
            stream = client.GetStream();
        }

        public void ConnectTo(System.Net.IPEndPoint ipEndPoint)
        {
            client = new System.Net.Sockets.TcpClient();
            client.Connect(ipEndPoint);
            stream = client.GetStream();
        }

        public void ConnectTo(string address, ushort port)
        {
            client = new System.Net.Sockets.TcpClient();
            client.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(address), port));
            stream = client.GetStream();
        }

        public void Send(byte[] data)
        {
            data = Combine(BitConverter.GetBytes(data.Length), data);
            stream.Write(data, 0, data.Length);
        }

        public byte[] Receive()
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);
            int size = BitConverter.ToInt32(buffer, 0);
            byte[] data = new byte[size];
            stream.Read(data, 0, data.Length);
            return data;
        }

        private byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
    }
}