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
                await context.PostAsync("Exception: Value cannot be null. Parameter name: entity\r\n[File of type \'text/plain\']\r\nBot at {0}\r\n", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                var registrant = new Registrant(state.FirstName, state.LastName)
                {
                    Email = state.Email,
                    StartTime = state.StartTime,
                    EndTime = DateTime.UtcNow
                };

                await VisugRepoTableStorage<Registrant>.CreateItemAsync(registrant);

                Thread.Sleep(2500);
                await context.PostAsync("Just kidding, your data was sent to Azure.");
            };

            return new FormBuilder<PersonalData>()
                .Message("Hello, this is Esther. I'm the 2Commit bot!")
                .Field(nameof(FirstName))
                .Message("Nice to meet you, {FirstName}!")
                .Field(nameof(WinGadgets))



                .Field(nameof(LastName))
                .Field(nameof(Email))
                .AddRemainingFields()
                .OnCompletion(processPersonalData)
                .Build();
        }
    }
}

/*
 * 
 *                 .Field(nameof(WinGadgets),
                            validate: (state, value) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = value };
                                var blnGadgets = (bool) value;

                                if (!blnGadgets)
                                {
                                    result.Feedback = "Waarom niet???";
                                    result.IsValid = false;
                                }

                                return Task.FromResult(result);
                            })


                    .Confirm((state) =>
                {
                    return Task.FromResult(new PromptAttribute("Can we still continue with the bot?"));
                })
*/
