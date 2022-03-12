using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class TestEnemyAI : MonoBehaviour
{
    public LayerMask HidableLayers;
    public EnemyLineOfSightChecker1 LineOfSightChecker;
    public NavMeshAgent Agent;
    [Range(-1, 1)]
    [Tooltip("Lower is a better hiding Spot")]
    public float HideSensativity = 0;
    [Range(1,10)]
    public float MinPlayerDistance = 5f;

    [Range(0,5f)]
    public float MinObstacleHeight =1.25f;

    [Range(0.01f, 1f)]
    public float UpdateFrequency = 0.25f;

    private Coroutine MovementCorountine;
    private Collider[] Colliders = new Collider[10]; // more is less performance, but  more options  to hide    

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        LineOfSightChecker.OnGainSight += HandleGainSight;
        LineOfSightChecker.OnLooseSight +=HandleLooseSight;

    }

    private void HandleGainSight(Transform Target)
    {
        if (MovementCorountine != null)
        {
            StopCoroutine(MovementCorountine);
        }

        MovementCorountine = StartCoroutine (Hide(Target));
    }

    private void HandleLooseSight(Transform target)
    {
        if (MovementCorountine != null)
        {
            StopCoroutine(MovementCorountine);
        }
    }


    private IEnumerator Hide(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateFrequency);
        while(true)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i] = null;

            }


            int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, LineOfSightChecker.Collider.radius, Colliders,HidableLayers);
            

            int hitReduction = 0;
            for (int i = 0; i < hits; i++)
            {
                if (Vector3.Distance(Colliders[i].transform.position, target.position) < MinPlayerDistance || Colliders[i].bounds.size.y < MinObstacleHeight)
                {
                    Colliders[i] = null;
                    hitReduction++;
                }
            }
            hits -= hitReduction;

            System.Array.Sort(Colliders, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                if (NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Agent.areaMask))
                {
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask))
                    {
                        Debug.LogError($"Unable to find edge close to {hit.position}");
                    }

                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < HideSensativity)
                    {
                        Agent.SetDestination(hit.position);
                        break;
                    }
                    else
                    {
                        // Previous spot was not facing "away" from the player, and we try other side of the object 
                        if (NavMesh.SamplePosition(Colliders[i].transform.position - (target.position -hit.position).normalized * 2, out NavMeshHit hit2, 2f, Agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, Agent.areaMask))
                            {
                                Debug.LogError($"Unable to find edge close to {hit2.position} (Second attmept)");
                            }

                            if (Vector3.Dot(hit2.normal, (target.position - hit2.position).normalized) < HideSensativity)
                            {
                                Agent.SetDestination(hit2.position);
                                break;
                            }
                            }
                    }

                }
                
                else
                {
                    Debug.LogError($"Unable to find NavMesh near object {Colliders[i].name } at {Colliders[i].transform.position} " );
                }
            }
            yield return Wait;

        }
    }

    private int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(Agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(Agent.transform.position, B.transform.position));
        }
    }
}
