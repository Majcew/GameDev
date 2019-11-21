using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private GameObject player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = new Ray(player.transform.position, animator.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            animator.SetBool("isFollowing", true);
            animator.SetBool("isPatroling", false);
        }
        else
        {
            animator.SetBool("isFollowing", false);
            animator.SetBool("isPatroling", true);
        }
    }
}
