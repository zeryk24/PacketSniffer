using CommandLine;

namespace PacketSniffer
{
    public class ArgumentParser
    {
        public void Parse(string[] args)
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
                });
        }
    }
}