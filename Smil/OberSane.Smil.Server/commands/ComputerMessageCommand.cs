using System;

namespace OberSane.Smil.Server.commands
{
    class ComputerMessageCommand : ICommand
    {
        private string message;

        public ComputerMessageCommand(string message)
        {
            this.message = message;
        }

        public void Execute()
        {
            Console.WriteLine("MESSAGE TO THE COMPUTER: " + message);
        }
    }
}
