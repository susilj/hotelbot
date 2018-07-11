
namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Dialogs
{
    using global::Microsoft.Bot.Builder.Dialogs;
    using global::Microsoft.Bot.Builder.FormFlow;
    using global::Microsoft.Bot.Builder.Luis;
    using global::Microsoft.Bot.Builder.Luis.Models;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    [LuisModel("{luis_app_id}", "{subscription_key}")]
    [Serializable]
    public class LUISDialog : LuisDialog<RoomReservation>
    {
        private readonly BuildFormDelegate<RoomReservation> reserveRoom;

        public LUISDialog(BuildFormDelegate<RoomReservation> reserveRoom)
        {
            this.reserveRoom = reserveRoom;
        }

        [LuisIntent("")]
        public async Task Node(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");

            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("Reservation")]
        public async Task Reservation(IDialogContext context, LuisResult result)
        {
            FormDialog<RoomReservation> enrollmentForm = new FormDialog<RoomReservation>(
                                                                            new RoomReservation(),
                                                                            this.reserveRoom,
                                                                            FormOptions.PromptInStart);

            context.Call<RoomReservation>(enrollmentForm, Callback);
        }

        [LuisIntent("QueryAmenity")]
        public async Task QueryAmenity(IDialogContext context, LuisResult result)
        {
            foreach (EntityRecommendation entity in result.Entities.Where(e => e.Type == "Amenity"))
            {
                string value = entity.Entity.ToLower();

                if(value == "pool" || value == "gym" || value == "wifi" || value == "towels")
                {
                    await context.PostAsync("Yes we have that!");

                    context.Wait(MessageReceived);

                    return;
                }
                else
                {
                    await context.PostAsync("I'm sorry we don't have that.");

                    context.Wait(MessageReceived);

                    return;
                }
            }

            await context.PostAsync("I'm sorry we don't have that.");

            context.Wait(MessageReceived);

            return;
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}