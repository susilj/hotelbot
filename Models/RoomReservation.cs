
namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Models
{
    using global::Microsoft.Bot.Builder.FormFlow;
    using System;
    using System.Collections.Generic;

    public enum BedSizeOptions
    {
        King,
        Queen,
        Double,
        Single
    }

    public enum AmenitiesOptions
    {
        Kitchen,
        ExtraTowels,
        GymAccess,
        Wifi
    }
    [Serializable]
    public sealed class RoomReservation
    {
        public BedSizeOptions? BedSize;
        public int? NumberOfOccupants;
        public DateTime? CheckInDate;
        public int? NumberOfDaysToStay;
        public List<AmenitiesOptions> Amenities;

        public static IForm<RoomReservation> BuildForm()
        {
            return new FormBuilder<RoomReservation>()
                .Message("Welcome to the hotel reservation bot!")
                .Build();
        }
    }
}