using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float beingChasedRange = 25f;
    public NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    PlaerMovementScript player;
    public bool RelicPicked = false;



    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent <NavMeshAgent>();
        player = FindObjectOfType<PlaerMovementScript>();

    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if (isProvoked & player.isVisible)
        {
            EngageTarget();

        }

        else if (distanceToTarget <= chaseRange && RelicPicked )
        {
            isProvoked = true; // add a setting to provoke the angel after picking the relic
        }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        if (distanceToTarget<=beingChasedRange)
        {
            isProvoked = false;
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }


    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //transform.rotation =  where the target is, we need to rotate at certain speed

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
