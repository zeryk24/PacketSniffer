using System;
using PacketDotNet;

namespace PacketSniffer
{
    public class PacketService
    {
        Packet packet;
        DateTime time;
        bool all;
        int len;

        public PacketService(Packet packet, DateTime time, bool all, int len)
        {
            this.packet = packet;
            this.time = time;
            this.all = all;
            this.len = len;

            ProcessTcpPacket();
            ProcessUdpPacket();
            ProcessIcmpPacket();
        }

        public void ProcessTcpPacket()
        {
            var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();
            if (tcpPacket != null && (Config.Instance.Tcp || all))
            {
                var ipPacket = (PacketDotNet.IPPacket)tcpPacket.ParentPacket;

                if (Config.Instance.Port != -1)
                    if (tcpPacket.SourcePort != Config.Instance.Port && tcpPacket.DestinationPort != Config.Instance.Port)
                        return;

                PacketInfo packetInfo = CreatePacketInfo(time,ipPacket,tcpPacket.SourcePort,tcpPacket.DestinationPort,len);
                System.Console.WriteLine(tcpPacket.PrintPacket(packetInfo));
            }
        }

        public void ProcessUdpPacket()
        {
            var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();
            if (udpPacket != null && (Config.Instance.Udp || all))
            {
                var ipPacket = (PacketDotNet.IPPacket)udpPacket.ParentPacket;

                if (Config.Instance.Port != -1)
                    if (udpPacket.SourcePort != Config.Instance.Port && udpPacket.DestinationPort != Config.Instance.Port)
                        return;

                PacketInfo packetInfo = CreatePacketInfo(time,ipPacket,udpPacket.SourcePort,udpPacket.DestinationPort,len);
                System.Console.WriteLine(udpPacket.PrintPacket(packetInfo));
            }
        }

        public void ProcessIcmpPacket()
        {
            var icmpV4Packet = packet.Extract<PacketDotNet.IcmpV4Packet>();
            if (icmpV4Packet != null && (Config.Instance.Icmp || all))
            {
                var ipPacket = (PacketDotNet.IPPacket)icmpV4Packet.ParentPacket;           
                PacketInfo packetInfo = CreatePacketInfo(time,ipPacket,null,null,len);
                System.Console.WriteLine(icmpV4Packet.PrintPacket(packetInfo));
            }
        }

        public PacketInfo CreatePacketInfo(DateTime time, IPPacket ipPacket, ushort? SourcePort, ushort? DestinationPort, int len)
        {
            PacketInfo packetInfo = new PacketInfo
                {
                    Time = time,
                    IpPacket = ipPacket,
                    SourcePort = SourcePort,
                    DestinationPort = DestinationPort,
                    Len = len
                };
            return packetInfo;
        }
    }
}
