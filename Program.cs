using System;
using CommandLine;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Globalization;
using System.Text;
using System.Linq;
using PacketDotNet;

namespace PacketSniffer
{
    class Program
    {
        static int i = 1;
        public static void device_OnPacketArrival(object s, CaptureEventArgs e)
        {
            if (i > Config.Instance.Num)
                Environment.Exit(0);
            var time = e.Packet.Timeval.Date;
            var len = e.Packet.Data.Length;

            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            bool all = !Config.Instance.Icmp && !Config.Instance.Tcp && !Config.Instance.Udp;

            var packetService = new PacketService(packet, time, all,len);            
            i++;
        }

        static void Main(string[] args)
        {
                 CommandLine.Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       Config.Instance.Intface = o.Intface;
                       Config.Instance.Port = o.Port;
                       Config.Instance.Tcp = o.Tcp;
                       Config.Instance.Udp = o.Udp;
                       Config.Instance.Icmp = o.Icmp;
                       Config.Instance.Num = o.Num;

                       var allDevices = CaptureDeviceList.Instance;

                       ICaptureDevice device = null;
                       foreach (var dev in allDevices)
                       {
                           if (dev.Name == Config.Instance.Intface)
                               device = dev;
                            
                           if (string.IsNullOrEmpty(Config.Instance.Intface))
                                System.Console.WriteLine(dev.ToString());
                       }

                       if (string.IsNullOrEmpty(Config.Instance.Intface))
                                return;

                       device.Open();
                       device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
                       device.Capture();

                       device.Close();
                   });
        }
    }
}