using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerIcyTower : PlayerController
{
    public CharacterController controller { get; private set; }

    public Transform groundPoint;

    bool isGrounded;
    float timeOfLastGrounded;
    public LayerMask groundMask;

    public Vector3 velocity;
    private float timeOfLAstJump = -1;
    bool isJumping;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();
        if (lost || !started) return;
        if (Time.timeSinceLevelLoad < timeOfLastGrounded + .15f) isGrounded = true;
        else isGrounded = false;

        if (isGrounded && Time.timeSinceLevelLoad > timeOfLAstJump + .1f) velocity.y = 0;
        else velocity.y = Mathf.Lerp(velocity.y, Physics.gravity.y, Time.deltaTime * 3);

        float targetXVel = horizontalValue * 5;
        if (isGrounded) targetXVel *= 2;
        velocity.x = Mathf.Lerp(velocity.x, targetXVel, Time.deltaTime * 10);

        if (spaceButton && isGrounded && !isJumping)
        {
            isJumping = true;
            StartCoroutine(Jumping());
            timeOfLAstJump = Time.timeSinceLevelLoad;
        }

        controller.Move(velocity * Time.deltaTime);

        if (Physics.CheckSphere(groundPoint.position, .1f, groundMask, QueryTriggerInteraction.Collide) && velocity.y <= 0)
            timeOfLastGrounded = Time.timeSinceLevelLoad;
    }

    IEnumerator Jumping()
    {
        float timing = 0;
        do
        {
            timing += Time.deltaTime;
            velocity.y = Mathf.Lerp(velocity.y, 20, Time.deltaTime * 50);
            yield return null;
        }
        while (timing < .22f);
        isJumping = false;
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(groundPoint.position, .1f, groundMask, QueryTriggerInteraction.Collide) && velocity.y <= 0)
            timeOfLastGrounded = Time.timeSinceLevelLoad;
    }
}
