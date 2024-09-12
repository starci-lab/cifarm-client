using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaSocketService : ManualSingletonMono<NakamaSocketService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            Initialize();

            yield return new WaitUntil(() => initialized);
            //register event
            socket.ReceivedMatchState += OnReceivedMatchState;
        }

        [Tooltip("Enable to display logs for updates on the state of placed items. Disable to hide these logs.")]
        [SerializeField]
        private bool debugPlacedItems = true;

        [ReadOnly]
        public List<PlacedItem> placedItems;

        private void OnReceivedMatchState(IMatchState matchState)
        {
            var opCode = (OpCode)matchState.OpCode;
            switch (opCode)
            {
                case OpCode.PlacedItems:
                    {
                        OnPlacedItemsStateReceived(matchState);
                        break;
                    }
            }
        }

        private void OnPlacedItemsStateReceived(IMatchState matchState)
        {
            var content = Encoding.UTF8.GetString(matchState.State);
            placedItems = JsonConvert.DeserializeObject<PlacedItems>(content).placedItems;
            if (debugPlacedItems)
            {
                DLogger.Log($"{placedItems.Count} placed items loaded. See the inspector for details.", "Nakama - Placed Items State", LogColors.Aquamarine);
            }
        }

        [HideInInspector]

        public ISocket socket = null;

        private bool initialized = false;
        private async void Initialize()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            //connect to socket
            socket = client.NewSocket(true);
            await socket.ConnectAsync(session);
            initialized = true;

            var objects = await client.ReadStorageObjectsAsync(session, new Nakama.IApiReadStorageObjectId[]
            {
                new StorageObjectId()
                {
                    Collection = CollectionType.System.GetStringValue(),
                    Key = SystemKey.CentralMatchInfo.GetStringValue(),
                }
            });

            var centralMatchInfo = JsonConvert.DeserializeObject<CentralMatchInfo>(objects.Objects.First().Value);
            var matchId = centralMatchInfo.matchId;

            //join
            await socket.JoinMatchAsync(matchId);
        }
    }
}