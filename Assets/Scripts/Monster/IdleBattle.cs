using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattle : StateMachineBehaviour
{
    MonsterAI monsterAI;

    float currentTime = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI == null)
        {
            monsterAI = animator.GetComponent<MonsterAI>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI.DISTANCETOTARGET > monsterAI.traceDistance)
        {// 추적 놓침
            animator.SetBool("Run", false);
            animator.SetBool("IsBattle", false);
        }
        else if (monsterAI.DISTANCETOTARGET > monsterAI.attackDistance)
        {// 어텍사거리 보다 멀어져서 뛰어감
            animator.SetBool("Run", true);
            animator.SetBool("IsBattle", true);
        }

        AttackDelay(animator);

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


    private void AttackDelay(Animator animator)
    {
        currentTime += Time.deltaTime;
        if (currentTime > monsterAI.attackDelay)
        {
            
            currentTime = 0;
            animator.SetTrigger("AttackTrigger");
            animator.SetInteger("AttackIndex", Random.Range(0, animator.GetInteger("AttackListCount")+1));
        }
    }

}
