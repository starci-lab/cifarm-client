using System.Threading.Tasks;
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
    }
}