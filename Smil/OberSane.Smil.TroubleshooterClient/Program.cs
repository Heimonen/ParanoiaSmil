using OberSane.Smil.TroubleshooterClient.MessageBus;

namespace OberSane.Smil.TroubleshooterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var messageBusSubscriber = new MessageBusSubscriber();
            //messageBusSubscriber.Start();
            var chatClient = new ChatClient();
            chatClient.Start();

            //messageBusSubscriber.Stop();
        }
    }
}
