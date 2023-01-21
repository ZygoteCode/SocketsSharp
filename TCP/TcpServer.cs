using System;

namespace SocketsSharp.Tcp
{
    public class TcpServer
    {
        private System.Net.Sockets.TcpListener listener;

        public TcpServer(ushort port)
        {
            this.listener = new System.Net.Sockets.TcpListener(port);
            this.listener.Start();
        }

        public void SendTo(System.Net.Sockets.TcpClient tcpClient, byte[] data)
        {
            System.Net.Sockets.NetworkStream stream = tcpClient.GetStream();
            data = Combine(BitConverter.GetBytes(data.Length), data);
            stream.Write(data, 0, data.Length);
        }

        public System.Net.Sockets.TcpClient AcceptTcpClient()
        {
            return this.listener.AcceptTcpClient();
        }

        public byte[] ReceiveFrom(System.Net.Sockets.TcpClient tcpClient)
        {
            if (!tcpClient.Connected)
            {
                return null;
            }

            if (tcpClient.Available == 0)
            {
                return null;
            }

            System.Net.Sockets.NetworkStream stream = tcpClient.GetStream();
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