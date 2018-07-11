
namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Modules
{
    using Autofac;
    using Dialogs;
    using global::Microsoft.Bot.Builder.Dialogs;
    using global::Microsoft.Bot.Builder.Internals.Fibers;
    using global::Microsoft.Bot.Builder.Luis;
    using global::Microsoft.Bot.Connector;
    using System.Configuration;

    internal sealed class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new LuisModelAttribute(ConfigurationManager.AppSettings["luis:ModelId"],
                ConfigurationManager.AppSettings["luis:SubscriptionId"])).AsSelf().AsImplementedInterfaces().SingleInstance();

            // Top Level Dialog
            builder.RegisterType<LUISDialog>().As<IDialog<IMessageActivity>>().InstancePerDependency();

            // Singlton services
            builder.RegisterType<LuisService>().Keyed<ILuisService>(FiberModule.Key_DoNotSerialize).AsImplementedInterfaces().SingleInstance();
        }
    }
}