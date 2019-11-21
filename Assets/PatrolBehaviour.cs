using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    private GameObject player;
    private NavMeshAgent navMesh;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesh = animator.GetComponent<NavMeshAgent>();
        navMesh.autoBraking = false;
        navMesh.isStopped = false;
        navMesh.destination = new Vector3(Random.Range(-8f, 8f), 0.08f, Random.Range(-8f, 8f));
        animator.transform.LookAt(navMesh.destination);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = new Ray(player.transform.position, animator.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 8f))
        {
            animator.SetBool("isFollowing", true);
            animator.SetBool("isPatroling", false);
        }
        else if (!navMesh.pathPending && navMesh.remainingDistance <0.5f)
        {
            animator.SetBool("isFollowing", false);
            animator.SetBool("isPatroling", false);
        }
    }
}
