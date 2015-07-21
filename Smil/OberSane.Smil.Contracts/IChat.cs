using System;

namespace OberSane.Smil.Contracts
{
    public interface IChat
    {
        string What { get; }

        DateTime When { get; }
    }
}
