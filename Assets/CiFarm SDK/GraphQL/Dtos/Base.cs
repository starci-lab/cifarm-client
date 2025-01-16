using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.GraphQL
{
    [Serializable]
    public class GetManyArgs
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }

    [Serializable]
    public class GraphQLVariables<TArgs>
    {
        [JsonProperty("args")]
        public TArgs Args { get; set; }
    }
}
