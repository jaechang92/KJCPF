using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : StateMachineBehaviour
{
    public float startTime;
    public GameObject Effect;
    public Transform tr;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tr == null)
        {
            tr = animator.GetComponent<BossMonster>().Tongue;
        }

        startTime = 0.35f;
        currentTime = 0;
        count = 0;
    }

    private float currentTime = 0;
    private float count = 0;
    private float attackDistance = 16.0f;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;
        if (currentTime>startTime && count == 0)
        {
            GameObject ef = Instantiate(Effect, tr.position, Quaternion.identity, tr);
            ef.transform.forward = animator.transform.forward;
            count++;
        }
        Vector3 nv = tr.forward;
        nv.y = 0;
        Vector3 np = tr.position;
        np.y = GameManager.instance.player.transform.position.y + 0.5f;
        Debug.DrawRay(np, nv * attackDistance, Color.red);
        if (currentTime > startTime)
        {
            if (Physics.Raycast(np, nv, attackDistance, LayerMask.GetMask("Player")) && count <= 1)
            {
                Debug.Log("불공격");
                count++;
            }
        }
        //float dist = Vector3.Distance(tr.position, GameManager.instance.player.transform.position);
        //if (currentTime > startTime && dist < attackDistance && count <= 1)
        //{
        //    // tr의 forward에서 일정 각도 안에 들어왔는가?

        //    //Vector3 nPosition = new Vector3(tr.forward.x * dist, GameManager.instance.player.transform.position.y,tr.forward.z * dist);
        //    //Debug.Log(nPosition);
        //    //if (GameManager.instance.player.skinnedMesh.bounds.Contains())
        //    //{
        //    //    Debug.Log("불공격");
        //    //    UIManager.instance.userInfo.uStats.Hp -= 50;
        //    //    UIManager.instance.SetHpMpUI();
        //    //    count++;
        //    //}

        //    Debug.DrawRay(tr.position, tr.forward * 100 ,Color.red);
        //    if (Physics.Raycast(tr.position, tr.forward, attackDistance,LayerMask.GetMask("Player")))
        //    {
        //        Debug.Log("불공격");
        //    }

        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (Vector3.Distance(tr.position, GameManager.instance.player.transform.position) < attackDistance )
        //{
        //    Debug.Log("불공격");
        //    UIManager.instance.userInfo.uStats.Hp -= 50;
        //    UIManager.instance.SetHpMpUI();
        //}
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
