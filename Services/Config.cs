namespace PacketSniffer
{
    public class Config
    {
        public static Config Instance { get; } = new Config();
        private Config() { }

        public string Intface { get; set; }
        public int Port { get; set; }
        public bool Tcp { get; set; }
        public bool Udp { get; set; }
        public bool Icmp { get; set; }
        public int Num { get; set; }
    }
}
