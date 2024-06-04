using Helium.Core.Models;
using System.Net.Sockets;
using System.Text;

namespace Helium.ImportService.Connections
{
    internal class RconWebConnection : IDisposable
    {
        private readonly Socket _socket;
        private byte[]? _xorKey;
        private const int msgLength = 8196;
        private const int timeout = 20000;

        public RconWebConnection()
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = timeout,
                ReceiveTimeout = timeout
            };
        }

        public bool Connect(string address, int port, string password)
        {
            _socket.Connect(address, port);

            _xorKey = new byte[msgLength];
            _socket.Receive(_xorKey);
            _xorKey = Trim(_xorKey);

            Send($"login {password}");

            string result = Receive();

            if (result != "SUCCESS")
            {
                return false;
            }
            return true;
        }

        public bool Connect(HllGameserver gameserver) => Connect(gameserver.Address, gameserver.Port, gameserver.Password);

        public string Request(string message)
        {
            Send(message);
            return Receive();
        }

        private void Send(string message)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(message);

            byte[] xoredMsg = XorMsg(msgBytes);
            int i = _socket.Send(xoredMsg);
            if (i != xoredMsg.Length)
            {
                throw new SocketException();
            }
        }

        private string Receive()
        {
            byte[] msgRaw = new byte[msgLength];

            _socket.Receive(msgRaw, msgLength, SocketFlags.None);

            List<byte> msg = new(XorMsg(Trim(msgRaw)));

            while (msg.Count >= msgLength)
            {
                try
                {
                    _socket.Receive(msgRaw, msgLength, SocketFlags.None);
                }
                catch (Exception)
                {
                    break;
                }
                msg.AddRange(XorMsg(msgRaw));
            }
            return Encoding.UTF8.GetString(msg.ToArray());
        }

        private static byte[] Trim(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);
            Array.Resize(ref array, lastIndex + 1);
            return array;
        }

        private byte[] XorMsg(byte[] msg)
        {
            if (_xorKey == null) throw new InvalidOperationException("Socket needs to be connected");
            for (int i = 0; i < msg.Length; i++)
            {
                msg[i] ^= _xorKey[i % _xorKey.Length];
            }

            return msg;
        }

        public void Dispose()
        {
            _socket.Disconnect(false);
            _socket.Dispose();
        }
    }
}
