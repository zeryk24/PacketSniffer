using System;
using PacketDotNet;

namespace PacketSniffer
{

    public static class PacketWrapper
    {
        public static PacketInfo CreatePacketInfo(this Packet packet, DateTime time, int len)
        {
            return new PacketInfo
            {
                Time = time,
                Packet = packet,
                IpPacket = (IPPacket)packet.ParentPacket,
                SourcePort = (packet as TransportPacket)?.SourcePort,
                DestinationPort = (packet as TransportPacket)?.DestinationPort,
                Len = len
            };
        }
    }

}