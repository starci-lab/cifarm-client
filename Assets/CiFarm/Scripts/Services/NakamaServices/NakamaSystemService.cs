using CiFarm.Scripts.Services.NakamaServices.NakamaRawService;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaSystemService : ManualSingletonMono<NakamaSystemService>
    {
        [Header("Activities")]
        [ReadOnly]
        public Activities activities;

        [Header("Rewards")]
        [ReadOnly]
        public Rewards rewards;

        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //load
            LoadActivitiesAsync();
            LoadRewardsAsync();
        }
        public async void LoadActivitiesAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new()
                {
                    Collection = CollectionType.System.GetStringValue(),
                    Key        = SystemKey.Activities.GetStringValue(),
                }
            });
            activities = JsonConvert.DeserializeObject<Activities>(objects.Objects.First().Value);
        }

        public async void LoadRewardsAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new()
                {
                    Collection = CollectionType.System.GetStringValue(),
                    Key        = SystemKey.Rewards.GetStringValue(),
                }
            });
            rewards = JsonConvert.DeserializeObject<Rewards>(objects.Objects.First().Value);
        }
    }
}