using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CiFarm.Scripts.Services.GameDatas
{
    [CreateAssetMenu(fileName = "ItemDetailConfig", menuName = "GameDatas/ItemDetailConfig", order = 1)]
    public class ItemDetailConfig : ScriptableObject
    {
        [SerializeField] private List<ItemDetailConfigEntity> itemDetail;

        public ItemDetailConfigEntity GetItemDetail(string id)
        {
            return itemDetail.FirstOrDefault(o => o.Key == id);
        }
    }

    [Serializable]
    public class ItemDetailConfigEntity
    {
        [SerializeField] private string   key;
        [SerializeField] private string   itemName;
        [SerializeField] private ItemType type;
        [SerializeField] private string   itemDescription;

        public string Key => key;

        public string ItemName => itemName;

        public ItemType Type => type;

        public string ItemDescription => itemDescription;
    }

    public enum ItemType
    {
        Seed,
        Tile,
        Product,
        Building
    }
}