using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Hosting;

namespace Shared.Hosting.Infrastructure.EventBus
{
    public class EventBusInitializationBackgroundService : BackgroundService
    {
        private readonly AutoSubscriber _autoSubscriber;
        private readonly EventBusSettings _busSettings;

        public EventBusInitializationBackgroundService(AutoSubscriber autoSubscriber, EventBusSettings busSettings)
        {
            _autoSubscriber = autoSubscriber;
            _busSettings = busSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_busSettings.RegistrationAssemblies != null && _busSettings.RegistrationAssemblies.Any())
            {
                await _autoSubscriber.SubscribeAsync(_busSettings.RegistrationAssemblies, stoppingToken);
            }
        }
    }
}