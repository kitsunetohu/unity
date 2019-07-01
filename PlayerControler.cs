using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControler : MonoBehaviour
{

    public enum PlayerActionState
    {
        START,
        WAITING,
        MOVING
    }
    public float speed = 8.0f;
    NavMeshAgent navAgent;
    Animator animator;
    PlayerActionState _CurrentPlayerActionState = PlayerActionState.START;
	
    public PlayerActionState CurrentPlayerActionState
    {
        get { return _CurrentPlayerActionState; }
        private set { _CurrentPlayerActionState = value; }
    }

    // Use this for initialization
    void Start()
    {

        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navAgent.speed = speed;
        UpdateActionState(PlayerActionState.WAITING);
        StartCoroutine("AnimationStateMachine");//开始监控player状态
        MouseManager.Instance.OnClickEnviorment.AddListener(HandleClickEnviorment);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MovwDirection=new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        transform.position=transform.position+MovwDirection*speed * Time.deltaTime;
    }

    void HandleClickEnviorment(Vector3 hitPoint){
        navAgent.isStopped = false;
        navAgent.SetDestination(hitPoint);

    }
    //开新线程，监控状态
    IEnumerator AnimationStateMachine()
    {
		PlayerActionState state=PlayerActionState.WAITING;
        while (Application.isPlaying)
        {

            if (!Input.anyKey || Vector3.Magnitude(navAgent.velocity) == 0)
            {
                 state = PlayerActionState.WAITING;

            }
            //if moving
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 || Vector3.Magnitude(navAgent.velocity) > 0)
            {
                if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                    navAgent.isStopped = true;

                transform.LookAt(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
                state = PlayerActionState.MOVING;

            }
            UpdateActionState(state);

            yield return null;
        }
    }

    void UpdateActionState(PlayerActionState state)
    {
        PlayerActionState previousActionState = _CurrentPlayerActionState;
        _CurrentPlayerActionState = state;
        if (previousActionState == state) return;
        switch (CurrentPlayerActionState)
        {
            case PlayerActionState.WAITING:
                animator.SetBool("isMoving", false);
                //repate watting Anition
                InvokeRepeating("watingAnmation", 0, 8.0f);
                break;

            case PlayerActionState.MOVING:
                if (previousActionState == PlayerActionState.WAITING)
                {
                    CancelInvoke("watingAnmation");
                }
                animator.SetBool("isMoving", true);
                break;
        }

    }
    void watingAnmation()
    {
        //隔一段时间切换waiting动画
        animator.ResetTrigger("wait01");
        animator.ResetTrigger("wait02");
        float a = Random.Range(0.0f, 1.0f);
        if (a > 0.5)
        {
            animator.SetTrigger("wait01");
        }
        else animator.SetTrigger("wait02");
    }



}

