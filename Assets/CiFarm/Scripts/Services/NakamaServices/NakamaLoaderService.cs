using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaLoaderService : ManualSingletonMono<NakamaLoaderService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            LoadCropsAsync();
            LoadTilesAsync();
            LoadAnimalsAsync();
        }

        [ReadOnly]
        public List<Crop> crops;

        [ReadOnly]
        public List<Tile> tiles;

        [ReadOnly]
        public List<Animal> animals;

        //load seeds
        public async void LoadCropsAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ListStorageObjectsAsync(session, CollectionType.Crops.GetStringValue(), 20);
            crops = objects.Objects.Select(_object =>
            {
                var seed = JsonConvert.DeserializeObject<Crop>(_object.Value);
                seed.key = _object.Key;
                return seed;
            }).ToList();
            DLogger.Log("Crops loaded", "Nakama - Crops", LogColors.LimeGreen);
        }

        //load tiles
        public async void LoadTilesAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ListStorageObjectsAsync(session, CollectionType.Tiles.GetStringValue(), 20);
            tiles = objects.Objects.Select(_object =>
            {
                var tile = JsonConvert.DeserializeObject<Tile>(_object.Value);
                tile.key = _object.Key;
                return tile;
            }).ToList();
            DLogger.Log("Tiles loaded", "Nakama - Tiles", LogColors.LimeGreen);
        }

        //load animals
        public async void LoadAnimalsAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ListStorageObjectsAsync(session, CollectionType.Animals.GetStringValue(), 20);
            animals = objects.Objects.Select(_object =>
            {
                var tile = JsonConvert.DeserializeObject<Animal>(_object.Value);
                tile.key = _object.Key;
                return tile;
            }).ToList();
            DLogger.Log("Animals loaded", "Nakama - Animals", LogColors.LimeGreen);
        }
    }
}