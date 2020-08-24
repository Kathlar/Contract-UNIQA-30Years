using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFPS : PlayerController
{
    public CharacterController controller { get; private set; }
    public Transform cameraTransform;

    public float rotationSpeed = 5;
    float yVel;

    public Transform groundPoint;
    public bool isGrounded;
    float timeOfLastGrounded;
    public LayerMask groundMask;

    public float jumpPower = 100;
    private float timeOfLAstJump = -1;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected override void Update()
    {
        if (Physics.CheckSphere(groundPoint.position, .1f, groundMask, QueryTriggerInteraction.Ignore) && yVel <= 0)
            isGrounded = true;
        else isGrounded = false;

        if (isGrounded && Time.timeSinceLevelLoad > timeOfLAstJump + .1f) yVel = Physics.gravity.y;
        else yVel = Mathf.Lerp(yVel, Physics.gravity.y, Time.deltaTime);

        base.Update();

        float mouseX = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, mouseX * Time.deltaTime * rotationSpeed, 0);

        if (isGrounded && spaceButton)
        {
            yVel = jumpPower;
            timeOfLAstJump = Time.timeSinceLevelLoad;
        }

        Vector3 moveVec = Vector3.zero;
        moveVec += transform.forward * moveSpeed * vertical;
        moveVec += transform.right * moveSpeed * horizontalValue;
        moveVec.y = yVel;
        controller.Move(moveVec * Time.deltaTime);

    }
}
