using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointContainer : MonoBehaviour
{
    public AudioSource audioSource { get; private set; }

    public int pointsCount = 10;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerPoints points))
        {
            if (audioSource)
            {
                audioSource.transform.SetParent(null);
                audioSource.Play();
                Destroy(audioSource.gameObject, audioSource.clip.length);
            }
            points.CollectPoints(pointsCount);
            Destroy(gameObject);
        }
    }
}
