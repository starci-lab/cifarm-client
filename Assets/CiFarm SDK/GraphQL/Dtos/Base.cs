using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.GraphQL
{
    [Serializable]
    public class GetManyArgs
    {
        [SerializeField]
        private int _limit;

        [JsonProperty("limit")]
        public int Limit
        {
            get => _limit;
            set => _limit = value;
        }

        [SerializeField]
        private int _offset;

        [JsonProperty("offset")]
        public int Offset
        {
            get => _offset;
            set => _offset = value;
        }
    }

    [Serializable]
    public class GraphQLVariables<TArgs>
    {
        [SerializeField]
        private TArgs _args;

        [JsonProperty("args")]
        public TArgs Args
        {
            get => _args;
            set => _args = value;
        }
    }
}
