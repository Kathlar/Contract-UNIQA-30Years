using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    private Vector3 startPos;
    bool goingBack;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Vector3 targetPos = startPos;
        targetPos.y += goingBack ? -.2f : .2f;
        transform.position += (targetPos - transform.position).normalized * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPos) < .05f) goingBack = !goingBack;
    }
}
