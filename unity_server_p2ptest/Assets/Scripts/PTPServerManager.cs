using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;
using Packet;

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
    
}