using UnityEngine;
using InGamePacket;

public class PacketHandler
{
    [RuntimeInitializeOnLoadMethod]
    static void RegistPacketHandler()
    {
        PTPServerManager.Instance.AddPacketHandler(InGamePacket.PacketType.PACKET_TYPE_S_MOVE, HANDLE_S_MOVE);
    }  
    
    static void HANDLE_S_MOVE(in byte[] InBuffer)
    {
        C_MOVE message = new C_MOVE(InBuffer);
        GameManager.Instance.Move(message._input);
    }
}
