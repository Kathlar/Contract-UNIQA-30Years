using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomX : MonoBehaviour
{
    protected float maxX;

    private void Awake()
    {
        maxX = -transform.localPosition.x;
        Vector3 newPos = transform.localPosition;
        newPos.x = Random.Range(transform.position.x, maxX);
        transform.localPosition = newPos;
    }

    private void OnDrawGizmos()
    {
        maxX = -transform.localPosition.x;
        Vector3 otherPos = transform.localPosition;
        otherPos.x = maxX;
        Gizmos.DrawCube(transform.parent.position + otherPos, transform.localScale);
    }
}
