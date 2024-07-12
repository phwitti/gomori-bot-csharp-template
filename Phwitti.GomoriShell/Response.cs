using Newtonsoft.Json;
using System;

namespace Phwitti.GomoriShell
{
    [Serializable]
    public struct JsonObjectResponse
    {
        [JsonProperty("j")]
        public int Column;

        [JsonProperty("i")]
        public int Row;

        [JsonProperty("card")]
        public JsonObjectCard Card;
        
        [JsonProperty("target_field_for_king_ability")]
        public int[]? TargetFieldForKingAbility;

        public static object[] Okay
            => [];
    }
}
