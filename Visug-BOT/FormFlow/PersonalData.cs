using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Scorables.Internals;
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
        [Prompt("Nice to meet you {FirstName}, What's your last name?")]
        public string LastName { get; set; }
        [Prompt("What's your email?")]
        public string Email { get; set; }
        [Prompt("Thank you {FirstName} {LastName}. Can we contact you on your email ({Email}) regarding Azure & .NET news?")]
        public bool CanContactByEmail { get; set; }
        private DateTime StartTime { get; set; }

        public PersonalData()
        {
            StartTime = DateTime.UtcNow;
        }

        public static IForm<PersonalData> BuildForm()
        {
            OnCompletionAsyncDelegate<PersonalData> processPersonalData = async (context, state) =>
            {
                await context.PostAsync("Exception: Value cannot be null. Parameter name: entity\r\n[File of type \'text/plain\']\r\nBot at {0}\r\n", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                var registrant = new Registrant(state.FirstName, state.LastName)
                {
                    Email = state.Email,
                    StartTime = state.StartTime,
                    EndTime = DateTime.UtcNow,
                    CanContactByEmail = state.CanContactByEmail,
                    WinGadgets = state.WinGadgets
                };

                await VisugRepoTableStorage<Registrant>.CreateItemAsync(registrant);

                Thread.Sleep(2500);

                if (state.WinGadgets)
                {
                    await context.PostAsync("Just kidding, your data was sent to Azure. \n We will contact you next week regarding the tech gadgets. \n - Esther");
                }
                else
                {
                    await context.PostAsync("Just kidding, your data was sent to Azure. - Esther");
                }

            };

            return new FormBuilder<PersonalData>()
                .Message("Hello, this is Esther. I'm the 2Commit bot!")
                .Field(nameof(FirstName))
                .Field(nameof(LastName))
                .Field(nameof(Email))
                .Field(nameof(WinGadgets))
                .AddRemainingFields()
                .OnCompletion(processPersonalData)
                .Build();
        }
    }
}