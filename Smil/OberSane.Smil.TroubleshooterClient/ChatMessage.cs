using System;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.TroubleshooterClient
{
    class ChatMessage : IChat
    {
        public string What { get; set; }
        public DateTime When { get; set; }
    }
}
