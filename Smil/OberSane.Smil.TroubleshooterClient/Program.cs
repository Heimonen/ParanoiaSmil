using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.TroubleshooterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetLogger.Use();

            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
              x.Host(new Uri("rabbitmq://localhost/"), h => { }));
            var busHandle = bus.Start();
            var text = string.Empty;

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new ChatMessage()
                {
                    What = text,
                    When = DateTime.Now
                };
                bus.Publish<IChat>(message);
            }

            busHandle.Stop().Wait();
        }
    }
}
