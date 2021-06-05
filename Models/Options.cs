
using CommandLine;
namespace PacketSniffer
{
    public class Options
    {
        [Option('i', "interface", Required = false, HelpText = "Interface you want to read from")]
        public string Intface { get; set; }
        [Option('p', Required = false, HelpText = "Port you want to read from", Default = -1)]
        public int Port { get; set; }
        [Option('t', "tcp", Required = false, HelpText = "Process only tcp packets")]
        public bool Tcp { get; set; }
        [Option('u', "udp", Required = false, HelpText = "Process only udp packets")]
        public bool Udp { get; set; }
        [Option("icmp", Required = false, HelpText = "Process only icmp packets")]
        public bool Icmp { get; set; }
        [Option('n', Required = false, HelpText = "Number of packets you want to process", Default = 1)]
        public int Num { get; set; }
    }
}
