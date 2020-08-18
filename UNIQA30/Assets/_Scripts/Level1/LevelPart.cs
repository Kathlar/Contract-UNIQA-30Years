using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    private LevelGenerator generator;
    bool generated;

    private void Awake()
    {
        generator = transform.parent.GetComponent<LevelGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (generated) return;
        if (other.GetComponent<PlayerController>())
        {
            generated = true;
            generator.NextPart();
        }
    }
}
