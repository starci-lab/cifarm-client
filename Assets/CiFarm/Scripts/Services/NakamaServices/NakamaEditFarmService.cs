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

            NakamaUserService.Instance.LoadInventoriesAsync();
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

        }

        public async Task ConstructBuildingRpcAsync(string buildingKey, Position position)
        {
            await NakamaRpcService.Instance.ConstructBuildingRpcAsync(new NakamaRpcService.ConstructBuildingRpcParams
            {
                Key      = buildingKey,
                Position = position
            });
            
            NakamaUserService.Instance.LoadInventoriesAsync();
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

        }
    }
}