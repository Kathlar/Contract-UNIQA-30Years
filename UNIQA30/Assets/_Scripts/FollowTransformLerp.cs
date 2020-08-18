using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransformLerp : MonoBehaviour
{
    public Transform transformToFollow;

    private Vector3 offset;
    public float regularMoveSpeed;

    public void DoStart()
    {
        started = true;
    }

    bool started;

    private void Start()
    {
        offset = transform.position - transformToFollow.position;
    }

    private void Update()
    {
        if (!started) return;
        regularMoveSpeed = Mathf.Min(1, regularMoveSpeed + Time.deltaTime * .1f);
        Vector3 nextPosition = transform.position;
        nextPosition.y = Mathf.Max(transformToFollow.position.y + offset.y, transform.position.y + regularMoveSpeed);
        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 4);
    }
}
