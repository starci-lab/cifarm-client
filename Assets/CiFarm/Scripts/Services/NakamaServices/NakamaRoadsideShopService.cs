using CiFarm.Scripts.Utilities;
using Codice.CM.Common;
using Imba.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaRoadsideShopService : ManualSingletonMono<NakamaRoadsideShopService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //load
            LoadDeliveringProductsAsync();
        }
        [ReadOnly]   
        public List<DeliveringProduct> deliveringProducts;

        public async void LoadDeliveringProductsAsync()
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var data = await NakamaRpcService.Instance.ListDeliveringProductsRpcAsync();
            deliveringProducts = data.deliveringProducts;
            DLogger.Log("Delivering product loaded", "Nakama - Delivering Products", LogColors.LimeGreen);
        }
    }
}