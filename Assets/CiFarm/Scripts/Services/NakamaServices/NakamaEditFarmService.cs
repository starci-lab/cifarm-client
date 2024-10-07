using System.Threading.Tasks;
using CiFarm.Scripts.Services.NakamaServices.NakamaRawService;
using Imba.Utils;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaEditFarmService : ManualSingletonMono<NakamaEditFarmService>
    {
        public async Task PlaceTileRpcAsync(string inventoryTileKey, Position position)
        {
            await NakamaRpcService.Instance.PlaceTileRpcAsync(new NakamaRpcService.PlaceTileRpcAsyncParams
            {
                inventoryTileKey = inventoryTileKey,
                position         = position
            });

            var loadInventoriesTask = NakamaUserService.Instance.LoadInventoriesAsync();
            var broadcastTask       = NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

            await Task.WhenAll(loadInventoriesTask, broadcastTask);
        }

        public async Task ConstructBuildingRpcAsync(string buildingKey, Position position)
        {
            await NakamaRpcService.Instance.ConstructBuildingRpcAsync(new NakamaRpcService.ConstructBuildingRpcParams
            {
                Key      = buildingKey,
                Position = position
            });
            
            var loadInventoriesTask = NakamaUserService.Instance.LoadInventoriesAsync();
            var broadcastTask       = NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

            await Task.WhenAll(loadInventoriesTask, broadcastTask);
        }
    }
}