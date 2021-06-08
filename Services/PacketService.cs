using System;
using PacketDotNet;

namespace PacketSniffer
{
    public class PacketService
    {
        private readonly PacketProcessor processor;

        public PacketService()
        {
            this.processor = new PacketProcessor();
        }

        public bool ProccessPacket(Packet packet, DateTime time, int len)
        {
            var writer = processor.Select(packet);
            if (writer == null)
                return false;

            var packetInfo = PacketWrapper.CreatePacketInfo(writer.Value.extracted, time, len);
            writer.Value.writer(packetInfo);

            return true;
        }
    }
}
