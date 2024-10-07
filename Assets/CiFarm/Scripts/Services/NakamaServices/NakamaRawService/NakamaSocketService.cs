using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.Services.NakamaServices.NakamaRawService
{
    public class NakamaSocketService : ManualSingletonMono<NakamaSocketService>
    {
        public UnityAction OnFetchPlacedDataFromServer;
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            InitializeAsync();

            yield return new WaitUntil(() => initialized);
            //register event
            centralSocket.ReceivedMatchState += (matchState) =>
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
            };
            timerSocket.ReceivedMatchState += (matchState) =>
            {
                var opCode = (OpCode)matchState.OpCode;
                switch (opCode)
                {
                    case OpCode.NextDeliveryTime:
                        {
                            OnNextDeliveryTimeStateReceived(matchState);
                            break;
                        }
                }
            };
        }

        public async Task ForceCentralBroadcastInstantlyRpcAsync()
        {
            await NakamaRpcService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
        }

        [Tooltip("Enable to display logs for updates on the state of placed items. Disable to hide these logs.")]
        [SerializeField]
        private bool debugPlacedItems = true;

        [ReadOnly]
        public List<PlacedItem> placedItems;

        [ReadOnly]
        public long nextDeliveryTime;

        private void OnPlacedItemsStateReceived(IMatchState matchState)
        {
            var content = Encoding.UTF8.GetString(matchState.State);
            placedItems = JsonConvert.DeserializeObject<PlacedItems>(content).placedItems;
            if (debugPlacedItems)
            {
//                DLogger.Log($"{placedItems.Count} placed items loaded. See the inspector for details.", "Nakama - Placed Items State", LogColors.Aquamarine);
            }

            OnFetchPlacedDataFromServer?.Invoke();
        }

        private void OnNextDeliveryTimeStateReceived(IMatchState matchState)
        {
            var content = Encoding.UTF8.GetString(matchState.State);
            nextDeliveryTime = JsonConvert.DeserializeObject<NextDeliveryTime>(content).time;
            if (debugPlacedItems)
            {
//                DLogger.Log($"Next delivery time loaded. See the inspector for details.", "Nakama - Next Delivery Time State", LogColors.Aquamarine);
            }
        }

        [HideInInspector]

        public ISocket centralSocket = null;
        public ISocket timerSocket = null;

        private bool initialized = false;
        private async void InitializeAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            //connect to socket
            centralSocket = client.NewSocket(true);
            timerSocket = client.NewSocket(true);
            await Task.WhenAll(
                centralSocket.ConnectAsync(session),
                timerSocket.ConnectAsync(session)
            );

            initialized = true;

            var objects = await client.ReadStorageObjectsAsync(session, new Nakama.IApiReadStorageObjectId[]
            {
                new StorageObjectId()
                {
                    Collection = CollectionType.System.GetStringValue(),
                    Key = SystemKey.MatchInfo.GetStringValue(),

                }
            });

            var matchInfo = JsonConvert.DeserializeObject<MatchInfo>(objects.Objects.First().Value);
            var centralMatchId = matchInfo.centralMatchId;
            var timerMatchId = matchInfo.timerMatchId;

            //join
            await Task.WhenAll(
                centralSocket.JoinMatchAsync(centralMatchId),
                timerSocket.JoinMatchAsync(timerMatchId)
            );
        }
    }
}