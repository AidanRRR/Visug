using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Visug2CommitBOTApp.Dialogs
{
    [Serializable]
    public class Testdialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ConversationReceived);
        }

        private async Task ConversationReceived(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            if (activity != null && activity.Text.Contains("Aidan"))
            {
                await context.PostAsync($"Dag {activity.Text}");
            }

            context.Wait(ConversationReceived);
        }
    }
}