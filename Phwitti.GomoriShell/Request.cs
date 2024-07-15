using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System;

namespace Phwitti.GomoriShell
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonObjectRequestType
    {
        NewGame,
        PlayFirstTurn,
        PlayTurn,
        Bye
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonObjectColor
    {
        [EnumMember(Value = "black")]
        Black,

        [EnumMember(Value = "red")]
        Red
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonObjectSuit
    {
        [EnumMember(Value = "♣")]
        Clubs,

        [EnumMember(Value = "♦")]
        Diamonds,

        [EnumMember(Value = "♥")]
        Hearts,

        [EnumMember(Value = "♠")]
        Spades,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonObjectRank
    {
        [EnumMember(Value = "2")]
        Two,

        [EnumMember(Value = "3")]
        Three,

        [EnumMember(Value = "4")]
        Four,

        [EnumMember(Value = "5")]
        Five,

        [EnumMember(Value = "6")]
        Six,

        [EnumMember(Value = "7")]
        Seven,

        [EnumMember(Value = "8")]
        Eight,

        [EnumMember(Value = "9")]
        Nine,

        [EnumMember(Value = "10")]
        Ten,

        [EnumMember(Value = "J")]
        Jack,

        [EnumMember(Value = "Q")]
        Queen,

        [EnumMember(Value = "K")]
        King,

        [EnumMember(Value = "A")]
        Ace,
    }

    [Serializable]
    public struct JsonObjectCard
    {
        [JsonProperty("suit")]
        public JsonObjectSuit Suit;

        [JsonProperty("rank")]
        public JsonObjectRank Rank;
    }

    [Serializable]
    public struct JsonObjectField
    {
        [JsonProperty("j")]
        public int Column;

        [JsonProperty("i")]
        public int Row;

        [JsonProperty("top_card")]
        public JsonObjectCard? TopCard;

        [JsonProperty("hidden_cards")]
        public JsonObjectCard[] HiddenCards;
    }

    [Serializable]
    public struct JsonObjectRequest
    {
        [JsonProperty("type")]
        public JsonObjectRequestType Type;

        [JsonProperty("color")]
        public JsonObjectColor Color;

        [JsonProperty("cards")]
        public JsonObjectCard[] Cards;

        [JsonProperty("fields")]
        public JsonObjectField[]? Fields;
    }

    [Serializable]
    public struct JsonObjectOkay
    {
    }
}
