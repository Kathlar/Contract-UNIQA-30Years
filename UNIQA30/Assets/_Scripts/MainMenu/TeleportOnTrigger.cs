using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTo;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportTo.position;
    }
}
