using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flow: MonoBehaviour
{
    public string flowName;

    public abstract void StartFlow(); 

    public abstract void ReloadFlow();
    // do we really need this?
    // what does this do? let the game manager be able to help deciding on whether we should go through the start again?
    // but why don't we just make it in the Start in the flow on their own?
}
