using System.Text;
using PacketDotNet;

namespace PacketSniffer
{

    public static class PacketExtensions
    {
        /// <summary> Extension method from PacketDotNet library </summary>
        ///source: https://github.com/chmorgan/packetnet/blob/master/PacketDotNet/Packet.cs
        /// This method is in the source above from line 440 to line 504, I just changed it for my need, it is not my code!
        public static string PrintPacket(this Packet packet, PacketInfo packetInfo)
        {
            var data = packet.BytesSegment.Bytes;
            var buffer = new StringBuilder();
            var bytes = "";
            var ascii = "";


            buffer.AppendFormat("{0} T {1} {2} : {3} > {4} : {5}, length {6} bytes\n", packetInfo.Time.ToString("yyyy:MM:dd"), packetInfo.Time.ToString("HH:mm:ss.fffzzz"),
                        packetInfo.IpPacket.SourceAddress, packetInfo.SourcePort, packetInfo.IpPacket.DestinationAddress, packetInfo.DestinationPort, packetInfo.Len);

            // parse the raw data
            for (var i = 1; i <= data.Length; i++)
            {
                // add the current byte to the bytes hex string
                bytes += data[i - 1].ToString("x").PadLeft(2, '0') + " ";

                // add the current byte to the asciiBytes array for later processing
                if (data[i - 1] < 0x21 || data[i - 1] > 0x7e)
                    ascii += ".";
                else
                    ascii += Encoding.ASCII.GetString(new[] { data[i - 1] });

                // add an additional space to split the bytes into
                //  two groups of 8 bytes
                if (i % 16 != 0 && i % 8 == 0)
                {
                    bytes += " ";
                    ascii += " ";
                }

                // append the output string
                string segmentNumber;
                if (i % 16 == 0)
                {
                    // add the 16 byte segment number
                    segmentNumber = (((i - 16) / 16) * 10).ToString().PadLeft(4, '0');

                    // build the line
                    buffer.AppendLine(segmentNumber + ":  " + bytes + "  " + ascii);

                    // reset for the next line
                    bytes = "";
                    ascii = "";

                    continue;
                }

                // handle the last pass
                if (i == data.Length)
                {
                    // add the 16 byte segment number
                    segmentNumber = ((((i - 16) / 16) + 1) * 10).ToString().PadLeft(4, '0');

                    // build the line
                    buffer.AppendLine(segmentNumber.PadLeft(4, '0') + ":  " + bytes.PadRight(49, ' ') + "  " + ascii);
                }
            }

            return buffer.ToString();
        }
    }
}
