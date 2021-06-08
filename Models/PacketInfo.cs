using System;
using PacketDotNet;

namespace PacketSniffer
{
    public class PacketInfo
    {
        public DateTime Time { get; set; }
        public IPPacket IpPacket { get; set; }
        public ushort? SourcePort{ get; set; }
        public ushort? DestinationPort { get; set; }
        public int Len { get; set; }
        public Packet Packet { get; internal set; }
    }
}
