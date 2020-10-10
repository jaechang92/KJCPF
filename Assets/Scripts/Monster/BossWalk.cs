using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    MonsterAI monsterAI = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI == null)
        {
            monsterAI = animator.GetComponent<MonsterAI>();
        }

        GameManager.instance.nowTraceMonsterList.Add(monsterAI);
        monsterAI.agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monsterAI.agent.destination = GameManager.instance.player.transform.position;

        // 타겟의 거리가 공격 거리보다 가까울때
        if (monsterAI.DISTANCETOTARGET < monsterAI.attackDistance)
        {
            animator.SetBool("Trace", false);
        }

        // 타겟의 거리가 추적 거리보다 멀때
        if (monsterAI.DISTANCETOTARGET > monsterAI.traceDistance)
        {
            animator.SetBool("Trace", false);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
