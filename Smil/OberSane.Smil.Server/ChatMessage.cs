using System;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.Server
{
    class ChatMessage : IChat
    {
        public string What { get; set; }
        public DateTime When { get; set; }
    }
}
