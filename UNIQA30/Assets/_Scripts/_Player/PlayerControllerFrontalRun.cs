using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFrontalRun : PlayerController
{
    public CharacterController controller { get; private set; }

    private Vector3 velocityZ;

    private int pos = 0;
    private float timeOfLastMove;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();
        if (started)
            animator.SetFloat("Velocity Z", 6);

        moveSpeed += Time.deltaTime * .05f;

        if (!started || lost) return;
        velocityZ = transform.forward * moveSpeed;
        Vector3 velocity = (velocityZ) * Time.deltaTime;
        controller.Move(velocity);

        if(Mathf.Abs(horizontalValue) > .2f && Time.timeSinceLevelLoad > timeOfLastMove + .5f)
        {
            int newMove = pos + (horizontalValue > 0 ? 1 : -1);
            if(Mathf.Abs(newMove) < 2)
            {
                timeOfLastMove = Time.timeSinceLevelLoad;
                pos = newMove;
            }
        }
        Vector3 targetV = transform.position;
        targetV.x = 2.5f * pos;
        if (Vector3.Distance(transform.position, targetV) > .5f) animator.SetFloat("Velocity X", 
            6 * Mathf.Sign(pos));
        else animator.SetFloat("Velocity X", 0);
        transform.position = Vector3.Lerp(transform.position, targetV, Time.deltaTime * moveSpeed);
    }
}
