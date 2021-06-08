using System;
using SharpPcap;

namespace PacketSniffer
{
    public class InterfaceListener
    {
        PacketService packetService;
        public InterfaceListener()
        {
            packetService = new PacketService();
        }

        public void Listen()
        {
            var allDevices = CaptureDeviceList.Instance;

            ICaptureDevice device = null;
            foreach (var dev in allDevices)
            {
                if (dev.Name == Config.Instance.Intface)
                {
                    device = dev;
                    break;
                }
                
                if (string.IsNullOrEmpty(Config.Instance.Intface))
                    System.Console.WriteLine(dev.ToString());
            }

            if (string.IsNullOrEmpty(Config.Instance.Intface))
                    return;

            device.Open();
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            device.Capture();

            device.Close();
        }

        int readNumber = 1;
        private void device_OnPacketArrival(object s, CaptureEventArgs e)
        {
            if (readNumber > Config.Instance.Num)
                Environment.Exit(0);
            var time = e.Packet.Timeval.Date;
            var len = e.Packet.Data.Length;

            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            bool all = !Config.Instance.Icmp && !Config.Instance.Tcp && !Config.Instance.Udp;

            if (!packetService.ProccessPacket(packet, time, len)) 
                Console.WriteLine("Neznámý packet");
            readNumber++;
        }

    }
}