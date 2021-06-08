using System;
using System.Collections.Generic;
using PacketDotNet;

namespace PacketSniffer
{

    public class PacketProcessor
    {
        private readonly Dictionary<Type, Action<PacketInfo>> PrintActions;

        public PacketProcessor()
        {
            PrintActions = new Dictionary<Type, Action<PacketInfo>>()
            {
                { typeof(TcpPacket), PrintTransportPacket },
                { typeof(UdpPacket), PrintTransportPacket },
                { typeof(IcmpV4Packet), PrintInternetPacket },
            };
        }

        public (Action<PacketInfo> writer, Packet extracted)? Select(Packet packet)
        {
            var tcp = packet.Extract<TcpPacket>();
            if (tcp != null && (Config.Instance.Tcp || Config.Instance.All))
                return (PrintActions[typeof(TcpPacket)], tcp);

            var udp = packet.Extract<UdpPacket>();
            if (udp != null && (Config.Instance.Tcp || Config.Instance.All))
                return (PrintActions[typeof(UdpPacket)], udp);

            var icmp = packet.Extract<IcmpV4Packet>();
            if (icmp != null && (Config.Instance.Icmp || Config.Instance.All))
                return (PrintActions[typeof(IcmpV4Packet)], icmp);

            return null;
        }

        private void PrintTransportPacket(PacketInfo packet)
        {
            if (Config.Instance.Port != -1 && packet.SourcePort != Config.Instance.Port && packet.DestinationPort != Config.Instance.Port)
                return;

            Console.WriteLine(packet.Packet.PrintPacket(packet));
        }

        private void PrintInternetPacket(PacketInfo packet)
        {
            Console.WriteLine(packet.IpPacket.PrintPacket(packet));
        }
    }

}
