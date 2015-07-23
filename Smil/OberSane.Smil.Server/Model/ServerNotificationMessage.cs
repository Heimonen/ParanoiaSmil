using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.Server.Model
{
    public class ServerNotificationMessage : IServerNotification
    {
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
