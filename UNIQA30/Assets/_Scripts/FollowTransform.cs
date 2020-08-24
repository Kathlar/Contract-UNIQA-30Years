using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform transformToFollow;

    public bool x, y, z;
    public Vector2 clampX, clampY, clampZ;
    private bool doClampX, doClampY, doClampZ;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - transformToFollow.position;
        doClampX = clampX.y > clampX.x;
        doClampY = clampY.y > clampY.x;
        doClampZ = clampZ.y > clampZ.x;
    }

    private void Update()
    {
        Vector3 nextPosition = transform.position;
        if (x) nextPosition.x = transformToFollow.position.x + offset.x;
        if (doClampX) nextPosition.x = Mathf.Clamp(nextPosition.x, clampX.x, clampX.y);
        if (y) nextPosition.y = transformToFollow.position.y + offset.y;
        if (doClampY) nextPosition.y = Mathf.Clamp(nextPosition.y, clampY.x, clampY.y);
        if (z) nextPosition.z = transformToFollow.position.z + offset.z;
        if (doClampZ) nextPosition.z = Mathf.Clamp(nextPosition.z, clampZ.x, clampZ.y);
        transform.position = nextPosition;
    }
}
