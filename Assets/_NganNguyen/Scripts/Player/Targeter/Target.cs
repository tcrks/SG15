using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;
    public bool isThisLowTarget=false;
    public float DistanceToinvestigate;
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
