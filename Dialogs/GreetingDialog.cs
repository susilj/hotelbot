
namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Dialogs
{
    using global::Microsoft.Bot.Builder.Dialogs;
    using global::Microsoft.Bot.Connector;
    using System;
    using System.Threading.Tasks;

    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi I'm Hotel Bot");

            await Respond(context);

            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            string userName = string.Empty;

            context.UserData.TryGetValue<string>("Name", out userName);

            if(string.IsNullOrWhiteSpace(userName))
            {
                await context.PostAsync("What is your name?");

                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync($"Hi {userName}. How can i help you today?");
            }
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            IMessageActivity message = await argument;

            string userName = string.Empty;

            bool getName = false;

            context.UserData.TryGetValue<string>("Name", out userName);

            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;

                context.UserData.SetValue<string>("Name", userName);

                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context);

            context.Done(message);
        }
    }
}