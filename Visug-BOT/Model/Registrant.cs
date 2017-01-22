using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Visug2CommitBOTApp.Model
{
    public class Registrant : TableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool CanContactByEmail { get; set; }
        public bool WinGadgets { get; set; }
        public new DateTime Timestamp { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Registrant(string firstName, string lastName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
            StartTime = DateTime.UtcNow;
        }
    }
}