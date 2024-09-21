using CiFarm.Scripts.Utilities;
using Imba.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaRoadsideShopService : ManualSingletonMono<NakamaRoadsideShopService>
    {
        public UnityAction OnDeliveringProductsUpdated;
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
            var data = await NakamaRpcService.Instance.ListDeliveringProductsRpcAsync();
            deliveringProducts = data.deliveringProducts;
            OnDeliveringProductsUpdated?.Invoke();
            DLogger.Log("Delivering product loaded", "Nakama - Delivering Products", LogColors.LimeGreen);
        }
        
        public async void DeliverProductsAsync(List<NakamaRpcService.InventoryWithIndex> inventoryWithIndexes)
        {   
            await NakamaRpcService.Instance.DeliverProductsRpcAsync(new NakamaRpcService.DeliverProductsRpcAsyncParams
            {
                inventoryWithIndexes = inventoryWithIndexes
            });
            
            DLogger.Log("DeliverProductsAsync", "Nakama - Delivering Products", LogColors.LimeGreen);
            LoadDeliveringProductsAsync();
            NakamaUserService.Instance.LoadInventoriesAsync();
        }
        
        public async void RetainProductsAsync(List<DeliveringProduct> deliveringProduct)
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            
            await NakamaRpcService.Instance.RetainProductsRpcAsync(new NakamaRpcService.RetainProductsRpcAsyncParams
            {
                deliveringProducts = deliveringProduct
            });
            
            DLogger.Log("RetainProductsAsync", "Nakama - Delivering Products", LogColors.LimeGreen);
            LoadDeliveringProductsAsync();
            NakamaUserService.Instance.LoadInventoriesAsync();
        }

    }
}