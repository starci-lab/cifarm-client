using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using System.Collections.Generic;

namespace CiFarm.Scripts.Services.NakamaBulletinService
{
    public class NakamaBulletinService : ManualSingletonMono<NakamaBulletinService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        [ReadOnly]
        public List<DailyReward> dailyRewards;
    }
}