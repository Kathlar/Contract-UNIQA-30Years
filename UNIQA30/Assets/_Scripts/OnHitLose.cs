using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitLose : MonoBehaviour
{
    public bool freezeTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            player.Lose();
            if (freezeTime) Time.timeScale = 0;
        }
    }
}
