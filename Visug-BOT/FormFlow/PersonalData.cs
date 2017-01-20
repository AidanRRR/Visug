using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Web.Http.Results;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using Visug2CommitBOTApp.Model;
using Visug2CommitBOTApp.Persistence;

namespace Visug2CommitBOTApp.FormFlow
{
    [Serializable]
    public class PersonalData
    {
        [Prompt("Would you like to win some tech gadgets? {||}")]
        public bool WinGadgets { get; set; }
        [Prompt("What's your first name?")]
        public string FirstName { get; set; }
        [Prompt("What's your last name?")]
        public string LastName { get; set; }
        [Prompt("What's your email?")]
        public string Email { get; set; }
        private DateTime StartTime { get; set; }

        public PersonalData()
        {
            StartTime = DateTime.UtcNow;
        }

        public static IForm<PersonalData> BuildForm()
        {
            OnCompletionAsyncDelegate<PersonalData> processPersonalData = async (context, state) =>
            {
                await context.PostAsync("Sending data to Azure...");
                var registrant = new Registrant(firstName: state.FirstName, lastName: state.LastName)
                {
                    Email = state.Email,
                    StartTime = state.StartTime,
                    EndTime = DateTime.UtcNow
                };

                await VisugRepoTableStorage<Registrant>.CreateItemAsync(registrant);
            };

            return new FormBuilder<PersonalData>()
                .Message("Hello! This is the 2Commit BOT. Type 'help' for help!")
                .OnCompletion(processPersonalData)
                .Build();
        }
    }
}