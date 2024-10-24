using System.Collections;
using CiFarm.Scripts.Services.NakamaServices.NakamaRawService;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaNftService : ManualSingletonMono<NakamaNftService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);
        }
    }
}
