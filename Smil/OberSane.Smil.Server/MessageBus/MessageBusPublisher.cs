using System;
using System.Configuration;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using OberSane.Smil.Contracts;

namespace OberSane.Smil.Server.MessageBus
{
    public class MessageBusPublisher
    {
        private IBusControl _busControl;
        private BusHandle _busHandle;

        public MessageBusPublisher()
        {
            InitializeMessageBus();
        }

        public void Publish(IServerNotification message)
        {
            _busControl.Publish(message);
        }

        public void Stop()
        {
            _busHandle.Stop().Wait();
        }

        private void InitializeMessageBus()
        {
            Log4NetLogger.Use();
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
              x.Host(GetDefaultRabbitMqBusUri(), h => { }));
            _busHandle = _busControl.Start();
        }

        public static Uri GetDefaultRabbitMqBusUri()
        {
            return new Uri(ConfigurationManager.AppSettings["DefaultRabbitMqBusUri"]);
        }
    }
}
