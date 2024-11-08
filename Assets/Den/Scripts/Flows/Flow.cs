using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flow: MonoBehaviour
{
    private new string name;

    public abstract void StartFlow(); 
    // do we really need this?
    // what does this do? let the game manager be able to help deciding on whether we should go through the start again?
    // but why don't we just make it in the Start in the flow on their own?
}
