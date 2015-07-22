using System;
using System.Threading.Tasks;
using MassTransit;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.TroubleshooterClient.MessageBus
{
    public class ServerConsumer : IConsumer<IServerNotification>
    {
        public Task Consume(ConsumeContext<IServerNotification> context)
        {
            Console.Write("TXT: " + context.Message.Text);
            return Task.FromResult(0);
        }
    }
}