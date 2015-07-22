using System;

namespace OberSane.Smil.Contracts
{
    public interface IServerNotification
    {
        string Text { get; }

        DateTime TimeStamp { get; }
    }
}
