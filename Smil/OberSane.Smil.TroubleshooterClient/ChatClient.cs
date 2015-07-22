using System;
using System.Configuration;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using OberSane.Smil.Contracts;
using OberSane.Smil.TroubleshooterClient.Model;

namespace OberSane.Smil.TroubleshooterClient
{
    public class ChatClient
    {
        private IBusControl _busControl;
        private BusHandle _busHandle;
        private string _userName;

        public ChatClient()
        {
            InitializeMessageBus();
        }

        public void Start()
        {
            InitializeUser();

            var command = string.Empty;

            while (command != Constants.QuitCommand)
            {
                Console.Write(_userName + " > ");
                command = Console.ReadLine();

                var message = new ChatMessage
                {
                    What = command,
                    When = DateTime.Now
                };
                _busControl.Publish<IChat>(message);
            }

            _busHandle.Stop().Wait();
        }

        private void InitializeUser()
        {
            Console.WriteLine("Welcome to SMIL v 0.1! I would like to know you more.");
            var nameIsCorrect = false;
            while(nameIsCorrect == false)
            {
                Console.WriteLine("What is your name?");
                Console.Write("> ");
                _userName = Console.ReadLine();
                Console.WriteLine("Thank you " + _userName + ", was it correct? [y/n]");
                Console.Write("> ");
                var reply = Console.ReadLine();
                nameIsCorrect = reply != null && (reply.ToLower() == "y" || reply.ToLower() == "yes");
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("> Connected to SMIL Mainframe as user "  + _userName);
        }

        private void InitializeMessageBus()
        {
            Log4NetLogger.Use();
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
              x.Host(GetDefaultRabbitMqBusUri(), h => { }));
            _busHandle = _busControl.Start();
        }

        private static Uri GetDefaultRabbitMqBusUri()
        {
            return new Uri(ConfigurationManager.AppSettings[Constants.DefaultRabbitMqBusUri]);
        }
    }
}
