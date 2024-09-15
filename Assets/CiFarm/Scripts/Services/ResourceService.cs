using CiFarm.Scripts.Services.GameDatas;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.Services
{
    public class ResourceService : ManualSingletonMono<ResourceService>
    {
        [SerializeField] private ModelGameObjectConfig modelGameObjectConfig;
        [SerializeField] private ItemDetailConfig itemDetailConfig;
        [SerializeField] private ShopSellConfig shopSellConfig;

        public ModelGameObjectConfig ModelGameObjectConfig => modelGameObjectConfig;

        public ItemDetailConfig ItemDetailConfig => itemDetailConfig;

        public ShopSellConfig ShopSellConfig => shopSellConfig;
    }
}