using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalk : StateMachineBehaviour
{
    MonsterAI monsterAI = null;

    float currentTime = 0;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monsterAI == null)
        {
            monsterAI = animator.GetComponent<MonsterAI>();
        }

        float a = MinMax(monsterAI.randomAreaSize - 2, monsterAI.randomAreaSize);
        float b = MinMax(monsterAI.randomAreaSize - 2, monsterAI.randomAreaSize);
        
        //monsterAI.RandomSize();
        monsterAI.originPosition = monsterAI.transform.position + new Vector3(a, 0, b) ;
        currentTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monsterAI.agent.destination = monsterAI.originPosition;
        currentTime += Time.deltaTime;
        if (Vector3.Distance(monsterAI.originPosition, monsterAI.transform.position) <= monsterAI.agent.stoppingDistance || currentTime >= 3.0f)
        {
            animator.SetBool("Walk", false);
        }

        // 타겟의 거리가 추적 거리보다 가까울때
        if (monsterAI.DISTANCETOTARGET < monsterAI.traceDistance)
        {
            animator.SetTrigger("RunTrigger");
            //animator.SetBool("Run", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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

    public int MinMax(int min,int max)
    {
        int randomValue;
        randomValue = Random.Range(-max, max + 1);
        if (Mathf.Abs(randomValue) < min)
        {
            randomValue = MinMax(min, max);
        }
        return randomValue;
    }
    
    
    


}
