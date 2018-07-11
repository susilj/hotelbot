

namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Dialogs
{
    using global::Microsoft.Bot.Builder.Dialogs;
    using global::Microsoft.Bot.Builder.FormFlow;
    using Models;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class HotelBotDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
                new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) =>
                {
                    return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuation);
                }),
                new DefaultCase<string, IDialog<string>>((context, text) =>
                {
                    return Chain.ContinueWith(FormDialog.FromForm(RoomReservation.BuildForm, FormOptions.PromptInStart), AfterGreetingContinuation);
                })
            )
            .Unwrap()
            .PostToUser();

        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            object token = await item;

            string name = string.Empty;

            context.UserData.TryGetValue<string>("Name", out name);

            return Chain.Return($"Thank you for using the hotel bot: {name}");
        }
    }
}