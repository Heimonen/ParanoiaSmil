using System;
using OberSane.Smil.TroubleshooterClient.MessageBus;
using OberSane.Smil.TroubleshooterClient.Model;

namespace OberSane.Smil.TroubleshooterClient
{
    public class ChatClient
    {
        private string _userName;
        private readonly MessageBusPublisher _messageBusPublisher;

        public ChatClient()
        {
            _messageBusPublisher = new MessageBusPublisher();
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

                _messageBusPublisher.Publish(message);
            }

            _messageBusPublisher.Stop();
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
    }
}
