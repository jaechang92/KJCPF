using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : StateMachineBehaviour
{
    MonsterAI monsterAI = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI == null)
        {
            monsterAI = animator.GetComponent<MonsterAI>();
            Init();
        }
        GameManager.instance.nowTraceMonsterList.Remove(monsterAI);

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RandomState(monsterAI.randomTime,animator);

        // 타겟의 거리가 추적 거리보다 가까울때
        if (monsterAI.DISTANCETOTARGET < monsterAI.traceDistance)
        {
            animator.SetTrigger("RunTrigger");
            animator.SetBool("Run", true);
        }


        if (monsterAI.DISTANCETOTARGET < monsterAI.attackDistance)
        {
            animator.SetBool("Attack", true);
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

    void Init()
    {
        monsterAI.currnetTime = 0;
        monsterAI.randomTime = 2.0f;
        monsterAI.randomAreaSize = 5;
        monsterAI.traceDistance = 10;
        monsterAI.attackDistance = 2.0f;
        //Debug.Log(monsterAI.attackDistance);
        //Debug.Log(monsterAI.agent);
        monsterAI.agent.stoppingDistance = monsterAI.attackDistance;
    }
    
    void RandomState(float randomTime, Animator animator)
    {
        monsterAI.currnetTime += Time.deltaTime;
        if (monsterAI.currnetTime > randomTime)
        {
            monsterAI.currnetTime = 0;
            if (Random.Range(0,1.0f) > 0.5f)
            {
                //상태전이
                animator.SetBool("Walk",true);
            }
        }
    }



}
