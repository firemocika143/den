using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(12000)]
public class SceneInfo : MonoBehaviour
{
    public static SceneInfo Instance { get; private set; }

    public Rect worldBound;

    private void Start()
    {
        Instance = this;
    }
}

/// <summary>
/// Stores a variable for global accessibility.
/// </summary>
/// <typeparam name="T">The type of variable to be stored.</typeparam>
public class GlobalVariable<T> : ScriptableObject
{
    [Tooltip("The current value of this variable at runtime.")]
    [SerializeField] protected T runtimeValue;
    /// The current value of the global variable.
    public virtual T Value
    {
        get => runtimeValue;
        set => runtimeValue = value;
    }
}

