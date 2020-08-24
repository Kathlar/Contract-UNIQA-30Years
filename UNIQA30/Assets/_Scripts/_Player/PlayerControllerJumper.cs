using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJumper : PlayerController
{
    public CharacterController controller { get; private set; }

    private float yVel;

    public Transform groundPoint;
    public bool isGrounded;
    float timeOfLastGrounded;
    public LayerMask groundMask;

    public float jumpPower = 100;
    private float timeOfLAstJump = -1;

    Vector3 targetGFXRot;

    public float rotationSpeed = 10;

    protected override void Awake()
    {
        base.Awake();

        controller = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        if (isGrounded && 
            Time.timeSinceLevelLoad > timeOfLAstJump + .05f) animator.SetInteger("Jumping", 0);


        if (isGrounded && Time.timeSinceLevelLoad > timeOfLAstJump + .1f) yVel = -10;
        else yVel = Mathf.Lerp(yVel, Physics.gravity.y, Time.deltaTime);

        base.Update();

        if (!started) return;

        Vector3 moveVec = Vector3.zero;
        moveVec.x = horizontalValue;

        if (horizontalValue > 0) targetGFXRot = new Vector3(0, 0, 0);
        else if(horizontalValue < 0) targetGFXRot = new Vector3(0, 180, 0);
        acutalGFX.transform.localRotation = Quaternion.Lerp(acutalGFX.transform.localRotation,
            Quaternion.Euler(targetGFXRot), Time.deltaTime * rotationSpeed);

        if (isGrounded && spaceButton)
        {
            yVel = jumpPower;
            timeOfLAstJump = Time.timeSinceLevelLoad;
            animator.SetInteger("Jumping", 1);
            animator.SetTrigger("JumpTrigger");
        }

        animator.SetFloat("Velocity Z", Mathf.Abs(horizontalValue) * 6);

        moveVec *= moveSpeed;
        moveVec.y = yVel;
        controller.Move(moveVec * Time.deltaTime);

        if (Physics.CheckSphere(groundPoint.position, .1f, groundMask, QueryTriggerInteraction.Collide) && yVel <= 0)
            isGrounded = true;
        else isGrounded = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundPoint.position, .1f);
    }
}
