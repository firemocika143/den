using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public static LanternManager Instance;

    [SerializeField]
    private List<LightOn> lanterns = new List<LightOn>();

    private void Start()
    {
        Instance = this;
    }

    public void AllCastFail()// well, this is quite inefficient for now, the better way is to check the casting ones only, maybe we can do that in PlayerController
    {
        foreach (LightOn l in lanterns)
        {
            if (l.castInProgress)
            {
                l.CastFail();
            }
        }
    }
}
