using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Linq;
using System;
using UnityEngine;
using InGamePacket;
using Unity.VisualScripting;

public class PTPServerManager : Singleton<PTPServerManager>
{
// - Member Variable
    const string            SERVER_IP   = "127.0.0.1"; // - temp ip/my
    const int               SERVER_PORT = 5450;
    const int               PACKET_MAX  = 1024;

    private bool            _isHost = true;
    
    private TcpListener     _tcplistener = null;
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

    // - Method
    private void Awake()
    {
        if (_isHost)
        {
            _thread = new Thread(new ThreadStart(ListenForIncommingRequest));
            _thread.IsBackground = true;
            _thread.Start();
        }
        else
        {
            ConnectServer();
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        ExecuteMessage();
    }

    // - Listening
    private void ListenForIncommingRequest()
    {
        try
        {
            _tcplistener = new TcpListener(IPAddress.Parse(SERVER_IP), 50001);
            _tcplistener.Start();
            Debug.Log("Server is listening");

            while(true)
            {
                using (_tcpclient = _tcplistener.AcceptTcpClient())
                {
                    using (_netstream = _tcpclient.GetStream())
                    {
                        do 
                        {
                            // - To do
                        } while (true);
                    }
                }
            }
        }
        catch(SocketException e)
        {
            Debug.LogError("SocketException : " + e);
        }
    }

    // - 
    private void ExecuteMessage()
    {
        if (_messagequeue.Count == 0)
            return;

        MessageInfo msg = _messagequeue[0];
        if (!_packethandlermap.ContainsKey(msg.type))
            return;

        _packethandlermap[msg.type](msg.buffer);
    }

    public bool ConnectServer()
    {
        try
        {
            _tcpclient = new TcpClient(SERVER_IP, SERVER_PORT);
            _netstream = _tcpclient.GetStream();

            _thread = new Thread(ReceiveMessage);
            _thread.Start();
        }
        catch(Exception e)
        {
            Debug.Log("Server Connect Fail : " + e);
            return false;
        }

        return true;
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            try
            {
                if (_netstream.CanRead)
                {
                    _readsize += _netstream.Read(_buffer, _readsize, PACKET_MAX);

                    // - first receive packet, read data
                    if (_packettype == PacketType.PACKET_TYPE_NONE)
                    {
                        _packetsize = BitConverter.ToInt32(_buffer, 0);
                        _packettype = (PacketType)BitConverter.ToInt32(_buffer, sizeof(int));
                    }

                    // - All packect receive, input data in Messagequeue
                    if (_readsize == _packetsize)
                    {
                        MessageInfo msg;
                        msg.type = _packettype;
                        msg.buffer = new byte[_packetsize];
                        Array.Copy(_buffer, msg.buffer, _packetsize);

                        _messagequeue.Add(msg);

                        _readsize = 0;
                        _packetsize = 0;
                        _packettype = PacketType.PACKET_TYPE_NONE;
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.Log("ReceiveMessage Fail : " + e);
            }
        }
    }

    public void SendMessage(in PacketBase InPacket)
    {
        if (_netstream == null)
            return;

        try
        {
            if (_netstream.CanWrite)
            {
                List<byte> _buffer = new List<byte>();
                InPacket.Serialize(_buffer);
                _netstream.Write(_buffer.ToArray(), 0, _buffer.Count());
            }
        }
        catch(SocketException e)
        {
            Debug.Log("SendMessage Fail : " + e);
        }
    }

    public void AddPacketHandler(PacketType intype, PacketHandler inhandler)
    {
        _packethandlermap.Add(intype, inhandler);
    }

    // - If Application Quit, Memory Release
    private void OnApplicationQuit()
    {
        DisConnectServer();
    }

    private void DisConnectServer()
    {
        if (_tcpclient != null)
            _tcpclient.Close();

        if (_netstream != null)
            _netstream.Close();

        if (_thread != null)
            _thread.Abort();
    }
}