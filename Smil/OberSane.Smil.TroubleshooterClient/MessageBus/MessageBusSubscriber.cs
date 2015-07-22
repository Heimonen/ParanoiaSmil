using MassTransit;
using MassTransit.Log4NetIntegration.Logging;

namespace OberSane.Smil.TroubleshooterClient.MessageBus
{
    public class MessageBusSubscriber
    {
        private IBusControl _busControl;
        private BusHandle _busHandle;

        public MessageBusSubscriber()
        {
            InitializeMessageBus();
        }

        public void Start()
        {
            _busHandle = _busControl.Start();
        }

        public void Stop()
        {
            _busHandle.Stop().Wait();
        }

        private void InitializeMessageBus()
        {
            Log4NetLogger.Use();
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(MessageBusPublisher.GetDefaultRabbitMqBusUri(), h => { });

                x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", e =>
                  e.Consumer<ServerConsumer>());
            });
        }
    }
}
