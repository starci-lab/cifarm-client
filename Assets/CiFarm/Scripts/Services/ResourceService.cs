using CiFarm.Scripts.Services.GameDatas;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.Services
{
    public class ResourceService : ManualSingletonMono<ResourceService>
    {
        [SerializeField] private ModelGameObjectConfig modelGameObjectConfig;

        public ModelGameObjectConfig ModelGameObjectConfig => modelGameObjectConfig;
    }
}