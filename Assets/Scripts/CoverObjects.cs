using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObjects : MonoBehaviour
{
    public Cover cover;

    public delegate void OnScatter();
    public OnScatter onScatter;

    public delegate void OnResetObjects();
    public OnResetObjects onResetObjects;
    // Start is called before the first frame update
    void Start()
    {
        cover.onCoverDestroy += ScatterCoverObjects;
        cover.onCoverReset += ResetCoverObjects;
    }
    public void ScatterCoverObjects()
    {
        onScatter?.Invoke();
    }
    public void ResetCoverObjects()
    {
        onResetObjects?.Invoke();
    }
}
