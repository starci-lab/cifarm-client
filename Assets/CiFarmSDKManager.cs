using System.Collections;
using CiFarm;
using Imba.Utils;
using UnityEngine;

public class CiFarmSDKManager : ManualSingletonMono<CiFarmSDKManager>
{
    public IEnumerator Start()
    {
        yield return new WaitUntil(() => CiFarmSDK.Instance != null);
        CiFarmSDK.Instance.Authenticate();
    }
}
