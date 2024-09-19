using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using System.Collections;
using UnityEngine;

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
