using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using OberSane.Smil.Contracts;
using OberSane.Smil.Server.commands;
using OberSane.Smil.Server.MessageBus;
using OberSane.Smil.Server.Model;

namespace OberSane.Smil.Server
{
    public class ChatConsumer : IConsumer<IChat>
    {
        private MessageBusPublisher _messageBusPublisher;
        private readonly CommandParser _parser;

        public ChatConsumer()
        {
            _parser = new CommandParser();
            _messageBusPublisher = new MessageBusPublisher();
        }

        public Task Consume(ConsumeContext<IChat> context)
        {
            try
            {
                _parser.ParseMessage(context).Execute();
            }
            catch (InvalidOperationException e)
            {
                Console.Write("Invalid command! Type /help for help ;D");
            }
            Console.WriteLine(" (" + Thread.CurrentThread.ManagedThreadId + ")");

            var reply = new ServerNotificationMessage
            {
                Text = "SMIL: Message revceived",
                TimeStamp = DateTime.Now
            };

            _messageBusPublisher.Publish(reply);

            return Task.FromResult(0);
        }
    }
}
