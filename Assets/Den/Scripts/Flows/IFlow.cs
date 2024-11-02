using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlow
{
    public string name { get; }

    public void StartFlow();
}
