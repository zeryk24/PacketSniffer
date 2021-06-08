namespace PacketSniffer
{
    class Program
    {
        static void Main(string[] args)
        {
            new ArgumentParser().Parse(args);
            new InterfaceListener().Listen();
        }
    }
}