using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using OberSane.Smil.Contracts;
using OberSane.Smil.Server.commands;

namespace OberSane.Smil.Server
{
    public class ChatConsumer : IConsumer<IChat>
    {

        private readonly CommandParser _parser;

        public ChatConsumer()
        {
            _parser = new CommandParser();
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
            return Task.FromResult(0);
        }
    }
}
