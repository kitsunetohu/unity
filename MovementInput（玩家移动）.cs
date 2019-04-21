using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirction;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed=10;
    public Animator anim;
    public float speed;//用来检测速度是否达到阈值
    public float moveSpeed=1;//移动的速度
    public float allowPlayerRotation = 0;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    float verticalVel;

    Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ = Input.GetAxisRaw("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirction = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirction), desiredRotationSpeed);
        }
        controller.Move(desiredMoveDirction*0.1f*moveSpeed);
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat("InputZ",InputZ,0.0f,Time.deltaTime*2f);
        //anim.SetFloat("InputX",InputX,0.0f,Time.deltaTime*2f);

        speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (speed > allowPlayerRotation)
        {
            //anim.SetFloat("InputMagnitude",speed,0.0f,Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            //anim.SetFloat("InputMagnitude",speed,0.0f,Time.deltaTime);
        }


    }
}
