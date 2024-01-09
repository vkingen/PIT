using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackPlayer : StateMachineBehaviour
{
    private EnemyController enemy;
    private FieldOfView fov;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetAllBools(animator);
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

        if (enemy.isDead && enemy.isNetworkZombie)
        {
            animator.SetTrigger("Death");
        }

        if (fov.attackTarget == null)
        {
            animator.SetTrigger("ChasePlayer");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackPlayer");
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

    private void ResetAllBools(Animator animator)
    {
        animator.SetBool("PlayerAttackRange", false);
        animator.SetBool("PlayerVisable", false);
    }
}
