using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.Server
{
    public class ChatConsumer : IConsumer<IChat>
    {
        public Task Consume(ConsumeContext<IChat> context)
        {
            Console.Write("TXT: " + context.Message.What);
            Console.Write("  SENT: " + context.Message.When);
            Console.Write("  PROCESSED: " + DateTime.Now);
            Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")");
            return Task.FromResult(0);
        }
    }
}
