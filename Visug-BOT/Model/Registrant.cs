using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Visug2CommitBOTApp.Model
{
    public class Registrant
    {
        [JsonProperty(PropertyName = "RegistrantID")]
        public Guid RegistrantId { get; set; }
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Timestamp")]
        public DateTime Timestamp { get; set; }

        public Registrant()
        {
            RegistrantId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }
    }
}