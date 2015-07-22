using System;
using System.Collections.Generic;

namespace OberSane.Smil.Server.commands
{
    class MessageCommand : ICommand
    {
        private List<Tuple<string, string>> arguments;
        private readonly string _data;

        public MessageCommand(List<Tuple<string, string>> arguments, string data)
        {
            this.arguments = arguments;
            _data = data;
        }

        public void Execute()
        {
            Console.WriteLine("MESSAGE: " + _data);
        }
    }
}
