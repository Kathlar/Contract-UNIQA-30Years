using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHorizontalRun : PlayerController
{
    public CharacterController controller { get; private set; }

    public Transform groundCheckPoint;
    public float groundCheckRadius = .2f;
    public LayerMask groundMask;
    public float jumpForce = 100;

    private bool isGrounded;

    private float timeOfLAstJump;

    private Vector3 velocity;
    private Vector3 targetVelocity;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        velocity = Physics.gravity;
    }

    protected override void Update()
    {
        base.Update();
        if (isGrounded && Time.timeSinceLevelLoad > timeOfLAstJump + .2f) animator.SetInteger("Jumping", 0);

        if(started)
            animator.SetFloat("Velocity Z", 6);

        Vector3 moveV = transform.forward * moveSpeed;
        if (!started || lost) moveV = Vector3.zero;
        controller.Move((velocity + moveV) * Time.deltaTime);
        velocity = Vector3.Lerp(velocity, Physics.gravity, Time.deltaTime);

        if (!started || lost) return;
        if (isGrounded && spaceButton) Jump();

        moveSpeed += Time.deltaTime * .05f;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius,
            groundMask, QueryTriggerInteraction.Ignore);
    }

    private void Jump()
    {
        animator.SetInteger("Jumping", 1);
        animator.SetTrigger("JumpTrigger");
        velocity.y = jumpForce;
        timeOfLAstJump = Time.timeSinceLevelLoad;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
