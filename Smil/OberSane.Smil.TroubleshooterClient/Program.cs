namespace OberSane.Smil.TroubleshooterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatClient = new ChatClient();
            chatClient.Start();
        }
    }
}
