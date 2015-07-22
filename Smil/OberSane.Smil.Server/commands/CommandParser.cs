using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MassTransit;
using OberSane.Smil.Contracts;
using OberSane.Smil.Server.commands;

namespace OberSane.Smil.Server.commands
{
    class CommandParser
    {
        public ICommand ParseMessage(ConsumeContext<IChat> message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var what = message.Message.What;
            if (IsCommand(what))
            {
                //Regex for splitting a string using space when not surrounded by single or double quotes
                var regex = new Regex("[^\\s\"']+|\"[^\"]*\"|'[^']*'");
                //MatchCollection
                var matches = regex.Matches(message.Message.What);

                var arr = Regex.Matches(message.Message.What, "[^\\s\"']+|\"[^\"]*\"|'[^']*'")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToArray();
                var command = arr[0];
                var arguments = new List<Tuple<string, string>>();
                string data = null;
                if (arr.Length > 1)
                {
                    for (var i = 1; i < arr.Length; i++)
                    {
                        if (IsArgument(arr[i]))
                        {
                            var argument = arr[i];
                            string value = null;
                            if (arr.Length >= i + 2)
                            {
                                value = arr[i + 1];
                                i++;
                            }
                            arguments.Add(new Tuple<string, string>(argument, value));
                        }
                        else if (IsData(arr[i]))
                        {
                            data = arr[i];
                        }
                    }
                }
                switch (command.ToLower())
                {
                    case "/message":
                        return new MessageCommand(arguments, data);
                    case "/who":
                        return new WhoCommand();
                    default:
                        throw new InvalidOperationException(command);
                }
            }
            else
            {
                return new ComputerMessageCommand(message.Message.What);
            }
        }

        private static bool IsData(string what)
        {
            // TODO: We might want to extend this to other characters
            return what.StartsWith("\"") || what.StartsWith("\'");
        }

        private static bool IsArgument(string what)
        {
            return what.StartsWith("-");
        }

        private static bool IsCommand(string what)
        {
            return what.StartsWith("/");
        }

    }
}
