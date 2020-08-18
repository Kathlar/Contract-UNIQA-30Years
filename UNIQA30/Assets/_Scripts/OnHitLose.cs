using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitLose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            player.Lose();
        }
    }
}
