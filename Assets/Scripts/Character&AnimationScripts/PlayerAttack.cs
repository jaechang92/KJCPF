using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateMachineBehaviour
{
    public int attackAniMaxCount;
    CharacterInput characterInput = null;
    private int attackIndex = 2;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterInput == null)
        {
            characterInput = animator.GetComponent<CharacterInput>();
        }

        if (attackIndex == attackAniMaxCount)
        {
            attackIndex = 0;
        }

        characterInput.isAttack = true;
        characterInput.trail.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("AttackIndex", attackIndex);
    }


    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < GameManager.instance.nowTraceMonsterList.Count; i++)
        {
            if (Vector3.Distance(GameManager.instance.player.transform.position, GameManager.instance.nowTraceMonsterList[i].transform.position) < GameManager.instance.userStats.uStats.AttackDistance)
            {
                GameManager.instance.nowTraceMonsterList[i].Hit(GameManager.instance.userStats.uStats.Attack);
            }
        }

        attackIndex++;
        characterInput.isAttack = false;
        characterInput.trail.enabled = false;
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
