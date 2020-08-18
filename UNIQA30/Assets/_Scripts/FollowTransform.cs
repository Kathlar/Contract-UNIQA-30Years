using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform transformToFollow;

    public bool x, y, z;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - transformToFollow.position;
    }

    private void Update()
    {
        Vector3 nextPosition = transform.position;
        if (x) nextPosition.x = transformToFollow.position.x + offset.x;
        if (y) nextPosition.y = transformToFollow.position.y + offset.y;
        if (z) nextPosition.z = transformToFollow.position.z + offset.z;
        transform.position = nextPosition;
    }
}
