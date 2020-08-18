using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 5;

    private void Start()
    {
        Invoke("DoDestroy", time);
    }

    void DoDestroy()
    {
        Destroy(gameObject);
    }
}
