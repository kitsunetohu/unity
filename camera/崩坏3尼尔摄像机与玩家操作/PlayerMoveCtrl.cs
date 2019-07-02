using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveCtrl : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    public Camera camFlowPlayer;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        if (camFlowPlayer == null)
        {
            camFlowPlayer = Camera.main;
        }
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        

        Vector3 verX=camFlowPlayer.transform.right;
        Vector3 verZ=camFlowPlayer.transform.forward;
        verX.y=0;
        verZ.y=0;

        _inputs = Vector3.zero;
        
        _inputs += 1.3f*Input.GetAxis("Horizontal") * verX;//如果围绕摄像机旋转的话 水平方向要稍快一点
        _inputs += Input.GetAxis("Vertical")* verZ;
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;
        
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        
    }
}
