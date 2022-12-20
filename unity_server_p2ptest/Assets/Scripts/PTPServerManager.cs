using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;
using InGamePacket;
using Unity.VisualScripting;

public class PTPServerManager : Singleton<PTPServerManager>
{
// - Member Variable
    const string            SERVER_IP   = "127.0.0.1";
    const int               SERVER_PORT = 7777;
    const int               PACKET_MAX  = 1024;

    private TcpClient       _tcpclient  = null;
    private NetworkStream   _netstream  = null;
    private Thread          _thread     = null;

    private byte[]          _buffer     = new byte[PACKET_MAX];
    private int             _packetsize = 0;
    private int             _readsize = 0;
    private PacketType      _packettype = PacketType.PACKET_TYPE_NONE;

    public delegate void PacketHandler(in byte[] inbuffer);
    private Dictionary<PacketType, PacketHandler> _packethandlermap = new Dictionary<PacketType, PacketHandler>();

    struct MessageInfo
    {
        public PacketType type;
        public byte[] buffer;
    }
    private List<MessageInfo> _messagequeue = new List<MessageInfo>();

    private void Update()
    {
        ExecuteMessage();
    }

    private void ExecuteMessage()
    {
        if (_messagequeue.Count == 0)
            return;

        MessageInfo msg = _messagequeue[0];
        if (!_packethandlermap.ContainsKey(msg.type))
            return;

        _packethandlermap[msg.type](msg.buffer);
    }

    // - If Application Quit, Memory Release
    private void OnApplicationQuit()
    {
        Release();
    }

    private void Release()
    {
        if (_tcpclient != null)
            _tcpclient.Close();

        if (_netstream != null)
            _netstream.Close();

        if (_thread != null)
            _thread.Abort();
    }
}