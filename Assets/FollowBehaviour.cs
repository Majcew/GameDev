using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowBehaviour : StateMachineBehaviour
{
    private GameObject player;
    private NavMeshAgent navMesh;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesh = animator.GetComponent<NavMeshAgent>();
        navMesh.isStopped = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = new Ray(player.transform.position, animator.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 6f))
        {
            animator.transform.LookAt(hit.point);
            navMesh.SetDestination(hit.point);
        }
        else
        {
            navMesh.isStopped = true;
            animator.SetBool("isFollowing", false);
            animator.SetBool("isPatroling", false);
        }
    }
}