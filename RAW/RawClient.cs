using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketsSharp.Raw
{
    public class RawClient
    {
        private ushort port;
        private Socket socket;

        public RawClient(string address, ushort port)
        {
            foreach (IPAddress ipAddress in Dns.GetHostEntry(address).AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(ipAddress, port);
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Raw, ProtocolType.Raw);

                if (tempSocket == null)
                {
                    continue;
                }

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    socket = tempSocket;
                    break;
                }
            }

            this.port = port;
        }

        public RawClient(IPAddress address, ushort port)
        {
            this.port = port;
            IPEndPoint ipe = new IPEndPoint(address, port);
            socket = new Socket(ipe.AddressFamily, SocketType.Raw, ProtocolType.Raw);
            socket.Connect(ipe);
        }

        public RawClient(IPEndPoint ipEndPoint)
        {
            this.port = (ushort)ipEndPoint.Port;
            socket = new Socket(ipEndPoint.AddressFamily, SocketType.Raw, ProtocolType.Raw);
            socket.Connect(ipEndPoint);
        }

        public ushort GetPort()
        {
            return this.port;
        }

        public Socket GetSocket()
        {
            return this.socket;
        }

        public void SendPacket(byte[] data, int length)
        {
            this.socket.Send(data, length, 0);
        }

        public void SendPacket(byte[] data)
        {
            this.socket.Send(data, data.Length, 0);
        }

        public void SendPacket(string data)
        {
            byte[] converted = Encoding.UTF8.GetBytes(data);
            this.socket.Send(converted, converted.Length, 0);
        }

        public RawPacket ReceivePacket()
        {
            byte[] data = new byte[Int16.MaxValue];
            int bytes = 0;

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
            }
            while (bytes > 0);

            return new RawPacket(data, bytes);
        }
    }
}