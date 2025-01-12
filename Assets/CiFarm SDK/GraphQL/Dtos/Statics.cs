using System;

namespace CiFarm.GraphQL
{
    [Serializable]
    public class GetCropsArgs : GetManyArgs { }

    [Serializable]
    public class GetAnimalsArgs : GetManyArgs { }

    [Serializable]
    public class GetBuildingsArgs : GetManyArgs { }

    [Serializable]
    public class GetTilesArgs : GetManyArgs { }

    [Serializable]
    public class GetSuppliesArgs : GetManyArgs { }

    [Serializable]
    public class GetDailyRewardsArgs : GetManyArgs { }
}
