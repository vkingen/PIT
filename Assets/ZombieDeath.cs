using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : StateMachineBehaviour
{
    private EnemyController enemy;
    private FieldOfView fov;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ResetAllBools(animator);
        enemy = animator.gameObject.GetComponentInParent<EnemyController>();
        fov = enemy.fov;

        if (fov != null)
        {
            enemy.agent.speed = 0;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fov == null) return;

        //if (fov.attackTarget == null)
        //{
        //    animator.SetTrigger("Death");
        //}

        //Set timer to destroy the ai or at least disable fov and enemy script

    }

    ////OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.ResetTrigger("AttackPlayer");
    //}

    //private void ResetAllBools(Animator animator)
    //{
    //    animator.SetBool("PlayerAttackRange", false);
    //    animator.SetBool("PlayerVisable", false);
    //}

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
