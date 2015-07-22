using System;
using System.Configuration;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using OberSane.Smil.Contracts;
using OberSane.Smil.TroubleshooterClient.Model;

namespace OberSane.Smil.TroubleshooterClient.MessageBus
{
    public class MessageBusPublisher
    {
        private IBusControl _busControl;
        private BusHandle _busHandle;

        public MessageBusPublisher()
        {
            InitializeMessageBus();
        }

        public void Publish(IChat message)
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
            return new Uri(ConfigurationManager.AppSettings[Constants.DefaultRabbitMqBusUri]);
        }
    }
}
