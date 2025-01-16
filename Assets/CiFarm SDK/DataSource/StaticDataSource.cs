using System;
using System.Collections;
using System.Collections.Generic;
using CiFarm;
using CiFarm.Core.Databases;
using Imba.Utils;
using Newtonsoft.Json;
using SimpleJSON;
using UnityEngine;

namespace CiFarm.Rendering
{
    public class StaticRenderingManager : ManualSingletonMono<StaticRenderingManager>
    {
        public List<AnimalEntity> Animals { get; set; }
        public List<BuildingEntity> Buildings { get; set; }

        private IEnumerator Start()
        {
            // Wait until the user is authenticated
            yield return new WaitUntil(() => CiFarmSDK.Instance.Authenticated);

            FetchAnimalsAsync();
            FetchBuildingsAsync();
        }

        public async void FetchAnimalsAsync()
        {
            Animals = await CiFarmSDK.Instance.GraphQLClient.QueryAnimalsAsync();
        }

        public async void FetchBuildingsAsync()
        {
            Buildings = await CiFarmSDK.Instance.GraphQLClient.QueryBuildingsAsync();
        }
    }
}
