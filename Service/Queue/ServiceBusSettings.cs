using Newtonsoft.Json;

namespace ChangeNotification.Service.Queue
{
    public class ServiceBusSettings
    {
        [JsonProperty(nameof(ConnectionString))]
        public string ConnectionString { get; set; }

        [JsonProperty(nameof(QueueName))]
        public string QueueName { get; set; }
    }
}
