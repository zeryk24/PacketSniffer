namespace PacketSniffer
{
    class Program
    {
        static void Main(string[] args)
        {
            new InfoPrinter().PrintHeader();
            new ArgumentParser().Parse(args);
            new InterfaceListener().Listen();
        }
    }
}