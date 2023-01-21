using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

namespace SocketsSharp.Raw
{
    public class RawServer
    {
        private ushort port;
        private Socket socket;
        private byte[] receiveBuffer;
        private EndPoint remoteEndPoint;

        public RawServer(ushort port)
        {
            this.port = port;
            this.receiveBuffer = new byte[Int16.MaxValue * Int16.MaxValue];
            this.remoteEndPoint = new IPEndPoint(IPAddress.Any, this.port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw);
            this.socket.Bind(new IPEndPoint(IPAddress.Any, this.port));
        }

        public RawServer(IPEndPoint ipEndPoint)
        {
            this.port = (ushort)ipEndPoint.Port;
            this.receiveBuffer = new byte[Int16.MaxValue * Int16.MaxValue];
            this.remoteEndPoint = ipEndPoint;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw);
            this.socket.Bind(ipEndPoint);
        }

        public RawServer(IPAddress address, ushort port)
        {
            this.remoteEndPoint = new IPEndPoint(address, port);
            this.port = port;
            this.receiveBuffer = new byte[Int16.MaxValue * Int16.MaxValue];
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw);
            this.socket.Bind(new IPEndPoint(address, port));
        }

        public RawServer(string address, ushort port)
        {
            this.remoteEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
            this.port = port;
            this.receiveBuffer = new byte[Int16.MaxValue * Int16.MaxValue];
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw);
            this.socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
        }

        public ushort GetPort()
        {
            return this.port;
        }

        public RawPacket ReceivePacket()
        {
            int length = this.socket.ReceiveFrom(this.receiveBuffer, 0, this.receiveBuffer.Length, SocketFlags.None, ref this.remoteEndPoint);
            return new RawPacket(this.receiveBuffer, length);
        }

        public Socket GetSocket()
        {
            return this.socket;
        }

        public byte[] GetReceiveBuffer()
        {
            return this.receiveBuffer;
        }

        public EndPoint GetRemoteEndPoint()
        {
            return this.remoteEndPoint;
        }

        public IPEndPoint GetIpEndPoint()
        {
            return ((IPEndPoint)this.remoteEndPoint);
        }

        public string GetIpAddress()
        {
            return this.GetIpEndPoint().Address.ToString();
        }

        public void SendPacket(byte[] data, int length, IPAddress address, ushort port)
        {
            this.socket.SendTo(data, length, SocketFlags.None, new IPEndPoint(address, port));
        }

        public void SendPacket(byte[] data, int length, string address, ushort port)
        {
            this.socket.SendTo(data, length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(address), port));
        }

        public void SendPacket(byte[] data, int length, IPEndPoint ipEndPoint)
        {
            this.socket.SendTo(data, length, SocketFlags.None, ipEndPoint);
        }

        public void SendPacket(byte[] data, IPAddress address, ushort port)
        {
            this.socket.SendTo(data, data.Length, SocketFlags.None, new IPEndPoint(address, port));
        }

        public void SendPacket(byte[] data, string address, ushort port)
        {
            this.socket.SendTo(data, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(address), port));
        }

        public void SendPacket(byte[] data, IPEndPoint ipEndPoint)
        {
            this.socket.SendTo(data, data.Length, SocketFlags.None, ipEndPoint);
        }

        public void SendPacket(string data, IPAddress address, ushort port)
        {
            byte[] content = Encoding.UTF8.GetBytes(data);
            this.socket.SendTo(content, content.Length, SocketFlags.None, new IPEndPoint(address, port));
        }

        public void SendPacket(string data, string address, ushort port)
        {
            byte[] content = Encoding.UTF8.GetBytes(data);
            this.socket.SendTo(content, content.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(address), port));
        }

        public void SendPacket(string data, IPEndPoint ipEndPoint)
        {
            byte[] content = Encoding.UTF8.GetBytes(data);
            this.socket.SendTo(content, content.Length, SocketFlags.None, ipEndPoint);
        }
    }
}