using System;
using System.Collections.Generic;
using UnityEngine;

namespace CiFarm.Scripts.Services.GameDatas
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "GameDatas/ShopConfig", order = 1)]
    public class ShopSellConfig : ScriptableObject
    {
        [SerializeField] private List<ShopConfigEntity> sellingItemsConfig;
    }

    [Serializable]
    public class ShopConfigEntity
    {
        [SerializeField] private string key;
        [SerializeField] private string itemName;
        [SerializeField] private int    sellValue;

        public string Key       => key;
        public string ItemName  => itemName;
        public int    SellValue => sellValue;
    }
}