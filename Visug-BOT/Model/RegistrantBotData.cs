using System;
using Newtonsoft.Json;

namespace Visug2CommitBOTApp.Model
{
    public class RegistrantBotData
    {
        [JsonProperty(PropertyName = "RegistrantID")]
        public Guid RegistrantId { get; set; }
        [JsonProperty(PropertyName = "TimeStartedChatting")]
        public DateTime StartTime { get; set; }
        [JsonProperty(PropertyName = "TimeEndedChatting")]
        public DateTime EndTime { get; set; }
    }
}