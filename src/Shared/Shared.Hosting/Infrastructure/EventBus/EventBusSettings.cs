using System.Reflection;

namespace Shared.Hosting.Infrastructure.EventBus
{
    public class EventBusSettings
    {
        public Assembly[] RegistrationAssemblies { get; set; }
        public string ConnectionString { get; set; }
        public string SubscriptionPrefixId { get; set; }
    }
}