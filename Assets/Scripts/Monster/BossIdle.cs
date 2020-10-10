using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    MonsterAI monsterAI = null;
    public float delayTime;
    public float currentTime = 0;
    int index = 0;
    void Init()
    {
        monsterAI.currnetTime = 0;
        monsterAI.randomTime = 2.0f;
        monsterAI.randomAreaSize = 5;
        monsterAI.traceDistance = 50;
        monsterAI.attackDistance = 25.0f;
        //Debug.Log(monsterAI.attackDistance);
        //Debug.Log(monsterAI.agent);
        monsterAI.agent.stoppingDistance = monsterAI.attackDistance;
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI == null)
        {
            monsterAI = animator.GetComponent<MonsterAI>();
            Init();
        }
        GameManager.instance.nowTraceMonsterList.Remove(monsterAI);
        delayTime = 2.0f;
        currentTime = 0;
        animator.SetBool("Attack", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;
        if (monsterAI.DISTANCETOTARGET < monsterAI.attackDistance)
        {
            if (currentTime >= delayTime)
            {
                // 어택
                animator.SetBool("Attack", true);
                animator.SetInteger("AttackIndex", RandomAttackIndex());
            }
            

        }
        else if(monsterAI.DISTANCETOTARGET < monsterAI.traceDistance)
        { // 타겟의 거리가 추적 거리보다 가까울때
            //animator.SetTrigger("RunTrigger");
            animator.SetBool("Trace", true);
            animator.SetBool("Attack", false);
        }
        

    }

    int RandomAttackIndex()
    {
        index = Random.Range(0, 4);
        return index;
        //return 2;
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
